using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        SpriteEffects PlayerSpriteDirection = SpriteEffects.None;
        EnumSpriteDirection PlayerDirection = EnumSpriteDirection.Right;
        double PlayerCurrentFrame;
        public EnumPlayerState PlayerState { get; set; }

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
            MyPlayerSprite.SpeedAnimation = 8.0d;
            PlayerOrigin = new Vector2(MyPlayerSprite.FrameWidth / 2, MyPlayerSprite.FrameHeight / 2);
            PlayerCurrentFrame = 0;

            PlayerPosition = new Rectangle(200, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);
            PlayerState = EnumPlayerState.Standing;
        }

        public void PlayerUpDate(GameTime pGameTime)
        {

            PlayerPosition = GroundCollision.StickToTheGround(PlayerPosition, ListMapPoints);
            

            switch (PlayerState)
            {
                case EnumPlayerState.Standing:
                    PlayerCurrentFrame = 0;
                    break;
                case EnumPlayerState.Walking:
                    PlayerCurrentFrame = 0;
                    break;
                case EnumPlayerState.Slashing:
                    MyPlayerSprite.SpriteGeneratorUpdate(pGameTime, PlayerDirection);
                    //PlayerCurrentFrame = PlayerCurrentFrame + (MyPlayerSprite.SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);
                    if(MyPlayerSprite.CurrentFrame >= MyPlayerSprite.FrameNumber)
                    {
                        PlayerState = EnumPlayerState.Standing;
                    }
                    break;
                default:
                    break;
            }

            MyPlayerSprite.SourceQuad = new Rectangle(0, 0, MyPlayerSprite.FrameWidth, MyPlayerSprite.FrameHeight);

        }

        public void PlayerDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(PlayerPic, PlayerPosition, MyPlayerSprite.SourceQuad, Color.White, 0, PlayerOrigin, PlayerSpriteDirection, 0);
        }

        public enum EnumPlayerState
        {
            Standing = 1,
            Walking = 2,
            Slashing = 3
        }
    }
}
