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

        Texture2D DKStandingPic;
        int DKStandingFrameWidth = 42;
        int DKStandingFrameHeight = 40;
        double currentFrame = 0;
        string parseQuads = "forth";
        Rectangle donkeyQuad;
        Rectangle donkeyPos;
        double speedAnimation = 8.0d;
        int speedFalling = 200;
        int speedWalking = 100;
        string donkeyDir = "right";
        int coefDir = 1;

        public DonkeyKong(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;
            DKStandingPic = Content.Load<Texture2D>("DKCStanding"); // 11 42x40
            donkeyQuad = new Rectangle(0, 0, DKStandingFrameWidth, DKStandingFrameHeight);
            donkeyPos = new Rectangle(1000, 200, DKStandingFrameWidth, DKStandingFrameHeight);
        }

        public void DonkeyKongUpDate(GameTime pGameTime)
        {
            #region Manage DK animation
            if(parseQuads == "forth")
                currentFrame = currentFrame + (speedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

            if (currentFrame > 10)
            {
                currentFrame = 10;
                parseQuads = "back";
            }

            if(parseQuads == "back")
                currentFrame = currentFrame - (speedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

            if (currentFrame < 0)
            {
                currentFrame = 0;
                parseQuads = "forth";
            }

            donkeyQuad.X = (int)Math.Floor(currentFrame) * DKStandingFrameWidth;
            #endregion

            #region Manage collision on the ground
            // find the 2 points around DK
            Vector2 leftBoundary = ListMapPoints.Where(x => donkeyPos.X >= x.X).LastOrDefault();
            if (leftBoundary == null)
                leftBoundary = ListMapPoints.First();
            Vector2 rightBoundary = ListMapPoints.Where(x => donkeyPos.X < x.X).FirstOrDefault();
            if (rightBoundary == null)
                rightBoundary = ListMapPoints.Last();

            // compute equation coeff
            double a = (rightBoundary.Y - leftBoundary.Y) / (rightBoundary.X - leftBoundary.X);
            double b = leftBoundary.Y - leftBoundary.X * a;

            // modify DK y
            if ((donkeyPos.Y + donkeyPos.Height) < (donkeyPos.X * a + b))
                donkeyPos.Y = donkeyPos.Y + (speedFalling * pGameTime.ElapsedGameTime.Milliseconds / 1000);
            else
                donkeyPos.Y = (int)(donkeyPos.X * a + b - donkeyPos.Height);
            #endregion

            #region Manage movement along x
            if (donkeyDir == "right")
                coefDir = 1;

            if(donkeyDir == "left")
                coefDir = -1;

            donkeyPos.X = donkeyPos.X + coefDir * (speedWalking * pGameTime.ElapsedGameTime.Milliseconds / 1000);

            if (donkeyPos.X > GameWindowWidth)
                donkeyDir = "left";
            
            if (donkeyPos.X < 0)
                donkeyDir = "right";

            #endregion
        }

        public void DonkeyKongDraw(GameTime pGameTime)
        {
            //SpriteBatch.Draw(DKStandingPic, donkeyPos, donkeyQuad, Color.White);
            // TODO retournement ne fonctionne pas
            SpriteBatch.Draw(DKStandingPic, null, donkeyPos, donkeyQuad, null, 0,
                             new Vector2((float)coefDir, 1), Color.White, SpriteEffects.None, 0);
        }
    }
}
