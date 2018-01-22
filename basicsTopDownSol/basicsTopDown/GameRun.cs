using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicsTopDown
{
    public class GameRun
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        ContentManager Content;
        SpriteBatch SpriteBatch;

        private MapGenFolder.MapGenerator MyMap { get; set; }

        public GameRun(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            MyMap = new MapGenFolder.MapGenerator(Content, SpriteBatch, "testMapBitMap", "tileSetMapGen01", 32, 32);
        }

        public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {

            return pMyState;
        }

        public void GameRunDraw(GameTime pGameTime)
        {
            MyMap.MapDraw(pGameTime);
        }
    }
}
