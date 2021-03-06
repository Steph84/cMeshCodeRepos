﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace jamGitHubGameOff
{
    public class GameClass
    {
        int GameWindowWidth;
        int GameWindowHeight;
        ContentManager Content;
        SpriteBatch SpriteBatch;
        Map MyMap;

        public GameClass(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch, GraphicsDevice pGraphicsDevice)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            MyMap = new Map(pGameWindowSize, Content, SpriteBatch, pGraphicsDevice);
        }

        public void GameClassUpdate(GameTime pGameTime)
        {
            MyMap.MapUpdate(pGameTime);
        }

        public void GameClassDraw(GameTime pGameTime)
        {
            MyMap.MapDraw(pGameTime);
        }

    }
}
