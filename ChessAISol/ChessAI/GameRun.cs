using ChessAI.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChessAI
{
    public class GameRun
    {
        private int GameWindowWidth { get; set; }
        private int GameWindowHeight { get; set; }
        private double GameSizeCoefficient { get; set; }

        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        public ChessBoard ChessBoard { get; set; }

        public GameRun(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindow.GameWindowWidth;
            GameWindowHeight = pGameWindow.GameWindowHeight;
            GameSizeCoefficient = pGameWindow.GameSizeCoefficient;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            ChessBoard = new ChessBoard(pGameWindow, pContent, pSpriteBatch);
        }

        public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {


            return pMyState;
        }

        public void GameRunDraw(GameTime pGameTime)
        {
            ChessBoard.ChessBoardDraw(pGameTime);
        }
    }
}
