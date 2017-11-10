using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jamGitHubGameOff
{
    public class GameClass
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        Map MyMap;

        public GameClass(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            MyMap = new Map(pGameWindowSize, Content, SpriteBatch);
        }

        public void GameClassUpdate(GameTime pGameTime)
        {

        }

        public void GameClassDraw(GameTime pGameTime)
        {
            MyMap.MapDraw(pGameTime);
        }

    }
}
