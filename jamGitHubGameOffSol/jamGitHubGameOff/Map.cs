using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace jamGitHubGameOff
{
    public class Map
    {
        DonkeyKong MyDonkeyKong;

        // position of all points for the map to walk on
        public List<Vector2> ListMapPoints =
            new List<Vector2> {
                                    new Vector2(0, 210),
                                    new Vector2(100, 210),
                                    new Vector2(250, 250),
                                    new Vector2(270, 305),
                                    new Vector2(340, 305),
                                    new Vector2(355, 315),
                                    new Vector2(405, 380),
                                    new Vector2(445, 370),
                                    new Vector2(475, 380),
                                    new Vector2(490, 435),
                                    new Vector2(535, 435),
                                    new Vector2(655, 405),
                                    new Vector2(735, 405),
                                    new Vector2(795, 420),
                                    new Vector2(860, 405),
                                    new Vector2(1150, 405)
                              };
        List<Rectangle> ListRectanglePoints = new List<Rectangle>();
        Texture2D segmentPoint;
        // for the platform
        //new Vector2(760, 345),
        //new Vector2(795, 340),
        //new Vector2(830, 345)

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
        public Map(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, GraphicsDevice pGraphicsDevice)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            #region to draw segments points
            foreach (var item in ListMapPoints)
            {
                ListRectanglePoints.Add(new Rectangle((int)item.X, (int)item.Y, 2, 2));
            }
            segmentPoint = new Texture2D(pGraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            segmentPoint.SetData(new[] { Color.White });
            #endregion

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

            MyDonkeyKong = new DonkeyKong(Content, SpriteBatch);
        }
        #endregion

        #region MapUpdate
        public void MapUpdate(GameTime pGameTime)
        {
            MyDonkeyKong.DonkeyKongUpDate(pGameTime);
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

            // draw the segments points
            foreach(var item in ListRectanglePoints)
                SpriteBatch.Draw(segmentPoint, item, Color.White);

            MyDonkeyKong.DonkeyKongDraw(pGameTime);
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
