using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Fleet
{
    private Texture2D CarTexture { get; set; }
    public List<Car> ListCars { get; set; }
    private Vector2 SpriteOrigin { get; set; }
    private SpriteEffects SpriteDirection { get; set; }
    private int SpriteWidth { get; set; }
    private int SpriteHeight { get; set; }
    private int CarNumbers { get; set; }

    public Fleet(int pCarNumbers)
    {
        ListCars = new List<Car>();
        CarNumbers = pCarNumbers;
        CarTexture = TrafficSimulator.GlobalContent.Load<Texture2D>("carTest");
        SpriteDirection = SpriteEffects.None;
        SpriteOrigin = new Vector2(CarTexture.Bounds.Width/2, CarTexture.Bounds.Height/2);

        for (int i = 1; i < CarNumbers + 1; i++)
        {
            Car tempCar = new Car(i, CarTexture);

            ListCars.Add(tempCar);
        }
    }

    public void FleetUpdate(GameTime pGameTime)
    {
        double tempSecond = pGameTime.ElapsedGameTime.Milliseconds / 1000.0d;

        foreach (Car c in ListCars)
        {
            // movement in relation to the direction
            switch (c.Direction)
            {
                case EnumDirection.North:
                    c.PositionOnTile = new Rectangle(c.PositionOnTile.X, c.PositionOnTile.Y - (int)(c.Speed * tempSecond), c.PositionOnTile.Width, c.PositionOnTile.Height);
                    break;
                case EnumDirection.East:
                    break;
                case EnumDirection.South:
                    break;
                case EnumDirection.West:
                    c.PositionOnTile = new Rectangle(c.PositionOnTile.X - (int)(c.Speed * tempSecond), c.PositionOnTile.Y, c.PositionOnTile.Width, c.PositionOnTile.Height);
                    break;
                default:
                    break;
            }

            Tile actualTile = TrafficSimulator.MyMap.ListTiles.Where(x => x.Id == c.TileId).First();

            #region Movement in relation to the tile flag
            // crossroads
            if (actualTile.Flag == 15)
            {
                // threshold to make a turn
                // move then pick a direction and continue
            }
            // turns
            else if (Array.Exists(Constantes.TilesTurn, x => x == actualTile.Flag))
            {
                // threshold to make a turn
                // move then turn
                // change direction mandatory
                if (c.PositionOnTile.Y < 17) // find the right threshold
                {
                    c.Direction = EnumDirection.West;
                    c.Speed = 100;
                }
            }
            // turnarounds
            else if (Array.Exists(Constantes.TilesTurnAround, x => x == actualTile.Flag))
            {
                // threshold
                // move to the end
                // then turn around and change direction opposite
            }
            // t-shape crossroads
            else if (Array.Exists(Constantes.TilesTTurn, x => x == actualTile.Flag))
            {
                // threshold to make a turn
                // move then pick a direction and continue
            }
            #endregion

            #region The car changes tile
            if (c.PositionOnTile.X < 0 || c.PositionOnTile.X > Constantes.SquareSize || c.PositionOnTile.Y < 0 || c.PositionOnTile.Y > Constantes.SquareSize)
            {
                // if so search for the new tile
                Rectangle actualPosition = c.ComputeActualPositionOnMap(c);
                Tile newTile = TrafficSimulator.MyMap.ListTiles
                    .Where(x => actualPosition.X > x.SquareDestination.X && actualPosition.X < x.SquareDestination.X + Constantes.SquareSize)
                    .Where(x => actualPosition.Y > x.SquareDestination.Y && actualPosition.Y < x.SquareDestination.Y + Constantes.SquareSize)
                    .First();

                c.TileId = newTile.Id;

                // update PositionOnTile to be back inside
                if (c.PositionOnTile.X < 0 || c.PositionOnTile.X > Constantes.SquareSize)
                    c.PositionOnTile = c.UpdateActualPositionOnTile(c, -Math.Sign(c.PositionOnTile.X), 0);
                if (c.PositionOnTile.Y < 0 || c.PositionOnTile.Y > Constantes.SquareSize)
                    c.PositionOnTile = c.UpdateActualPositionOnTile(c, 0, -Math.Sign(c.PositionOnTile.Y));
            }
            #endregion

        }
    }

    public void FleetDraw(GameTime pGameTime)
    {
        foreach (Car c in ListCars)
        {
            // compute position by translation
            Tile actualTile = TrafficSimulator.MyMap.ListTiles.Where(x => x.Id == c.TileId).First();
            c.PositionOnMap = c.ComputeActualPositionOnMap(c);
            
            TrafficSimulator.spriteBatch.Draw(CarTexture, c.PositionOnMap, null, Color.White, Convert.ToSingle((int)c.Direction * Math.PI / 2.0d), SpriteOrigin, SpriteDirection, 0.0f);
        }
    }
}

public class Car
{
    public int Id { get; set; }
    public int TileId { get; set; }
    public EnumDirection Direction { get; set; }
    public double Speed { get; set; }
    public Rectangle PositionOnTile { get; set; }
    public Rectangle PositionOnMap { get; set; }

    public Car(int pId, Texture2D pCarTexture)
    {
        Id = pId;
        TileId = Constantes.SquareNumbers;
        Direction = EnumDirection.North;
        Speed = 500;
        PositionOnTile = new Rectangle(19 + 3, Constantes.SquareSize - pCarTexture.Height, pCarTexture.Width, pCarTexture.Height);
    }

    protected internal Rectangle ComputeActualPositionOnMap(Car pC)
    {
        Tile actualTile = TrafficSimulator.MyMap.ListTiles.Where(x => x.Id == pC.TileId).First();
        return new Rectangle()
        {
            Width = pC.PositionOnTile.Width,
            Height = pC.PositionOnTile.Height,
            X = actualTile.SquareDestination.X + pC.PositionOnTile.X,
            Y = actualTile.SquareDestination.Y + pC.PositionOnTile.Y
        };
    }

    protected internal Rectangle UpdateActualPositionOnTile(Car pC, int pXupdate, int pYupdate)
    {
        return new Rectangle()
        {
            Width = pC.PositionOnTile.Width,
            Height = pC.PositionOnTile.Height,
            X = pC.PositionOnTile.X + pXupdate * Constantes.SquareSize,
            Y = pC.PositionOnTile.Y + pYupdate * Constantes.SquareSize
        };
    }
}

public enum EnumDirection
{
    North = 0, // cts x=19
    East = 1, // cts y=
    South = 2, // cts x=7
    West = 3, // cts y=
}
