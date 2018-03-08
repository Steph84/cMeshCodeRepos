using basicsTopDown.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace basicsTopDown
{
    public class GameRun
    {
        private int GameWindowWidth { get; set; }
        private int GameWindowHeight { get; set; }
        private double GameSizeCoefficient { get; set; }

        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private MapFolder.Map MyMap { get; set; }
        private CharacterFolder.Player MyLink { get; set; }

        public GameRun(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindow.GameWindowWidth;
            GameWindowHeight = pGameWindow.GameWindowHeight;
            GameSizeCoefficient = pGameWindow.GameSizeCoefficient;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            MyMap = new MapFolder.Map(Content, SpriteBatch, "testMapBitMap", "wallsTopDownTileSet", 96, 96, GameSizeCoefficient);
            //MyMap = new MapFolder.Map(Content, SpriteBatch, "testMapBitMap", "tileSetMapGen01", 32, 32, GameSizeCoefficient);
            MyLink = new CharacterFolder.Player(Content, SpriteBatch, new Rectangle(100, 100, 0, 0), "linkWalkingAnimation", new Rectangle(0, 0, 16, 24), GameSizeCoefficient, MyMap);
        }

        public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            MyLink.SpriteUpdate(pGameTime, MyMap);
            
            return pMyState;
        }

        public void GameRunDraw(GameTime pGameTime)
        {
            MyMap.MapDraw(pGameTime);
            MyLink.SpriteDraw(pGameTime);
        }
    }
}
