using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace jamGitHubGameOff
{
    public class DonkeyKong
    {
        ContentManager Content;
        SpriteBatch SpriteBatch;

        Texture2D DKStandingPic;
        int DKStandingFrameWidth = 42;
        int DKStandingFrameHeight = 40;
        double currentFrame = 0;
        string parseQuads = "forth";

        public DonkeyKong(ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            DKStandingPic = Content.Load<Texture2D>("DKCStanding"); // 11 42x40
        }

        public void DonkeyKongUpDate(GameTime pGameTime)
        {
            if(parseQuads == "forth")
                currentFrame = currentFrame + (8.0d * (double)pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

            if (currentFrame > 10)
            {
                currentFrame = 10;
                parseQuads = "back";
            }

            if(parseQuads == "back")
                currentFrame = currentFrame - (10.0d * (double)pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

            if (currentFrame < 0)
            {
                currentFrame = 0;
                parseQuads = "forth";
            }
        }

        public void DonkeyKongDraw(GameTime pGameTime)
        {
            Rectangle donkeyQuad = new Rectangle((int)Math.Floor(currentFrame) * DKStandingFrameWidth, 0, DKStandingFrameWidth, DKStandingFrameHeight);

            SpriteBatch.Draw(DKStandingPic, new Rectangle(0, 0, 42, 40), donkeyQuad, Color.White);
        }
    }
}
