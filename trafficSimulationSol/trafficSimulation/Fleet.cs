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
        SpriteOrigin = new Vector2();
            
        for (int i = 1; i < CarNumbers + 1; i++)
        {
            Car tempCar = new Car()
            {
                Id = i,
                TileId = Constantes.SquareNumbers,
                Direction = EnumDirection.North,
                Speed = 0,
                PositionOnTile = new Rectangle(19, Constantes.SquareSize - CarTexture.Height, CarTexture.Width, CarTexture.Height)
            };

            ListCars.Add(tempCar);
        }
    }

    public void FleetDraw(GameTime pGameTime)
    {
        foreach (Car c in ListCars)
        {
            // compute position by translation
            Tile actualTile = TrafficSimulator.MyMap.ListTiles.Where(x => x.Id == c.TileId).First();
            c.PositionOnMap = new Rectangle()
            {
                Width = c.PositionOnTile.Width,
                Height = c.PositionOnTile.Height,
                X = actualTile.SquareDestination.X + c.PositionOnTile.X,
                Y = actualTile.SquareDestination.Y + c.PositionOnTile.Y
            };
            TrafficSimulator.spriteBatch.Draw(CarTexture, c.PositionOnMap, null, Color.White, 0.0f, SpriteOrigin, SpriteDirection, 0.0f);
        }
    }
}

public class Car
{
    public int Id { get; set; }
    public int TileId { get; set; }
    public EnumDirection Direction { get; set; }
    public int Speed { get; set; }
    public Rectangle PositionOnTile { get; set; }
    public Rectangle PositionOnMap { get; set; }
}

public enum EnumDirection
{
    North = 1, // cts x=19
    East = 2, // cts y=
    South = 3, // cts x=7
    West = 4, // cts y=
}
