using ChessAI.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private int SquareSize { get; set; }
        public BoardSquare[,] Board { get; set; }

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

        // TODO
        private void InitializeChessPieces()
        {
            foreach (Piece.Color color in (Piece.Color[])Enum.GetValues(typeof(Piece.Color)))
            {
                for (int i = 1; i < 7; i++)
                {
                    Piece tempP = null;
                    if (i == 1)
                    {
                        // queen and king
                    }

                    if (i < 3)
                    {
                        // rook, bishop and knight
                    }

                    // pawns
                    tempP = new Piece(color, Piece.Type.Pawn, i);

                    // add to the chessboard

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
                }
            }
            //DebugToolBox.ShowLine(Content, SpriteBatch, DirectionMoving.ToString(), new Vector2(Position.X, Position.Y));
        }

        // if the number is even (pair)
        private bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }
}

//MapTextureGrid[row, column]