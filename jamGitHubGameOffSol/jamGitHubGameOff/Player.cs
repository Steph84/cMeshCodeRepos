using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace jamGitHubGameOff
{
    public class Player
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        List<Vector2> ListMapPoints;

        SpriteGenerator MyPlayerSprite;
        Texture2D PlayerPic;
        int PlayerFrameNumber;

        public Rectangle PlayerPosition { get; set; }
        Vector2 PlayerOrigin;
        EnumSpriteDirection PlayerDirection = EnumSpriteDirection.Right;
        double PlayerCurrentFrame;
        public EnumPlayerState PlayerState { get; set; }

        double PlayerSpeedWalking;

        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        public Player(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;

            PlayerPic = Content.Load<Texture2D>("jasonSlashing");
            PlayerFrameNumber = 7;
            MyPlayerSprite = new SpriteGenerator(SpriteBatch, PlayerPic, PlayerFrameNumber, false, false);

            MyPlayerSprite.SourceQuad = new Rectangle(0, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);
            MyPlayerSprite.SpeedAnimation = 40.0d;
            PlayerOrigin = new Vector2(MyPlayerSprite.FrameWidth / 2, MyPlayerSprite.FrameHeight / 2);
            PlayerCurrentFrame = 0;

            PlayerPosition = new Rectangle(200, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);
            PlayerState = EnumPlayerState.Standing;

            PlayerSpeedWalking = 0.2;
        }

        public void PlayerUpDate(GameTime pGameTime)
        {
            PlayerPosition = GroundCollision.StickToTheGround(PlayerPosition, ListMapPoints);

            newState = Keyboard.GetState();

            // avoid the player to be stuck in a side part 1/2
            int tempOldX = PlayerPosition.X;

            if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                PlayerState = EnumPlayerState.Walking;
                PlayerPosition = new Rectangle(PlayerPosition.X - (int)(PlayerSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds),
                                                PlayerPosition.Y, PlayerPosition.Width, PlayerPosition.Height);
            }
            else if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                PlayerState = EnumPlayerState.Walking;
                PlayerPosition = new Rectangle(PlayerPosition.X + (int)(PlayerSpeedWalking * pGameTime.ElapsedGameTime.Milliseconds),
                                                PlayerPosition.Y, PlayerPosition.Width, PlayerPosition.Height);
            }

            // avoid the player to be stuck in a side part 2/2
            if (PlayerPosition.X < 0 || PlayerPosition.X > ListMapPoints[2].X)
                PlayerPosition = new Rectangle(tempOldX, PlayerPosition.Y, PlayerPosition.Width, PlayerPosition.Height);
            
            if (newState.IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
            {
                PlayerState = EnumPlayerState.Slashing;
            }

            switch (PlayerState)
            {
                case EnumPlayerState.Standing:
                    MyPlayerSprite.SourceQuad = new Rectangle(0, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);
                    PlayerCurrentFrame = 0;
                    break;
                case EnumPlayerState.Walking:
                    MyPlayerSprite.SourceQuad = new Rectangle(0, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);
                    PlayerCurrentFrame = 0;
                    break;
                case EnumPlayerState.Slashing:
                    MyPlayerSprite.SpriteGeneratorUpdate(pGameTime, PlayerDirection);
                    if(MyPlayerSprite.CurrentFrame < 0)
                    {
                        PlayerState = EnumPlayerState.Standing;
                        MyPlayerSprite.CurrentFrame = 0;
                        MyPlayerSprite.ParseQuads = "forth";
                    }
                    break;
                default:
                    break;
            }
            oldState = newState;
        }

        public void PlayerDraw(GameTime pGameTime)
        {
            MyPlayerSprite.SpriteGeneratorDraw(pGameTime, PlayerPosition);
        }

        public enum EnumPlayerState
        {
            Standing = 1,
            Walking = 2,
            Slashing = 3
        }
    }
}
