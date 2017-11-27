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

        public Rectangle PlayerPosition { get; set; }

        public Player(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, List<Vector2> pListMapPoints)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            ListMapPoints = pListMapPoints;

            PlayerPosition = new Rectangle(5, 5, 32, 32);

        }
    }
}
