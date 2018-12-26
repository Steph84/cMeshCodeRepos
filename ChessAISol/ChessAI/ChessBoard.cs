using ChessAI.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAI
{
    public class ChessBoard
    {
        private WindowDimension GameWindow { get; set; }
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private int RowNumber { get; set; }
        private int ColumnNumber { get; set; }
        public static int SquareSize { get; set; }
        private static int BoardSize { get; set; }
        public static BoardSquare[,] Board { get; set; }

        private Texture2D DarkSquare { get; set; }
        private Texture2D LightSquare { get; set; }

        public class BoardSquare
        {
            public Rectangle SquareDestination { get; set; }
            public Texture2D SquareTexture { get; set; }
            public Piece Piece { get; set; }
        }

        public ChessBoard(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindow = pGameWindow;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            RowNumber = 8;
            ColumnNumber = 8;

            DarkSquare = Content.Load<Texture2D>("square brown dark_1x");
            LightSquare = Content.Load<Texture2D>("square brown light_1x");

            // we need 8 squares from the standard of 576
            // it means that each square have to be 576/8 = 72, multiply by gameSizeCoef
            SquareSize = (int)((GameWindow.ArrayResolution[0, 3] / RowNumber) * GameWindow.GameSizeCoefficient);
            BoardSize = SquareSize * RowNumber;

            InitializeChessBoard();

            Piece.LoadPieceTextures(Content);
            InitializeChessPieces();
        }

        private void InitializeChessBoard()
        {
            Board = new BoardSquare[RowNumber, ColumnNumber];

            for (int row = 0; row < RowNumber; row++)
            {
                for (int column = 0; column < ColumnNumber; column++)
                {
                    Board[row, column] = new BoardSquare();
                    Board[row, column].SquareDestination = new Rectangle(column * SquareSize, row * SquareSize, SquareSize, SquareSize);
                    if (IsEven(row + column))
                    {
                        Board[row, column].SquareTexture = LightSquare;
                    }
                    else
                    {
                        Board[row, column].SquareTexture = DarkSquare;
                    }
                    Board[row, column].Piece = null;
                }
            }
        }

        private void InitializeChessPieces()
        {
            foreach (Piece.Color color in (Piece.Color[])Enum.GetValues(typeof(Piece.Color)))
            {
                for (int i = 1; i < 9; i++)
                {
                    if (i == 1)
                    {
                        // create King and Queen
                        Piece tempQ = new Piece(color, Piece.Type.Queen, i);
                        Piece tempK = new Piece(color, Piece.Type.King, i);
                        Board[tempQ.Position.Y, tempQ.Position.X].Piece = tempQ;
                        Board[tempK.Position.Y, tempK.Position.X].Piece = tempK;
                    }

                    if (i < 3)
                    {
                        // create Rooks, Bishops and Knights
                        Piece tempR = new Piece(color, Piece.Type.Rook, i);
                        Piece tempB = new Piece(color, Piece.Type.Bishop, i);
                        Piece tempKn = new Piece(color, Piece.Type.Knight, i);
                        Board[tempR.Position.Y, tempR.Position.X].Piece = tempR;
                        Board[tempB.Position.Y, tempB.Position.X].Piece = tempB;
                        Board[tempKn.Position.Y, tempKn.Position.X].Piece = tempKn;
                    }

                    // pawns
                    Piece tempP = new Piece(color, Piece.Type.Pawn, i);
                    Board[tempP.Position.Y, tempP.Position.X].Piece = tempP;
                }
            }
        }

        public void ChessBoardDraw(GameTime pGameTime)
        {
            for (int row = 0; row < RowNumber; row++)
            {
                for (int column = 0; column < ColumnNumber; column++)
                {
                    var tempSquare = Board[row, column];
                    SpriteBatch.Draw(tempSquare.SquareTexture, tempSquare.SquareDestination, null, Color.White);
                    if(tempSquare.Piece != null)
                    {
                        SpriteBatch.Draw(tempSquare.Piece.PieceTexture, tempSquare.SquareDestination, null, Color.White);
                    }
                }
            }
            //DebugToolBox.ShowLine(Content, SpriteBatch, DirectionMoving.ToString(), new Vector2(Position.X, Position.Y));
        }

        // if the number is even (pair)
        private bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        public static BoardSquare InWhichSquareAreWe(MouseState currentState)
        {
            if(currentState.X < BoardSize && currentState.X > 0 && currentState.Y < BoardSize && currentState.Y > 0)
            {
                int tempRow = (int)Math.Floor((decimal)(currentState.Y / SquareSize));
                int tempCol = (int)Math.Floor((decimal)(currentState.X / SquareSize));

                return Board[tempRow, tempCol];
            }
            else
            {
                return null;
            }
        }
    }
}