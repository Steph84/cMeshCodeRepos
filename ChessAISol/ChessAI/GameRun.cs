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
        public Player Player { get; set; }
        public Computer Computer { get; set; }
        public PlayerTurn Turn { get; set; }

        public enum PlayerTurn
        {
            Computer = 0,
            Player = 1,
            None = 2
        }
        
        public GameRun(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindow.GameWindowWidth;
            GameWindowHeight = pGameWindow.GameWindowHeight;
            GameSizeCoefficient = pGameWindow.GameSizeCoefficient;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            ChessBoard = new ChessBoard(pGameWindow, pContent, pSpriteBatch);
            Player = new Player(pSpriteBatch);
            Computer = new Computer(pContent, pSpriteBatch);
            Turn = PlayerTurn.Player;
        }

        public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            switch (Turn)
            {
                case PlayerTurn.Computer:
                    Turn = Computer.ComputerUpdate(pGameTime, Turn);
                    break;
                case PlayerTurn.Player:
                    Turn = Player.PlayerUpdate(pGameTime, Turn);
                    break;
                default:
                    break;
            }

            return pMyState;
        }

        public void GameRunDraw(GameTime pGameTime)
        {
            ChessBoard.ChessBoardDraw(pGameTime);
            Player.PlayerDraw(pGameTime);
            Computer.ComputerDraw(pGameTime);

            // game over
            if(Turn == PlayerTurn.None)
            {
                DebugToolBox.ShowLine(Content, SpriteBatch, "GAME OVER", new Vector2(GameWindowWidth/2, GameWindowHeight/2));
            }
        }
    }
}
