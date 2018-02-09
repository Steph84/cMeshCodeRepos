using basicsTopDown.MapFolder;
using basicsTopDown.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace basicsTopDown.CharacterFolder
{
    public enum EnumDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        NorthEast = 10,
        SouthEast = 11,
        SouthWest = 30,
        NorthWest = 31,
        None = 99
    }

    public class CharacterObject : SpriteObject
    {
        public string Name { get; set; }
        public EnumDirection DirectionMoving { get; set; }
        public EnumDirection DirectionBumping { get; set; }

        // for animation 
        private Rectangle SourceQuad { get; set; }
        private Rectangle FrameSize { get; set; }
        private int FrameNumber { get; set; }
        private double CurrentFrame { get; set; }
        private double SpeedAnimation { get; set; }
        private SpriteEffects SpriteEffect { get; set; }

        protected double SpeedWalking { get; set; }
        protected Map Map { get; set; }

        Vector2 SpriteOrigin = new Vector2();

        // properties for the movement

        public CharacterObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pGameSizeCoefficient)
        {
            FrameSize = pFrameSize;
            SourceQuad = new Rectangle(0, 0, FrameSize.Width, FrameSize.Height);
            SpriteData = Content.Load<Texture2D>(pSpriteName);
            DirectionMoving = EnumDirection.East;
            Map = pMap;

            // for animation
            CurrentFrame = 0;
            SpeedAnimation = 10;
            SpriteEffect = SpriteEffects.None;

            #region Check the line number
            // an animation tileSet is made of 4 lines
            // 1 animation toward north
            // 2 animation toward east
            // 3 animation toward south
            // 4 animation toward west
            
            if(SpriteData.Height / FrameSize.Height == 4)
            {
                // it's ok
            }
            else
            {
                throw new System.Exception("The animation tileSet don't have the good format");
            }
            #endregion

            #region Determine the frame number
            if ((SpriteData.Width / FrameSize.Width) % 1 == 0)
            {
                FrameNumber = SpriteData.Width / FrameSize.Width;
            }
            else
            {
                throw new System.Exception("The frame number is not an integer");
            }
            #endregion

            #region Initialize Sprite size showing
            int spriteWidthShowing = (int)Math.Round(FrameSize.Width * pGameSizeCoefficient, MidpointRounding.AwayFromZero);
            int spriteHeightShowing = (int)Math.Round(FrameSize.Height * pGameSizeCoefficient, MidpointRounding.AwayFromZero);

            Size = new Rectangle(0, 0, spriteWidthShowing, spriteHeightShowing);
            Position = new Rectangle(pPosition.X, pPosition.Y, spriteWidthShowing, spriteHeightShowing);
            #endregion

            CalculateCharacterCoordinates(Map);
        }


        #region override Update to manange animation
        protected override void SpriteUpdate(GameTime pGameTime, MapFolder.Map pMap)
        {
            if (IsMoving)
            {
                // Update character coordinates
                CalculateCharacterCoordinates(pMap);

                // CurrentFrame update for the animation
                CurrentFrame = CurrentFrame + (SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

                if (CurrentFrame > FrameNumber)
                    CurrentFrame = 0;
            }
            else
            {
                CurrentFrame = 0;
            }

            // fix sprite direction for diagonales
            var tempCoefDir = (int)DirectionMoving;
            if (tempCoefDir >= 10)
            {
                tempCoefDir = (int)Math.Floor(tempCoefDir / 10.0d);
            }

            // update of the SourceQuad
            SourceQuad = new Rectangle((int)CurrentFrame * SourceQuad.Width, tempCoefDir * SourceQuad.Height,
                                       SourceQuad.Width, SourceQuad.Height);
        }
        #endregion

        public override void SpriteDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, SourceQuad, Color.White, 0, SpriteOrigin, SpriteEffect, 0);
            DebugToolBox.ShowLine(Content, SpriteBatch, NSPointsInCoordinate.Coord9Center.ToString(), new Vector2(Position.X, Position.Y));
        }

        public static bool CollisionCharacterOnMap(GameTime pGameTime, Map pMap, CharacterObject pCharacter)
        {
            bool tempToReturn = false;

            if(pCharacter != null)
            {
                List<Vector2> tempListTilesCoord = new List<Vector2>();
                List<string> tempListPropertiesName = new List<string>();

                foreach(PropertyInfo property in pCharacter.NSPointsInCoordinate.GetType().GetProperties())
                {
                    Vector2 tileCoord = (Vector2)property.GetValue(pCharacter.NSPointsInCoordinate, null);
                    if(pMap.MapTextureGrid[(int)tileCoord.Y, (int)tileCoord.X] == Map.MapTexture.Wall)
                    {
                        tempListTilesCoord.Add(tileCoord);
                        tempListPropertiesName.Add(property.Name);
                        tempToReturn = true;
                    }
                }
            }
            return tempToReturn;
        }

        private void CalculateCharacterCoordinates(Map pMap)
        {
            #region 9 slices points position of the sprite in pixel
            NSPointsInPixel = new NineSlicePoints
            {
                Coord1North = new Vector2(Position.X + Size.Width / 2, Position.Y),
                Coord2NorthEast = new Vector2(Position.X + Size.Width, Position.Y),
                Coord3East = new Vector2(Position.X + Size.Width, Position.Y + Size.Height / 2),
                Coord4SouthEast = new Vector2(Position.X + Size.Width, Position.Y + Size.Height),
                Coord5South = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height),
                Coord6SouthWest = new Vector2(Position.X, Position.Y + Size.Height),
                Coord7West = new Vector2(Position.X, Position.Y + Size.Height / 2),
                Coord8NorthWest = new Vector2(Position.X, Position.Y),
                Coord9Center = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
            };
            #endregion

            #region 9 slices points position of the sprite in coordinate
            NSPointsInCoordinate = new NineSlicePoints
            {
                Coord1North = new Vector2((float)Math.Floor(NSPointsInPixel.Coord1North.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord1North.Y / pMap.TileSizeShowing.Height)),
                Coord2NorthEast = new Vector2((float)Math.Floor(NSPointsInPixel.Coord2NorthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord2NorthEast.Y / pMap.TileSizeShowing.Height)),
                Coord3East = new Vector2((float)Math.Floor(NSPointsInPixel.Coord3East.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord3East.Y / pMap.TileSizeShowing.Height)),
                Coord4SouthEast = new Vector2((float)Math.Floor(NSPointsInPixel.Coord4SouthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord4SouthEast.Y / pMap.TileSizeShowing.Height)),
                Coord5South = new Vector2((float)Math.Floor(NSPointsInPixel.Coord5South.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord5South.Y / pMap.TileSizeShowing.Height)),
                Coord6SouthWest = new Vector2((float)Math.Floor(NSPointsInPixel.Coord6SouthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord6SouthWest.Y / pMap.TileSizeShowing.Height)),
                Coord7West = new Vector2((float)Math.Floor(NSPointsInPixel.Coord7West.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord7West.Y / pMap.TileSizeShowing.Height)),
                Coord8NorthWest = new Vector2((float)Math.Floor(NSPointsInPixel.Coord8NorthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord8NorthWest.Y / pMap.TileSizeShowing.Height)),
                Coord9Center = new Vector2((float)Math.Floor(NSPointsInPixel.Coord9Center.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord9Center.Y / pMap.TileSizeShowing.Height))
            };
            #endregion
        }

    }
}
