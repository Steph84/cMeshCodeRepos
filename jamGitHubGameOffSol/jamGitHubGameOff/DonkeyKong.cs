using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jamGitHubGameOff
{
    public class DonkeyKong
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        List<Vector2> ListMapPoints;

        SpriteGenerator MyDKStandingSprite;
        Texture2D DKStandingPic;
        int DKStandingFrameNumber;

        SpriteGenerator MyDKWalkingSprite;
        Texture2D DKWalkingPic;
        int DKWalkingFrameNumber;
        
        Rectangle DonkeyKongPosition;
        double DKSpeedFalling;
        double DKSpeedWalking;
        EnumSpriteDirection DonkeyKongDirection = EnumSpriteDirection.Left;
        EnumDonkeyKongAction DonkeyKongAction = EnumDonkeyKongAction.Standing;

        

        public DonkeyKong(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;

            DKStandingPic = Content.Load<Texture2D>("DKCStanding"); // 11 42x40
            DKStandingFrameNumber = 11;
            MyDKStandingSprite = new SpriteGenerator(SpriteBatch, DKStandingPic, DKStandingFrameNumber, false);
            MyDKStandingSprite.SourceQuad = new Rectangle(0, 0, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            MyDKStandingSprite.SpeedAnimation = 8.0d;

            DKWalkingPic = Content.Load<Texture2D>("DKCWalking"); // 20
            DKWalkingFrameNumber = 20;
            MyDKWalkingSprite = new SpriteGenerator(SpriteBatch, DKWalkingPic, DKWalkingFrameNumber, true);
            MyDKWalkingSprite.SourceQuad = new Rectangle(0, 0, MyDKWalkingSprite.FrameWidth, MyDKWalkingSprite.FrameHeight);
            MyDKWalkingSprite.SpeedAnimation = 10.0d;

            DonkeyKongPosition = new Rectangle(100, 200, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            DKSpeedFalling = 0.2d;
            DKSpeedWalking = 0.07d; // minimum 0.0625 for 16ms frame rate

            DonkeyKongAction = EnumDonkeyKongAction.Walking;
        }

        public void DonkeyKongUpDate(GameTime pGameTime)
        {
            #region Manage collision on the ground
            // find the 2 points around DK
            Vector2 leftBoundary = ListMapPoints.Where(x => DonkeyKongPosition.X >= x.X).LastOrDefault();
            if (leftBoundary == null)
                leftBoundary = ListMapPoints.First();
            Vector2 rightBoundary = ListMapPoints.Where(x => DonkeyKongPosition.X < x.X).FirstOrDefault();
            if (rightBoundary == null)
                rightBoundary = ListMapPoints.Last();

            // compute equation coeff
            double a = (rightBoundary.Y - leftBoundary.Y) / (rightBoundary.X - leftBoundary.X);
            double b = leftBoundary.Y - leftBoundary.X * a;

            // modify DK y
            if ((DonkeyKongPosition.Y + DonkeyKongPosition.Height/2) < (DonkeyKongPosition.X * a + b))
                DonkeyKongPosition.Y = DonkeyKongPosition.Y + (int)(DKSpeedFalling * pGameTime.ElapsedGameTime.Milliseconds);
            else
                DonkeyKongPosition.Y = (int)(DonkeyKongPosition.X * a + b - DonkeyKongPosition.Height/2);
            #endregion

            #region Manage movement along x
            DonkeyKongPosition.X = DonkeyKongPosition.X + (int)DonkeyKongDirection * (int)(DKSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds);

            if (DonkeyKongPosition.X > GameWindowWidth - DonkeyKongPosition.Width / 2)
                DonkeyKongDirection = EnumSpriteDirection.Left;

            if (DonkeyKongPosition.X < DonkeyKongPosition.Width / 2)
                DonkeyKongDirection = EnumSpriteDirection.Right;
            #endregion

            // update the sprite direction and the animation
            switch (DonkeyKongAction)
            {
                case EnumDonkeyKongAction.Standing:
                    MyDKStandingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Walking:
                    MyDKWalkingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Running:
                    break;
                case EnumDonkeyKongAction.Jumping:
                    break;
                case EnumDonkeyKongAction.Falling:
                    break;
                case EnumDonkeyKongAction.Throwing:
                    break;
                case EnumDonkeyKongAction.Hit:
                    break;
                case EnumDonkeyKongAction.Celebrate:
                    break;
                default:
                    break;
            }
        }

        public void DonkeyKongDraw(GameTime pGameTime)
        {
            switch (DonkeyKongAction)
            {
                case EnumDonkeyKongAction.Standing:
                    MyDKStandingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Walking:
                    MyDKWalkingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Running:
                    break;
                case EnumDonkeyKongAction.Jumping:
                    break;
                case EnumDonkeyKongAction.Falling:
                    break;
                case EnumDonkeyKongAction.Throwing:
                    break;
                case EnumDonkeyKongAction.Hit:
                    break;
                case EnumDonkeyKongAction.Celebrate:
                    break;
                default:
                    break;
            }
        }
    }
    
    public enum EnumDonkeyKongAction
    {
        Standing = 0,
        Walking = 1,
        Running = 2,
        Jumping = 3,
        Falling = 4,
        Throwing = 5,
        Hit = 6,
        Celebrate = 7
    }
}
