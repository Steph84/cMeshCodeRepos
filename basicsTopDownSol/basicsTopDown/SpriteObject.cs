using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using static basicsTopDown.MapFolder.Map;

namespace basicsTopDown
{
    [Flags]
    public enum EnumDirection
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        NorthEast = 16,
        SouthEast = 32,
        SouthWest = 64,
        NorthWest = 128,
        None = 256
    }

    public class SpriteObject
    {
        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }
        public EnumDirection DirectionMoving { get; set; }
        public EnumDirection DirectionBumping { get; set; }

        protected bool IsMoving { get; set; }
        protected double GameSizeCoefficient { get; set; }
        protected Rectangle Size { get; set; }
        protected ContentManager Content { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Map Map { get; set; }
        protected Rectangle OldPosition { get; set; }
        protected NineSlicePoints NSPointsInPixel { get; set; }
        protected NineSlicePoints OldNSPointsInPixel { get; set; }

        private NineSlicePoints NSPointsInCoordinate { get; set; }
        
        public class NineSlicePoints
        {
            //public Vector2 North { get; set; }
            //public Vector2 East { get; set; }
            //public Vector2 South { get; set; }
            //public Vector2 West { get; set; }
            public Vector2 Center { get; set; }
            public Vector2 NorthEast { get; set; }
            public Vector2 SouthEast { get; set; }
            public Vector2 SouthWest { get; set; }
            public Vector2 NorthWest { get; set; }
        }

        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, double pGameSizeCoefficient, Map pMap)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            IsMoving = false;
            GameSizeCoefficient = pGameSizeCoefficient;
            Map = pMap;
        }

        #region SpriteUpdate
        public virtual void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            #region Manage collision Sprite on Map
            if (IsMoving == true)
            {
                DirectionBumping = EnumDirection.None;
                CalculateSpriteCoordinates(pMap);

                // if there is a sprite colliding into specific texture at the end of the movement
                if(CheckCollisionOnTexture(pMap, MapTexture.Wall))
                {
                    // let's go back in time to correct the position

                    #region Process the segments equation
                    Dictionary<string, List<Vector2>> DictStartEndPoints = new Dictionary<string, List<Vector2>>();
                    Dictionary<string, Tuple<float, float>> DictSegmentEquations = new Dictionary<string, Tuple<float, float>>();

                    // extract Old positions
                    foreach (PropertyInfo property in OldNSPointsInPixel.GetType().GetProperties())
                    {
                        if (property.Name != "Center")
                        {
                            Vector2 tempCoord = (Vector2)property.GetValue(OldNSPointsInPixel, null);
                            DictStartEndPoints.Add(property.Name, new List<Vector2>() { tempCoord });
                        }
                    }

                    // extract current virtual positions
                    foreach (PropertyInfo property in NSPointsInPixel.GetType().GetProperties())
                    {
                        if (property.Name != "Center")
                        {
                            Vector2 tempCoord = (Vector2)property.GetValue(NSPointsInPixel, null);
                            DictStartEndPoints[property.Name].Add(tempCoord);
                        }
                    }

                    // calculate the segements equation
                    foreach(KeyValuePair<string, List<Vector2>> startEndPoint in DictStartEndPoints)
                    {
                        DictSegmentEquations.Add(startEndPoint.Key, CalculateSegmentEquation(startEndPoint.Value[0], startEndPoint.Value[1]));
                    }
                    #endregion

                    #region Create list of abscissas to parse
                    Dictionary<string, List<float>> DictListAbscissa = new Dictionary<string, List<float>>();
                    foreach (KeyValuePair<string, List<Vector2>> startEndPointBis in DictStartEndPoints)
                    {
                        DictListAbscissa.Add(startEndPointBis.Key, new List<float>());
                        float tempVal = startEndPointBis.Value[0].X;
                        while(tempVal <= startEndPointBis.Value[1].X)
                        {
                            DictListAbscissa[startEndPointBis.Key].Add(tempVal);
                            tempVal += 0.5f;
                        }
                    }
                    #endregion

                    #region Parse abscissas
                    Dictionary<string, int?> DictStepColliding = new Dictionary<string, int?>();
                    foreach (KeyValuePair<string, List<float>> listAbscissas in DictListAbscissa)
                    {
                        DictStepColliding.Add(listAbscissas.Key, null);

                        for (int i = 0; i < listAbscissas.Value.Count; i++)
                        {
                            float localItem = listAbscissas.Value[i];

                            float tempY = CalculateOrdonateViaAbscissa(localItem, DictSegmentEquations[listAbscissas.Key]);
                            Vector2 tempTile = CalculateCoordinatesInTileWithPixel(new Vector2(localItem, tempY), pMap.TileSizeShowing);

                            if (pMap.MapTextureGrid[(int)tempTile.Y, (int)tempTile.X] == MapTexture.Wall)
                            {
                                DictStepColliding[listAbscissas.Key] = i;
                                continue;
                            }
                        }
                    }
                    #endregion

                    int stepToGoBack = 999;
                    foreach(KeyValuePair<string, int?> stepColliding in DictStepColliding)
                    {
                        if(stepColliding.Value != null)
                        {
                            if (stepToGoBack > stepColliding.Value - 1)
                            {
                                stepToGoBack = (int)stepColliding.Value - 1;
                            }
                        }
                    }
                    
                    if(stepToGoBack == -1)
                    {
                        Position = OldPosition;
                    }
                    else if(stepToGoBack < 999)
                    {
                        float xBack = DictListAbscissa["NorthWest"][stepToGoBack];
                        float yBack = CalculateOrdonateViaAbscissa(xBack, DictSegmentEquations["NorthWest"]);
                        //Position = new Rectangle(xBack, yBack, Position.Width, Position.Height);
                    }

                    int a = 1;

                    // create lists of x coordinate to parse for each corner

                    // parse the lists with the 4 equations to check when the sprite collide

                    // manage the slide after the collide

                }

                // old collision method
                //if (CollisionSpriteOnMap(pGameTime, pMap, this) != null)
                //{
                //    Position = OldPosition;
                //}
            }
            #endregion
        }
        #endregion

        public virtual void SpriteDraw(GameTime pGameTime) { }

        #region Method to calculate Y=f(X)
        private float CalculateOrdonateViaAbscissa(float pX, Tuple<float, float> pGradientYIntersect)
        {
            return (pX * pGradientYIntersect.Item1 + pGradientYIntersect.Item2);
        }
        #endregion
        
        #region Method to calculate the segement equation
        private Tuple<float, float> CalculateSegmentEquation(Vector2 pStartPoint, Vector2 pEndPoint)
        {
            float gradient = 0;
            float yIntercept = 0;
            //if (pEndPoint.X != pStartPoint.X)
            //{
            //    gradient = (pEndPoint.Y - pStartPoint.Y) / (pEndPoint.X - pStartPoint.X);
            //}
            yIntercept = pStartPoint.Y - (pStartPoint.X * gradient);

            return new Tuple<float, float>(gradient, yIntercept);
        }
        #endregion

        #region Method to check if the sprite collide with a specific texture on the map
        private bool CheckCollisionOnTexture(Map pMap, MapTexture pTexture)
        {
            foreach (PropertyInfo property in NSPointsInCoordinate.GetType().GetProperties())
            {
                Vector2 tileCoord = (Vector2)property.GetValue(NSPointsInCoordinate, null);

                if (pMap.MapTextureGrid[(int)tileCoord.Y, (int)tileCoord.X] == MapTexture.Wall)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Method to check if a sprite collide with the map basic
        public static TileObject CollisionSpriteOnMap(GameTime pGameTime, Map pMap, SpriteObject pSprite)
        {
            TileObject tile = null;

            for (int row = 0; row < pMap.MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < pMap.MapSizeInTile.Width; column++)
                {
                    if (pMap.MapGrid[row, column].Texture == Map.MapTexture.Wall)
                    {
                        bool collide = false;
                        collide = CollisionObject.CheckCollision(pMap.MapGrid[row, column].Position, pSprite.Position);
                        if (collide == true)
                        {
                            tile = pMap.MapGrid[row, column];
                        }
                    }
                }
            }
            return tile;
        }
        #endregion
        
        #region Method to calculate Charcater positions (9 slices) in pixel and in coordinates
        protected void CalculateSpriteCoordinates(Map pMap)
        {
            #region 9 slices points position of the sprite in pixel
            NSPointsInPixel = new NineSlicePoints
            {
                //North = new Vector2(Position.X + Size.Width / 2, Position.Y),
                //East = new Vector2(Position.X + Size.Width, Position.Y + Size.Height / 2),
                //South = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height),
                //West = new Vector2(Position.X, Position.Y + Size.Height / 2),
                Center = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2),
                NorthEast = new Vector2(Position.X + Size.Width, Position.Y),
                SouthEast = new Vector2(Position.X + Size.Width, Position.Y + Size.Height),
                SouthWest = new Vector2(Position.X, Position.Y + Size.Height),
                NorthWest = new Vector2(Position.X, Position.Y)
            };
            #endregion

            #region 9 slices points position of the sprite in coordinate
            NSPointsInCoordinate = new NineSlicePoints
            {
                //North = new Vector2((float)Math.Floor(NSPointsInPixel.North.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.North.Y / pMap.TileSizeShowing.Height)),
                //East = new Vector2((float)Math.Floor(NSPointsInPixel.East.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.East.Y / pMap.TileSizeShowing.Height)),
                //South = new Vector2((float)Math.Floor(NSPointsInPixel.South.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.South.Y / pMap.TileSizeShowing.Height)),
                //West = new Vector2((float)Math.Floor(NSPointsInPixel.West.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.West.Y / pMap.TileSizeShowing.Height)),
                Center = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.Center, pMap.TileSizeShowing),
                NorthEast = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.NorthEast, pMap.TileSizeShowing),
                SouthEast = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.SouthEast, pMap.TileSizeShowing),
                SouthWest = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.SouthWest, pMap.TileSizeShowing),
                NorthWest = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.NorthWest, pMap.TileSizeShowing)
            };
            #endregion
        }
        #endregion

        #region Method to calculate coordinates in tile with coordinates in pixel
        private Vector2 CalculateCoordinatesInTileWithPixel(Vector2 pCoordInPixel, Rectangle pTileSize)
        {
            return new Vector2((float)Math.Floor(pCoordInPixel.X / pTileSize.Width), (float)Math.Floor(pCoordInPixel.Y / pTileSize.Height));
        }
        #endregion
    }
}
