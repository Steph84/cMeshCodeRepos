using basicsTopDown.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace basicsTopDown
{
    public class GameRun
    {
        private int GameWindowWidth { get; set; }
        private int GameWindowHeight { get; set; }
        private double GameSizeCoefficient { get; set; }

        ContentManager Content;
        SpriteBatch SpriteBatch;

        private MapGenFolder.MapGenerator MyMap { get; set; }
        private CharacterFolder.Player MyLink { get; set; }
        private List<SpriteObject> SpritesList { get ; set; }

        public GameRun(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindow.GameWindowWidth;
            GameWindowHeight = pGameWindow.GameWindowHeight;
            GameSizeCoefficient = pGameWindow.GameSizeCoefficient;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            MyMap = new MapGenFolder.MapGenerator(Content, SpriteBatch, "testMapBitMap", "wallsTopDownTileSet", 96, 96, GameSizeCoefficient);
            MyLink = new CharacterFolder.Player(Content, SpriteBatch, new Rectangle(100, 100, 0, 0), "link");
            SpritesList = new List<SpriteObject>();

        }

        public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            bool IsColliding = false;
            if(SpriteObject.SpriteObjectCollision(pGameTime, MyMap, MyLink) != null)
            {
                IsColliding = true;
            }
            
            MyLink.PlayerControl(pGameTime, IsColliding);
            
            return pMyState;
        }

        public void GameRunDraw(GameTime pGameTime)
        {
            MyMap.MapDraw(pGameTime);
            MyLink.CharacterDraw(pGameTime);
        }
    }
}
