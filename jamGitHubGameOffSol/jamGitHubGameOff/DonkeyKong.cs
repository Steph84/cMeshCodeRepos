using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static jamGitHubGameOff.Baril;

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
        double elapsedTimeBarilTargeting = 0;
         Random RandomObject = new Random();

        int deltaPosX;

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
            MyDKStandingSprite = new SpriteGenerator(SpriteBatch, DKStandingPic, DKStandingFrameNumber, false, true);
            MyDKStandingSprite.SourceQuad = new Rectangle(0, 0, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            MyDKStandingSprite.SpeedAnimation = 8.0d;

            DKWalkingPic = Content.Load<Texture2D>("DKCWalking");
            DKWalkingFrameNumber = 20;
            MyDKWalkingSprite = new SpriteGenerator(SpriteBatch, DKWalkingPic, DKWalkingFrameNumber, true, true);
            MyDKWalkingSprite.SourceQuad = new Rectangle(0, 0, MyDKWalkingSprite.FrameWidth, MyDKWalkingSprite.FrameHeight);
            MyDKWalkingSprite.SpeedAnimation = 10.0d;
            
            DKHoldBarilStandingPic = Content.Load<Texture2D>("DKHoldBarilStanding");
            DKHoldBarilStandingFrameNumber = 3;
            MyDKHoldBarilStandingSprite = new SpriteGenerator(SpriteBatch, DKHoldBarilStandingPic, DKHoldBarilStandingFrameNumber, true, true);
            MyDKHoldBarilStandingSprite.SourceQuad = new Rectangle(0, 0, MyDKHoldBarilStandingSprite.FrameWidth, MyDKHoldBarilStandingSprite.FrameHeight);
            MyDKHoldBarilStandingSprite.SpeedAnimation = 4.0d;

            DKHoldBarilWalkingPic = Content.Load<Texture2D>("DKHoldBarilWalking");
            DKHoldBarilWalkingFrameNumber = 15;
            MyDKHoldBarilWalkingSprite = new SpriteGenerator(SpriteBatch, DKHoldBarilWalkingPic, DKHoldBarilWalkingFrameNumber, true, true);
            MyDKHoldBarilWalkingSprite.SourceQuad = new Rectangle(0, 0, MyDKHoldBarilWalkingSprite.FrameWidth, MyDKHoldBarilWalkingSprite.FrameHeight);
            MyDKHoldBarilWalkingSprite.SpeedAnimation = 8.0d;

            DKLiftBarilPic = Content.Load<Texture2D>("DKLiftBaril");
            DKLiftBarilFrameNumber = 7;
            MyDKLiftBarilSprite = new SpriteGenerator(SpriteBatch, DKLiftBarilPic, DKLiftBarilFrameNumber, true, false);
            MyDKLiftBarilSprite.SourceQuad = new Rectangle(0, 0, MyDKLiftBarilSprite.FrameWidth, MyDKLiftBarilSprite.FrameHeight);
            MyDKLiftBarilSprite.SpeedAnimation = 12.0d;

            DKThrowBarilPic = Content.Load<Texture2D>("DKThrowBaril");
            DKThrowBarilFrameNumber = 19;
            MyDKThrowBarilSprite = new SpriteGenerator(SpriteBatch, DKThrowBarilPic, DKThrowBarilFrameNumber, true, false);
            MyDKThrowBarilSprite.SourceQuad = new Rectangle(0, 0, MyDKThrowBarilSprite.FrameWidth, MyDKThrowBarilSprite.FrameHeight);
            MyDKThrowBarilSprite.SpeedAnimation = 20.0d;
            #endregion

            int DKSpawnPosX = RandomObject.Next((int)ListMapPoints[9].X, (int)ListMapPoints[16].X); // 9 to 16
            DonkeyKongPosition = new Rectangle(DKSpawnPosX, 0, MyDKStandingSprite.FrameWidth, MyDKStandingSprite.FrameHeight);
            DKSpeedWalking = 0.063d; // minimum 0.0625 for 16ms frame rate

            DonkeyKongAction = EnumDonkeyKongAction.Standing;
        }

        public void DonkeyKongUpDate(GameTime pGameTime)
        {
            elapsedTimePatroling = elapsedTimePatroling + (pGameTime.ElapsedGameTime.Milliseconds) / 1000.0d;
            elapsedTimeBarilSpawn = elapsedTimeBarilSpawn + (pGameTime.ElapsedGameTime.Milliseconds) / 1000.0d;
            
            #region Baril Spawn + Movement
            if (elapsedTimeBarilSpawn > 2 && MyBaril == null)
            {
                MyBaril = new Baril(new Tuple<int, int>(GameWindowWidth, GameWindowHeight), Content, SpriteBatch, ListMapPoints);
            }
            if (MyBaril != null && (MyBaril.BarilState == EnumBarilState.Standing
                                    || MyBaril.BarilState == EnumBarilState.Lifted
                                    || MyBaril.BarilState == EnumBarilState.Held))
            {
                deltaPosX = MyBaril.BarilPosition.X - DonkeyKongPosition.X;
                MyBaril.BarilUpDate(pGameTime, DonkeyKongPosition, DonkeyKongAction);
                if (Math.Abs(deltaPosX) > MyBaril.BarilPosition.Width / 2)
                    DonkeyKongAction = EnumDonkeyKongAction.Seeking;
                if (Math.Abs(deltaPosX) <= 0 && DonkeyKongAction == EnumDonkeyKongAction.Seeking)
                    DonkeyKongAction = EnumDonkeyKongAction.Lifting;
            }

            if (MyBaril != null && (MyBaril.BarilState == EnumBarilState.MoveBack
                                    || MyBaril.BarilState == EnumBarilState.Thrown))
            {
                MyBaril.BarilUpDate(pGameTime, DonkeyKongPosition, DonkeyKongAction, MyDKThrowBarilSprite);
            }
            #endregion

            DonkeyKongPosition = GroundCollision.StickToTheGround(DonkeyKongPosition, ListMapPoints);

            #region Manage movement along x
            switch (DonkeyKongAction)
            {
                case EnumDonkeyKongAction.Standing:
                    DonkeyKongPosition.Width = MyDKStandingSprite.FrameWidth;
                    DonkeyKongPosition.Height = MyDKStandingSprite.FrameHeight;
                    break;
                case EnumDonkeyKongAction.Walking:
                    DonkeyKongPosition.Width = MyDKWalkingSprite.FrameWidth;
                    DonkeyKongPosition.Height = MyDKWalkingSprite.FrameHeight;
                    {
                        DonkeyKongPosition.X = DonkeyKongPosition.X + (int)DonkeyKongDirection * (int)(DKSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds);

                        if (DonkeyKongPosition.X > ListMapPoints[16].X - DonkeyKongPosition.Width / 2)
                            DonkeyKongDirection = EnumSpriteDirection.Left;

                        if (DonkeyKongPosition.X < ListMapPoints[9].X + DonkeyKongPosition.Width / 2)
                            DonkeyKongDirection = EnumSpriteDirection.Right;
                    }
                    break;
                case EnumDonkeyKongAction.Seeking:
                    {
                        if (deltaPosX > 0)
                            DonkeyKongDirection = EnumSpriteDirection.Right;
                        else
                            DonkeyKongDirection = EnumSpriteDirection.Left;

                        DonkeyKongPosition.X = DonkeyKongPosition.X + (int)DonkeyKongDirection * (int)(DKSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds);
                    }
                    break;
                case EnumDonkeyKongAction.Lifting:
                    DonkeyKongPosition.Width = MyDKLiftBarilSprite.FrameWidth;
                    DonkeyKongPosition.Height = MyDKLiftBarilSprite.FrameHeight;
                    if (MyDKLiftBarilSprite.CurrentFrame >= MyDKLiftBarilSprite.FrameNumber)
                    {
                        DonkeyKongAction = EnumDonkeyKongAction.HoldingStand;
                    }
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    elapsedTimeBarilTargeting = elapsedTimeBarilTargeting + (pGameTime.ElapsedGameTime.Milliseconds) / 1000.0d;
                    DonkeyKongPosition.Width = MyDKHoldBarilStandingSprite.FrameWidth;
                    DonkeyKongPosition.Height = MyDKHoldBarilStandingSprite.FrameHeight;
                    DonkeyKongDirection = EnumSpriteDirection.Left;
                    if (elapsedTimeBarilTargeting > 3)
                    {
                        DonkeyKongAction = EnumDonkeyKongAction.Throwing;
                    }
                    break;
                case EnumDonkeyKongAction.HoldingWalk:
                    break;
                case EnumDonkeyKongAction.Throwing:
                    DonkeyKongPosition.Width = MyDKThrowBarilSprite.FrameWidth;
                    DonkeyKongPosition.Height = MyDKThrowBarilSprite.FrameHeight;
                    if (MyDKThrowBarilSprite.CurrentFrame >= MyDKThrowBarilSprite.FrameNumber)
                    {
                        DonkeyKongAction = EnumDonkeyKongAction.Standing;
                    }
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
                case EnumDonkeyKongAction.Seeking:
                    MyDKWalkingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.Lifting:
                    MyDKLiftBarilSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    MyDKHoldBarilStandingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
                    break;
                case EnumDonkeyKongAction.HoldingWalk:
                    //MyDKHoldBarilWalkingSprite.SpriteGeneratorUpdate(pGameTime, DonkeyKongDirection);
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
                case EnumDonkeyKongAction.Seeking:
                    MyDKWalkingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.Lifting:
                    MyDKLiftBarilSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    MyBaril.BarilDraw(pGameTime);
                    break;
                case EnumDonkeyKongAction.HoldingStand:
                    MyDKHoldBarilStandingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
                    break;
                case EnumDonkeyKongAction.HoldingWalk:
                    //MyDKHoldBarilWalkingSprite.SpriteGeneratorDraw(pGameTime, DonkeyKongPosition);
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
        Seeking = 2,
        Lifting = 3,
        HoldingStand = 4,
        HoldingWalk = 5,
        Throwing = 6,
        Running = 7,
        Jumping = 8,
        Falling = 9,
        Hit = 10,
        Celebrate = 11
    }
}