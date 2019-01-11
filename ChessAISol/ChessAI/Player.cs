using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static ChessAI.UtilFolder.MouseManager;

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

        //MouseManager myMouse = new MouseManager();
        MouseState oldState = new MouseState();
        MouseState newState = new MouseState();
        EnumMouse temp = EnumMouse.NoAction;

        public Player(SpriteBatch pSpriteBatch)
        {
            SpriteBatch = pSpriteBatch;
            Piece = null;
        }

        #region Methods
        private void MovePiece(ChessBoard.BoardSquare tempSquare)
        {
            Piece.NbMove += 1;
            if (Piece.PieceType == Piece.Type.Pawn && Piece.Speed == 2)
            {
                Piece.Speed = 1;
            }
            tempSquare.Piece = Piece;
            Piece = null;
            ChessBoard.SearchPossibleMoves();
        }
        #endregion

        #region Game Methods
        public void PlayerUpdate(GameTime pGameTime)
        {
            temp = EnumMouse.NoAction;
            newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Released)
            {
                temp = EnumMouse.Press;
                ChessBoard.BoardSquare tempSquare = ChessBoard.InWhichSquareAreWeByPixel(newState);

                // if we are on the chess board
                if (tempSquare != null)
                {
                    // if I don't have a piece in my hand
                    if (Piece == null)
                    {
                        if (tempSquare.Piece != null && tempSquare.Piece.PieceColor == Piece.PieceColors.White)
                        {
                            Piece = tempSquare.Piece;
                            tempSquare.Piece = null;
                        }
                    }
                    // if I do have a piece in my hand
                    else
                    {
                        // check if the move is correct
                        if (Piece.ListPossibleMoves.Contains(new Point(tempSquare.SquareCoordinate.X, tempSquare.SquareCoordinate.Y)))
                        {
                            // if we are on an empty square
                            if (tempSquare.Piece == null)
                            {
                                MovePiece(tempSquare);
                            }
                            // if we are on square with a piece
                            else
                            {
                                switch (tempSquare.Piece.PieceColor)
                                {
                                    case Piece.PieceColors.Black:
                                        ChessBoard.OffBoardPieces.Add(tempSquare.Piece);
                                        MovePiece(tempSquare);
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
            }
            else if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Hold;
            }
            else if (newState.LeftButton != ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Up;
            }

            oldState = newState;
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
