using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace ChessAI
{
    public class Player
    {
        #region Attributes
        private SpriteBatch SpriteBatch { get; set; }
        public int MouseX { get; set; }
        public int MouseY { get; set; }
        public Piece Piece { get; set; }
        #endregion

        MouseState oldState = new MouseState();
        MouseState newState = new MouseState();

        public Player(SpriteBatch pSpriteBatch)
        {
            SpriteBatch = pSpriteBatch;
            Piece = null;
        }

        #region Methods
        private GameRun.PlayerTurn MovePiece(ChessBoard.BoardSquare tempSquare, GameRun.PlayerTurn pTurn)
        {
            Piece.NbMove += 1;
            if (Piece.PieceType == Piece.PieceTypes.Pawn && Piece.Speed == 2)
            {
                Piece.Speed = 1;
            }
            Piece.Position = new Point(tempSquare.SquareCoordinate.X, tempSquare.SquareCoordinate.Y);
            tempSquare.Piece = Piece;
            Piece = null;
            ChessBoard.SearchPossibleMoves();

            switch (pTurn)
            {
                case GameRun.PlayerTurn.Computer:
                    return GameRun.PlayerTurn.Player;
                case GameRun.PlayerTurn.Player:
                    return GameRun.PlayerTurn.Computer;
                default:
                    return GameRun.PlayerTurn.None;
            }
        }
        #endregion

        #region Game Methods
        public GameRun.PlayerTurn PlayerUpdate(GameTime pGameTime, GameRun.PlayerTurn pTurn)
        {
            newState = Mouse.GetState();

            // if we press right clic, the piece come back to the original position
            // still the player turn and no more move consumption
            if (newState.RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed)
            {
                if(Piece != null)
                {
                    ChessBoard.Board.Where(x => x.Row == Piece.Position.Y && x.Column == Piece.Position.X).Single().Piece = Piece;
                    Piece = null;
                }
            }

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
            {
                ChessBoard.BoardSquare tempSquare = ChessBoard.InWhichSquareAreWeByPixel(newState);

                // if the player don't have a piece in hand
                if (Piece == null)
                {
                    if (tempSquare != null && tempSquare.Piece != null &&
                        tempSquare.Piece.PieceColor == Piece.PieceColors.White &&
                        tempSquare.Piece.ListPossibleMoves.Count > 0)
                    {
                        Piece = tempSquare.Piece;
                        tempSquare.Piece = null;
                    }
                }
                // if the player have a piece in hand
                else
                {
                    if (tempSquare != null && Piece.ListPossibleMoves.Contains(new Point(tempSquare.SquareCoordinate.X, tempSquare.SquareCoordinate.Y)))
                    {
                        if (tempSquare.Piece == null)
                        {
                            pTurn = MovePiece(tempSquare, pTurn);
                        }
                        // if we are on square with a piece
                        else
                        {
                            switch (tempSquare.Piece.PieceColor)
                            {
                                case Piece.PieceColors.Black:
                                    ChessBoard.OffBoardPieces.Add(tempSquare.Piece);
                                    pTurn = MovePiece(tempSquare, pTurn);
                                    break;
                                case Piece.PieceColors.White:
                                    // nothing to do
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            oldState = newState;

            return pTurn;
        }

        public void PlayerDraw(GameTime pGameTime)
        {
            if (Piece != null)
            {
                SpriteBatch.Draw(Piece.PieceTexture, new Rectangle(newState.X - (ChessBoard.SquareSize / 2), newState.Y - (ChessBoard.SquareSize / 2), ChessBoard.SquareSize, ChessBoard.SquareSize), null, Color.White);
            }
        }
        #endregion
    }
}
