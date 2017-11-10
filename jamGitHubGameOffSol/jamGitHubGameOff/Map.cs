using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace jamGitHubGameOff
{
    public class Map
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        
        // parameters for the pictures draw
        Texture2D DKCMapLayer1Pic, DKCMapLayer2Pic, DKCMapLayer3Pic;
        Rectangle DKCMapLayer1Target, DKCMapLayer2Target, DKCMapLayer3Target;
        List<int> ListDKCMapLayer1Pos = new List<int>();
        List<int> ListDKCMapLayer2Pos = new List<int>();

        #region Constructor Map
        public Map(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;
            
            // layer 1 horizon
            DKCMapLayer1Pic = Content.Load<Texture2D>("jungle-sky-2x");
            DKCMapLayer1Target = new Rectangle(0, 0, DKCMapLayer1Pic.Width, DKCMapLayer1Pic.Height);
            MapComputePositionPics(DKCMapLayer1Pic, ListDKCMapLayer1Pos);
            
            // layer 2 trees in the shadow
            DKCMapLayer2Pic = Content.Load<Texture2D>("jungle-trees-fill-dark-2");
            DKCMapLayer2Target = new Rectangle(0, 0, DKCMapLayer2Pic.Width, DKCMapLayer2Pic.Height);
            MapComputePositionPics(DKCMapLayer2Pic, ListDKCMapLayer2Pos);

            // Layer 3 platform part
            DKCMapLayer3Pic = Content.Load<Texture2D>("DKCBarrelCannonCanyonCut");
            DKCMapLayer3Target = new Rectangle(0, 0, DKCMapLayer3Pic.Width, DKCMapLayer3Pic.Height);
        }
        #endregion

        #region MapDraw
        public void MapDraw(GameTime pGameTime)
        {
            for (int i = 0; i < ListDKCMapLayer1Pos.Count; i++)
            {
                DKCMapLayer1Target.X = ListDKCMapLayer1Pos[i];
                SpriteBatch.Draw(DKCMapLayer1Pic, DKCMapLayer1Target, Color.White);
            }

            for (int i = 0; i < ListDKCMapLayer2Pos.Count; i++)
            {
                DKCMapLayer2Target.X = ListDKCMapLayer2Pos[i];
                SpriteBatch.Draw(DKCMapLayer2Pic, DKCMapLayer2Target, Color.White);
            }
            
            SpriteBatch.Draw(DKCMapLayer3Pic, DKCMapLayer3Target, Color.White);
        }
        #endregion

        #region Method to manage the duplication of the different layers
        private void MapComputePositionPics(Texture2D pPic, List<int> pListPosX)
        {
            if (pPic.Width < GameWindowWidth)
            {
                int sub = pPic.Width - GameWindowWidth;
                int iteration = 0;

                while (sub < pPic.Width)
                {
                    pListPosX.Add(iteration);
                    iteration += pPic.Width;
                    sub += pPic.Width;
                }
            }
        }
        #endregion
    }
}
