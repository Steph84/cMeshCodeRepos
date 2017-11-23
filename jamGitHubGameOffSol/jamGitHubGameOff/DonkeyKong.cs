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
        Baril MyBaril;

        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        List<Vector2> ListMapPoints;

        #region Load Sprites
        SpriteGenerator MyDKStandingSprite;
        Texture2D DKStandingPic;
        int DKStandingFrameNumber;

        SpriteGenerator MyDKWalkingSprite;
        Texture2D DKWalkingPic;
        int DKWalkingFrameNumber;

        SpriteGenerator MyDKHoldBarilStandingSprite;
        Texture2D DKHoldBarilStandingPic;
        int DKHoldBarilStandingFrameNumber;

        SpriteGenerator MyDKHoldBarilWalkingSprite;
        Texture2D DKHoldBarilWalkingPic;
        int DKHoldBarilWalkingFrameNumber;

        SpriteGenerator MyDKLiftBarilSprite;
        Texture2D DKLiftBarilPic;
        int DKLiftBarilFrameNumber;

        SpriteGenerator MyDKThrowBarilSprite;
        Texture2D DKThrowBarilPic;
        int DKThrowBarilFrameNumber;
        #endregion

        Rectangle DonkeyKongPosition;
        double DKSpeedWalking;
        EnumSpriteDirection DonkeyKongDirection = EnumSpriteDirection.Left;
        EnumDonkeyKongAction DonkeyKongAction = EnumDonkeyKongAction.Standing;

        double elapsedTimePatroling = 0;
        double elapsedTimeBarilSpawn = 0;
        Random RandomObject = new Random();

        public DonkeyKong(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;

            #region Initialize Sprites
            DKStandingPic = Content.Load<Texture2D>("DKCStanding");
            DKStandingFrameNumber = 11;
            MyDKStandingSprite = new SpriteGenerator(SpriteBatch, DKStandingPic, DKStandingFrameNumber, false);
            MyDKStandingSprite.SourceQuad = new Rectangle(0, 0, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            MyDKStandingSprite.SpeedAnimation = 8.0d;

            DKWalkingPic = Content.Load<Texture2D>("DKCWalking");
            DKWalkingFrameNumber = 20;
            MyDKWalkingSprite = new SpriteGenerator(SpriteBatch, DKWalkingPic, DKWalkingFrameNumber, true);
            MyDKWalkingSprite.SourceQuad = new Rectangle(0, 0, MyDKWalkingSprite.FrameWidth, MyDKWalkingSprite.FrameHeight);
            MyDKWalkingSprite.SpeedAnimation = 10.0d;
            
            DKHoldBarilStandingPic = Content.Load<Texture2D>("DKHoldBarilStanding");
            DKHoldBarilStandingFrameNumber = 3;
            MyDKHoldBarilStandingSprite = new SpriteGenerator(SpriteBatch, DKHoldBarilStandingPic, DKHoldBarilStandingFrameNumber, false);
            MyDKHoldBarilStandingSprite.SourceQuad = new Rectangle(0, 0, MyDKHoldBarilStandingSprite.FrameWidth, MyDKHoldBarilStandingSprite.FrameHeight);
            MyDKHoldBarilStandingSprite.SpeedAnimation = 8.0d;

            DKHoldBarilWalkingPic = Content.Load<Texture2D>("DKHoldBarilWalking");
            DKHoldBarilWalkingFrameNumber = 15;
            MyDKHoldBarilWalkingSprite = new SpriteGenerator(SpriteBatch, DKHoldBarilWalkingPic, DKHoldBarilWalkingFrameNumber, false);
            MyDKHoldBarilWalkingSprite.SourceQuad = new Rectangle(0, 0, MyDKHoldBarilWalkingSprite.FrameWidth, MyDKHoldBarilWalkingSprite.FrameHeight);
            MyDKHoldBarilWalkingSprite.SpeedAnimation = 8.0d;

            DKLiftBarilPic = Content.Load<Texture2D>("DKLiftBaril");
            DKLiftBarilFrameNumber = 7;
            MyDKLiftBarilSprite = new SpriteGenerator(SpriteBatch, DKLiftBarilPic, DKLiftBarilFrameNumber, false);
            MyDKLiftBarilSprite.SourceQuad = new Rectangle(0, 0, MyDKLiftBarilSprite.FrameWidth, MyDKLiftBarilSprite.FrameHeight);
            MyDKLiftBarilSprite.SpeedAnimation = 8.0d;

            DKThrowBarilPic = Content.Load<Texture2D>("DKThrowBaril");
            DKThrowBarilFrameNumber = 19;
            MyDKThrowBarilSprite = new SpriteGenerator(SpriteBatch, DKThrowBarilPic, DKThrowBarilFrameNumber, false);
            MyDKThrowBarilSprite.SourceQuad = new Rectangle(0, 0, MyDKThrowBarilSprite.FrameWidth, MyDKThrowBarilSprite.FrameHeight);
            MyDKThrowBarilSprite.SpeedAnimation = 8.0d;
            #endregion

            DonkeyKongPosition = new Rectangle(700, 300, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            DKSpeedWalking = 0.063d; // minimum 0.0625 for 16ms frame rate

            DonkeyKongAction = EnumDonkeyKongAction.Walking;
            
        }

        public void DonkeyKongUpDate(GameTime pGameTime)
        {
            elapsedTimePatroling = elapsedTimePatroling + (pGameTime.ElapsedGameTime.Milliseconds) / 1000.0d;
            elapsedTimeBarilSpawn = elapsedTimeBarilSpawn + (pGameTime.ElapsedGameTime.Milliseconds) / 1000.0d;

            #region Baril Spawn
            if (elapsedTimeBarilSpawn > 5 && MyBaril == null)
            {
                MyBaril = new Baril(new Tuple<int, int>(GameWindowWidth, GameWindowHeight), Content, SpriteBatch);
            }
            #endregion


            #region Baril Spawn
            if (MyBaril != null)
            {
                MyBaril.BarilUpDate(pGameTime, DonkeyKongPosition);
            }
            #endregion

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
            DonkeyKongPosition.Y = (int)(DonkeyKongPosition.X * a + b - DonkeyKongPosition.Height/2);
            #endregion

            #region Manage movement along x
            if (DonkeyKongAction == EnumDonkeyKongAction.Walking)
            {
                DonkeyKongPosition.X = DonkeyKongPosition.X + (int)DonkeyKongDirection * (int)(DKSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds);

                if (DonkeyKongPosition.X > ListMapPoints[16].X - DonkeyKongPosition.Width / 2)
                    DonkeyKongDirection = EnumSpriteDirection.Left;

                if (DonkeyKongPosition.X < ListMapPoints[6].X + DonkeyKongPosition.Width / 2)
                    DonkeyKongDirection = EnumSpriteDirection.Right;
            }
            #endregion

            #region AI
            int tempRdmAI = RandomObject.Next(3, 7);
            if (elapsedTimePatroling > tempRdmAI)
            {
                switch (DonkeyKongAction)
                {
                    case EnumDonkeyKongAction.Standing:
                        DonkeyKongAction = EnumDonkeyKongAction.Walking;
                        elapsedTimePatroling = 0;
                        break;
                    case EnumDonkeyKongAction.Walking:
                        DonkeyKongAction = EnumDonkeyKongAction.Standing;
                        elapsedTimePatroling = 0;
                        break;
                    case EnumDonkeyKongAction.Running:
                        break;
                    case EnumDonkeyKongAction.Jumping:
                        break;
                    default:
                        break;
                }
            }



            #endregion

            #region Update the sprite direction and the animation
            switch (DonkeyKongAction)
            {
                case EnumDonkeyKongAction.Standing:
                    MyDKStandingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Walking:
                    MyDKWalkingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Lifting:
                    MyDKLiftBarilSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    MyDKHoldBarilStandingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.HoldingWalk:
                    MyDKHoldBarilWalkingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Throwing:
                    MyDKThrowBarilSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Running:
                    break;
                case EnumDonkeyKongAction.Jumping:
                    break;
                case EnumDonkeyKongAction.Falling:
                    break;
                case EnumDonkeyKongAction.Hit:
                    break;
                case EnumDonkeyKongAction.Celebrate:
                    break;
                default:
                    break;
            }
            #endregion
        }

        public void DonkeyKongDraw(GameTime pGameTime)
        {
            #region Baril Spawn
            if (MyBaril != null)
            {
                MyBaril.BarilDraw(pGameTime);
            }
            #endregion

            switch (DonkeyKongAction)
            {
                case EnumDonkeyKongAction.Standing:
                    MyDKStandingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Walking:
                    MyDKWalkingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Lifting:
                    MyDKLiftBarilSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    MyDKHoldBarilStandingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.HoldingWalk:
                    MyDKHoldBarilWalkingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Throwing:
                    MyDKThrowBarilSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Running:
                    break;
                case EnumDonkeyKongAction.Jumping:
                    break;
                case EnumDonkeyKongAction.Falling:
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
        Lifting = 2,
        HoldingStand = 3,
        HoldingWalk = 4,
        Throwing = 5,
        Running = 6,
        Jumping = 7,
        Falling = 8,
        Hit = 9,
        Celebrate = 10
    }
}