﻿using basicsTopDown.MapFolder;
using basicsTopDown.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace basicsTopDown.CharacterFolder
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
            DirectionBumping = EnumDirection.None;
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

            #region set the right tileSet line
            var tempCoefDirection = 0;
            switch (DirectionMoving)
            {
                case EnumDirection.North:
                    tempCoefDirection = 0;
                    break;
                case EnumDirection.East:
                    tempCoefDirection = 1;
                    break;
                case EnumDirection.South:
                    tempCoefDirection = 2;
                    break;
                case EnumDirection.West:
                    tempCoefDirection = 3;
                    break;
                case EnumDirection.North | EnumDirection.East:
                    tempCoefDirection = 1;
                    break;
                case EnumDirection.South | EnumDirection.East:
                    tempCoefDirection = 1;
                    break;
                case EnumDirection.South | EnumDirection.West:
                    tempCoefDirection = 3;
                    break;
                case EnumDirection.North | EnumDirection.West:
                    tempCoefDirection = 3;
                    break;
                default:
                    break;
            }
            #endregion

            // update of the SourceQuad
            SourceQuad = new Rectangle((int)CurrentFrame * SourceQuad.Width, tempCoefDirection * SourceQuad.Height,
                                       SourceQuad.Width, SourceQuad.Height);
        }
        #endregion

        public override void SpriteDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, SourceQuad, Color.White, 0, SpriteOrigin, SpriteEffect, 0);
            DebugToolBox.ShowLine(Content, SpriteBatch, DirectionMoving.ToString() + " / " + DirectionBumping.ToString(), new Vector2(Position.X, Position.Y));
        }

        public void CollisionCharacterOnMap(GameTime pGameTime, Map pMap, CharacterObject pCharacter)
        {
            List<Vector2> tempListTilesCoord = new List<Vector2>();
            List<string> tempListPropertiesName = new List<string>();
            
            if (pCharacter != null)
            {
                #region Extraction Bumping property
                foreach(PropertyInfo property in pCharacter.NSPointsInCoordinate.GetType().GetProperties())
                {
                    Vector2 tileCoord = (Vector2)property.GetValue(pCharacter.NSPointsInCoordinate, null);
                    if(pMap.MapTextureGrid[(int)tileCoord.Y, (int)tileCoord.X] == Map.MapTexture.Wall)
                    {
                        tempListTilesCoord.Add(tileCoord);
                        tempListPropertiesName.Add(property.Name);
                    }
                }
                #endregion

                #region Conversion Bumping property to Bumping Direction (manage direction number)
                if (tempListPropertiesName.Count != 0)
                {
                    foreach(string propName in tempListPropertiesName)
                    {
                        if(propName == "North")
                        {
                            UpdateDirectionBumping(EnumDirection.North);
                        }

                        if (propName == "East")
                        {
                            UpdateDirectionBumping(EnumDirection.East);
                        }

                        if (propName == "South")
                        {
                            UpdateDirectionBumping(EnumDirection.South);
                        }

                        if (propName == "West")
                        {
                            UpdateDirectionBumping(EnumDirection.West);
                        }

                        //if (propName == "NorthEast")
                        //{
                        //    UpdateDirectionBumping(EnumDirection.NorthEast);
                        //}

                        //if (propName == "SouthEast")
                        //{
                        //    UpdateDirectionBumping(EnumDirection.SouthEast);
                        //}

                        //if (propName == "SouthWest")
                        //{
                        //    UpdateDirectionBumping(EnumDirection.SouthWest);
                        //}

                        //if (propName == "NorthWest")
                        //{
                        //    UpdateDirectionBumping(EnumDirection.NorthWest);
                        //}
                    }
                }
                #endregion
            }
        }

        #region Method to calculate Charcater positions (9 slices) in pixel and in coordinates
        private void CalculateCharacterCoordinates(Map pMap)
        {
            #region 9 slices points position of the sprite in pixel
            NSPointsInPixel = new NineSlicePoints
            {
                North = new Vector2(Position.X + Size.Width / 2, Position.Y),
                East = new Vector2(Position.X + Size.Width, Position.Y + Size.Height / 2),
                South = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height),
                West = new Vector2(Position.X, Position.Y + Size.Height / 2),
                Center = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
                //NorthEast = new Vector2(Position.X + Size.Width, Position.Y),
                //SouthEast = new Vector2(Position.X + Size.Width, Position.Y + Size.Height),
                //SouthWest = new Vector2(Position.X, Position.Y + Size.Height),
                //NorthWest = new Vector2(Position.X, Position.Y),
            };
            #endregion

            #region 9 slices points position of the sprite in coordinate
            NSPointsInCoordinate = new NineSlicePoints
            {
                North = new Vector2((float)Math.Floor(NSPointsInPixel.North.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.North.Y / pMap.TileSizeShowing.Height)),
                East = new Vector2((float)Math.Floor(NSPointsInPixel.East.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.East.Y / pMap.TileSizeShowing.Height)),
                South = new Vector2((float)Math.Floor(NSPointsInPixel.South.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.South.Y / pMap.TileSizeShowing.Height)),
                West = new Vector2((float)Math.Floor(NSPointsInPixel.West.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.West.Y / pMap.TileSizeShowing.Height)),
                Center = new Vector2((float)Math.Floor(NSPointsInPixel.Center.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Center.Y / pMap.TileSizeShowing.Height))
                //NorthEast = new Vector2((float)Math.Floor(NSPointsInPixel.NorthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.NorthEast.Y / pMap.TileSizeShowing.Height)),
                //SouthEast = new Vector2((float)Math.Floor(NSPointsInPixel.SouthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.SouthEast.Y / pMap.TileSizeShowing.Height)),
                //SouthWest = new Vector2((float)Math.Floor(NSPointsInPixel.SouthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.SouthWest.Y / pMap.TileSizeShowing.Height)),
                //NorthWest = new Vector2((float)Math.Floor(NSPointsInPixel.NorthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.NorthWest.Y / pMap.TileSizeShowing.Height)),
            };
            #endregion
        }
        #endregion

        #region Method to update DirectionBumping member
        private void UpdateDirectionBumping(EnumDirection pEnumToAdd)
        {
            if (DirectionBumping == EnumDirection.None)
                DirectionBumping = pEnumToAdd;
            else
                DirectionBumping = DirectionBumping | pEnumToAdd;
        }
        #endregion
    }
}
