﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jamGitHubGameOff
{
    public class Baril
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        List<Vector2> ListMapPoints;

        SpriteGenerator MyBarilSprite;
        Texture2D BarilPic;
        int BarilFrameNumber;
        
        public Rectangle BarilPosition { get; set; }
        double BarilCurrentFrame;
        Vector2 BarilOrigin;
        SpriteEffects BarilDirection = SpriteEffects.None;
        public EnumBarilState BarilState { get; set; }
        double BarilSpeedUp, BarilSpeedBack, BarilSpeedThrow;

        Random RandomObject = new Random();

        public Baril(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;

            BarilPic = Content.Load<Texture2D>("DKBaril");
            BarilFrameNumber = 5;
            MyBarilSprite = new SpriteGenerator(SpriteBatch, BarilPic, BarilFrameNumber, false, false);
            MyBarilSprite.SourceQuad = new Rectangle(0, 0, MyBarilSprite.FrameWidth, MyBarilSprite.FrameHeight);
            MyBarilSprite.SpeedAnimation = 8.0d;
            BarilOrigin = new Vector2(MyBarilSprite.FrameWidth / 2, MyBarilSprite.FrameHeight / 2);
            BarilCurrentFrame = 0;
            int BarilSpawnPosX = RandomObject.Next((int)ListMapPoints[9].X, (int)ListMapPoints[16].X); // 9 to 16
            BarilPosition = new Rectangle(BarilSpawnPosX, 0, MyBarilSprite.FrameWidth, MyBarilSprite.FrameHeight);

            BarilState = EnumBarilState.Standing;
            BarilSpeedUp = 0.065;
            BarilSpeedBack = 0.065;
        }

        public void BarilUpDate(GameTime pGameTime, Rectangle pDonkeyKongPosition,
                                EnumDonkeyKongAction? pDonkeyKongAction, SpriteGenerator pSprite = null)
        {
            switch (pDonkeyKongAction)
            {
                case EnumDonkeyKongAction.Lifting:
                    BarilState = EnumBarilState.Lifted;
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    BarilState = EnumBarilState.Held;
                    break;
                case EnumDonkeyKongAction.Throwing:
                    if (pSprite != null && pSprite.CurrentFrame < 5) // != null to avoid lag between states
                    {
                        BarilState = EnumBarilState.MoveBack;
                    }
                    else
                    {
                        BarilState = EnumBarilState.Thrown;
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine(BarilState);
            switch (BarilState)
            {
                case EnumBarilState.Standing:
                    BarilPosition = GroundCollision.StickToTheGround(BarilPosition, ListMapPoints);
                    BarilCurrentFrame = 0;
                    break;
                case EnumBarilState.Lifted:
                    if (BarilCurrentFrame < 4)
                    {
                        BarilCurrentFrame = BarilCurrentFrame + (MyBarilSprite.SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);
                    }
                    if (BarilPosition.Y + BarilPosition.Height > pDonkeyKongPosition.Top)
                    {
                        BarilPosition = new Rectangle(BarilPosition.X, BarilPosition.Y - (int)(BarilSpeedUp * pGameTime.ElapsedGameTime.Milliseconds), BarilPosition.Width, BarilPosition.Height);
                    }
                    break;
                case EnumBarilState.Held:
                    BarilPosition = new Rectangle(BarilPosition.X, pDonkeyKongPosition.Top - BarilPosition.Height, BarilPosition.Width, BarilPosition.Height);
                    BarilCurrentFrame = 4;
                    break;
                case EnumBarilState.MoveBack:
                    int tempBarilXPos = 0;
                    tempBarilXPos = BarilPosition.X + (int)(BarilSpeedBack * pGameTime.ElapsedGameTime.Milliseconds);
                    BarilPosition = new Rectangle(tempBarilXPos, BarilPosition.Y, BarilPosition.Width, BarilPosition.Height);
                    BarilCurrentFrame = 4;
                    break;
                case EnumBarilState.Thrown:
                    // compute trajectory
                    BarilCurrentFrame = 4;
                    break;
                default:
                    break;
            }

            MyBarilSprite.SourceQuad = new Rectangle((int)Math.Floor(BarilCurrentFrame) * MyBarilSprite.FrameWidth,
                                                    MyBarilSprite.SourceQuad.Y,
                                                    MyBarilSprite.SourceQuad.Width,
                                                    MyBarilSprite.SourceQuad.Height);
        }

        public void BarilDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(BarilPic, BarilPosition, MyBarilSprite.SourceQuad, Color.White, 0, BarilOrigin, BarilDirection, 0);

            //switch (BarilState)
            //{
            //    case EnumBarilState.Standing:
            //        SpriteBatch.Draw(SpritePicture, pDonkeyKongPosition, SourceQuad, Color.White, 0, SpriteOrigin, SpriteDirection, 0);
            //        break;
            //    case EnumBarilState.Lifted:
            //        SpriteBatch.Draw(SpritePicture, pDonkeyKongPosition, SourceQuad, Color.White, 0, SpriteOrigin, SpriteDirection, 0);
            //        break;
            //    case EnumBarilState.Held:
            //        break;
            //    case EnumBarilState.Thrown:
            //        break;
            //    default:
            //        break;
            //}
        }

        public enum EnumBarilState
        {
            Standing = 1,
            Lifted = 2,
            Held = 3,
            MoveBack = 4,
            Thrown = 5
        }
    }
}
