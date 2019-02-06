using ChessAI.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessAI
{
    public class ChessBoard
    {
        #region Attributes
        private WindowDimension GameWindow { get; set; }
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        public static int BoardSize { get; set; }
        public static int RowNumber { get; set; }
        public static int ColumnNumber { get; set; }
        public int SquareNumbers { get; set; }
        public static int SquareSize { get; set; }
        public static List<BoardSquare> Board { get; set; }
        public static List<Piece> OffBoardPieces { get; set; }
        public static List<PossibleMove> ListPossibleBlackMoves { get; set; }
        public static List<PossibleMove> ListPossibleWhiteMoves { get; set; }

        private Texture2D DarkSquare { get; set; }
        private Texture2D LightSquare { get; set; }
        #endregion

        public class BoardSquare
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public Rectangle SquareDestination { get; set; }
            public Point SquareCoordinate { get; set; }
            public Texture2D SquareTexture { get; set; }
            public Piece Piece { get; set; }
        }

        public class PossibleMove
        {
            public PieceLight Piece { get; set; }
            public Point From { get; set; }
            public Point To { get; set; }
            public bool WillEat { get; set; }
            public bool WillBeEaten { get; set; }
            public double Density { get; set; }
            public double Rate { get; set; }
        }

        public class PieceLight
        {
            public Piece.PieceColors PieceColor { get; set; }
            public Piece.PieceTypes PieceType { get; set; }
        }

        public ChessBoard(WindowDimension pGameWindow, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindow = pGameWindow;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            RowNumber = 8;
            ColumnNumber = 8;
            SquareNumbers = RowNumber * ColumnNumber;
            OffBoardPieces = new List<Piece>();

            ListPossibleBlackMoves = new List<PossibleMove>();
            ListPossibleWhiteMoves = new List<PossibleMove>();

            DarkSquare = Content.Load<Texture2D>("square brown dark_1x");
            LightSquare = Content.Load<Texture2D>("square brown light_1x");

            // we need 8 squares from the standard of 576
            // it means that each square have to be 576/8 = 72, multiply by gameSizeCoef
            SquareSize = (int)((GameWindow.ArrayResolution[0, 3] / RowNumber) * GameWindow.GameSizeCoefficient);
            BoardSize = SquareSize * RowNumber;

            InitializeChessBoard();

            Piece.LoadPieceTextures(Content);
            InitializeChessPieces();

            SearchPossibleMoves();
        }

        #region Methods
        public static void SearchPossibleMoves()
        {
            foreach (BoardSquare sqrToProbe in Board.Where(x => x.Piece != null))
            {
                sqrToProbe.Piece.ListPossibleMoves = new List<Point>();

                switch (sqrToProbe.Piece.PieceType)
                {
                    #region PAWN
                    case Piece.PieceTypes.Pawn:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                #region standard move
                                if (dir == Piece.EnumDirection.North || dir == Piece.EnumDirection.South)
                                {
                                    for (int i = 1; i <= sqrToProbe.Piece.Speed; i++)
                                    {
                                        Point tempCoord = new Point();

                                        switch (sqrToProbe.Piece.PieceColor)
                                        {
                                            case Piece.PieceColors.Black:
                                                {
                                                    if (dir == Piece.EnumDirection.South)
                                                    {
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y + i);
                                                    }
                                                }
                                                break;
                                            case Piece.PieceColors.White:
                                                {
                                                    if (dir == Piece.EnumDirection.North)
                                                    {
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y - i);
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }

                                        BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                        if (item == null)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (item.Piece == null)
                                            {
                                                sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region eat move
                                else
                                {
                                    Point tempCoord = new Point();

                                    switch (sqrToProbe.Piece.PieceColor)
                                    {
                                        case Piece.PieceColors.Black:
                                            {
                                                switch (dir)
                                                {
                                                    case Piece.EnumDirection.SouthEast:
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y + 1);
                                                        break;
                                                    case Piece.EnumDirection.SouthWest:
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y + 1);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            break;
                                        case Piece.PieceColors.White:
                                            {
                                                switch (dir)
                                                {
                                                    case Piece.EnumDirection.NorthEast:
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y - 1);
                                                        break;
                                                    case Piece.EnumDirection.NorthWest:
                                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y - 1);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }

                                    BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                    if (item == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (item.Piece == null)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                            {
                                                sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        break;
                    #endregion
                    #region ROOK
                    case Piece.PieceTypes.Rook:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                for (int i = 1; i <= sqrToProbe.Piece.Speed; i++)
                                {
                                    Point tempCoord = new Point();
                                    switch (dir)
                                    {
                                        case Piece.EnumDirection.North:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        case Piece.EnumDirection.East:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y);
                                            break;
                                        case Piece.EnumDirection.South:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.West:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y);
                                            break;
                                        default:
                                            break;
                                    }

                                    BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                    if (item == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (item.Piece == null)
                                        {
                                            sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                        }
                                        else
                                        {
                                            if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                            {
                                                sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region KNIGHT
                    case Piece.PieceTypes.Knight:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                Point tempCoord = new Point();
                                switch (dir)
                                {
                                    case Piece.EnumDirection.NorthNorthEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y - 2);
                                        break;
                                    case Piece.EnumDirection.NorthEastEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 2, sqrToProbe.Piece.Position.Y - 1);
                                        break;
                                    case Piece.EnumDirection.SouthEastEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 2, sqrToProbe.Piece.Position.Y + 1);
                                        break;
                                    case Piece.EnumDirection.SouthSouthEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y + 2);
                                        break;
                                    case Piece.EnumDirection.SouthSouthWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y + 2);
                                        break;
                                    case Piece.EnumDirection.SouthWestWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 2, sqrToProbe.Piece.Position.Y + 1);
                                        break;
                                    case Piece.EnumDirection.NorthWestWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 2, sqrToProbe.Piece.Position.Y - 1);
                                        break;
                                    case Piece.EnumDirection.NorthNorthWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y - 2);
                                        break;
                                    default:
                                        break;
                                }

                                BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                if (item == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (item.Piece == null)
                                    {
                                        sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                    }
                                    else
                                    {
                                        if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                        {
                                            sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                        }
                                        continue;
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region BISHOP
                    case Piece.PieceTypes.Bishop:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                for (int i = 1; i <= sqrToProbe.Piece.Speed; i++)
                                {
                                    Point tempCoord = new Point();
                                    switch (dir)
                                    {
                                        case Piece.EnumDirection.NorthEast:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        case Piece.EnumDirection.SouthEast:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.SouthWest:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.NorthWest:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        default:
                                            break;
                                    }

                                    BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                    if (item == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (item.Piece == null)
                                        {
                                            sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                        }
                                        else
                                        {
                                            if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                            {
                                                sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region QUEEN
                    case Piece.PieceTypes.Queen:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                for (int i = 1; i <= sqrToProbe.Piece.Speed; i++)
                                {
                                    Point tempCoord = new Point();
                                    switch (dir)
                                    {
                                        case Piece.EnumDirection.North:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        case Piece.EnumDirection.NorthEast:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        case Piece.EnumDirection.East:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y);
                                            break;
                                        case Piece.EnumDirection.SouthEast:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X + i, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.South:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.SouthWest:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y + i);
                                            break;
                                        case Piece.EnumDirection.West:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y);
                                            break;
                                        case Piece.EnumDirection.NorthWest:
                                            tempCoord = new Point(sqrToProbe.Piece.Position.X - i, sqrToProbe.Piece.Position.Y - i);
                                            break;
                                        default:
                                            break;
                                    }

                                    BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                    if (item == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (item.Piece == null)
                                        {
                                            sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                        }
                                        else
                                        {
                                            if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                            {
                                                sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region KING
                    case Piece.PieceTypes.King:
                        {
                            foreach (var dir in sqrToProbe.Piece.ListDirections)
                            {
                                Point tempCoord = new Point();
                                switch (dir)
                                {
                                    case Piece.EnumDirection.North:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y - 1);
                                        break;
                                    case Piece.EnumDirection.NorthEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y - 1);
                                        break;
                                    case Piece.EnumDirection.East:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y);
                                        break;
                                    case Piece.EnumDirection.SouthEast:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X + 1, sqrToProbe.Piece.Position.Y + 1);
                                        break;
                                    case Piece.EnumDirection.South:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X, sqrToProbe.Piece.Position.Y + 1);
                                        break;
                                    case Piece.EnumDirection.SouthWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y + 1);
                                        break;
                                    case Piece.EnumDirection.West:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y);
                                        break;
                                    case Piece.EnumDirection.NorthWest:
                                        tempCoord = new Point(sqrToProbe.Piece.Position.X - 1, sqrToProbe.Piece.Position.Y - 1);
                                        break;
                                    default:
                                        break;
                                }

                                BoardSquare item = InWhichSquareAreWeByCoord(tempCoord);
                                if (item == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (item.Piece == null)
                                    {
                                        sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                    }
                                    else
                                    {
                                        if (sqrToProbe.Piece.PieceColor != item.Piece.PieceColor)
                                        {
                                            sqrToProbe.Piece.ListPossibleMoves.Add(tempCoord);
                                        }
                                        continue;
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
        }

        private void InitializeChessBoard()
        {
            Board = new List<BoardSquare>();
            int tempRow = 0;
            int tempColumn = 0;

            for (int i = 0; i < SquareNumbers; i++)
            {
                BoardSquare neoSquare = new BoardSquare();
                neoSquare.SquareDestination = new Rectangle(tempColumn * SquareSize, tempRow * SquareSize, SquareSize, SquareSize);
                if (IsEven(tempRow + tempColumn))
                {
                    neoSquare.SquareTexture = LightSquare;
                }
                else
                {
                    neoSquare.SquareTexture = DarkSquare;
                }
                neoSquare.Piece = null;
                neoSquare.SquareCoordinate = new Point(tempColumn, tempRow);
                neoSquare.Row = tempRow;
                neoSquare.Column = tempColumn;

                Board.Add(neoSquare);

                if (tempColumn < ColumnNumber - 1)
                {
                    tempColumn++;
                }
                else
                {
                    tempColumn = 0;
                    tempRow++;
                }
            }
        }

        private void InitializeChessPieces()
        {
            foreach (Piece.PieceColors color in (Piece.PieceColors[])Enum.GetValues(typeof(Piece.PieceColors)))
            {
                for (int i = 1; i < 9; i++)
                {
                    if (i == 1)
                    {
                        // create King and Queen
                        Piece tempQ = new Piece(color, Piece.PieceTypes.Queen, i);
                        Piece tempK = new Piece(color, Piece.PieceTypes.King, i);

                        Board.Where(x => x.Row == tempQ.Position.Y && x.Column == tempQ.Position.X).Single().Piece = tempQ;
                        Board.Where(x => x.Row == tempK.Position.Y && x.Column == tempK.Position.X).Single().Piece = tempK;
                    }

                    if (i < 3)
                    {
                        // create Rooks, Bishops and Knights
                        Piece tempR = new Piece(color, Piece.PieceTypes.Rook, i);
                        Piece tempB = new Piece(color, Piece.PieceTypes.Bishop, i);
                        Piece tempKn = new Piece(color, Piece.PieceTypes.Knight, i);

                        Board.Where(x => x.Row == tempR.Position.Y && x.Column == tempR.Position.X).Single().Piece = tempR;
                        Board.Where(x => x.Row == tempB.Position.Y && x.Column == tempB.Position.X).Single().Piece = tempB;
                        Board.Where(x => x.Row == tempKn.Position.Y && x.Column == tempKn.Position.X).Single().Piece = tempKn;
                    }

                    // pawns
                    Piece tempP = new Piece(color, Piece.PieceTypes.Pawn, i);
                    Board.Where(x => x.Row == tempP.Position.Y && x.Column == tempP.Position.X).Single().Piece = tempP;
                }
            }
        }

        // if the number is even (pair)
        private bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        public static BoardSquare InWhichSquareAreWeByPixel(MouseState currentState)
        {
            if (currentState.X < BoardSize && currentState.X > 0 && currentState.Y < BoardSize && currentState.Y > 0)
            {
                int tempRow = (int)Math.Floor((decimal)(currentState.Y / SquareSize));
                int tempCol = (int)Math.Floor((decimal)(currentState.X / SquareSize));

                return Board.Where(x => x.Row == tempRow && x.Column == tempCol).Single();
            }
            else
            {
                return null;
            }
        }

        private static BoardSquare InWhichSquareAreWeByCoord(Point posToCheck)
        {
            if (posToCheck.X < ColumnNumber && posToCheck.X >= 0 && posToCheck.Y < RowNumber && posToCheck.Y >= 0)
            {
                return Board.Where(x => x.Row == posToCheck.Y && x.Column == posToCheck.X).Single();
            }
            else
            {
                return null;
            }
        }

        public static void GetPossibleMovesForeachSide()
        {
            ListPossibleBlackMoves = new List<PossibleMove>();
            ListPossibleWhiteMoves = new List<PossibleMove>();

            foreach (BoardSquare sqrToHarvest in Board.Where(x => x.Piece != null))
            {
                switch (sqrToHarvest.Piece.PieceColor)
                {
                    case Piece.PieceColors.Black:
                        foreach (Point to in sqrToHarvest.Piece.ListPossibleMoves)
                        {
                            ListPossibleBlackMoves.Add(new PossibleMove()
                            {
                                Piece = new PieceLight() { PieceColor = sqrToHarvest.Piece.PieceColor, PieceType = sqrToHarvest.Piece.PieceType },
                                From = sqrToHarvest.Piece.Position,
                                To = to,
                                Density = 0,
                                Rate = 0,
                                WillBeEaten = false,
                                WillEat = false
                            });
                        }
                        break;
                    case Piece.PieceColors.White:
                        foreach (Point to in sqrToHarvest.Piece.ListPossibleMoves)
                        {
                            ListPossibleWhiteMoves.Add(new PossibleMove()
                            {
                                Piece = new PieceLight() { PieceColor = sqrToHarvest.Piece.PieceColor, PieceType = sqrToHarvest.Piece.PieceType },
                                From = sqrToHarvest.Piece.Position,
                                To = to,
                                Density = 0,
                                Rate = 0,
                                WillBeEaten = false,
                                WillEat = false
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Game Methods
        public void ChessBoardDraw(GameTime pGameTime)
        {
            foreach (BoardSquare tempSquare in Board)
            {
                SpriteBatch.Draw(tempSquare.SquareTexture, tempSquare.SquareDestination, null, Color.White);
                if (tempSquare.Piece != null)
                {
                    SpriteBatch.Draw(tempSquare.Piece.PieceTexture, tempSquare.SquareDestination, null, Color.White);
                }
                DebugToolBox.ShowLine(Content, SpriteBatch, tempSquare.SquareCoordinate.ToString(), new Vector2(tempSquare.SquareDestination.X + 15, tempSquare.SquareDestination.Y + 30));
            }

            if (OffBoardPieces.Count > 0)
            {
                foreach (Piece p in OffBoardPieces)
                {
                    SpriteBatch.Draw(p.PieceTexture, new Rectangle(p.Position.X * SquareSize + BoardSize, p.Position.Y * SquareSize, SquareSize, SquareSize), null, Color.White);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (i == ListPossibleBlackMoves.Count)
                {
                    break;
                }
                PossibleMove poMo = ListPossibleBlackMoves[i];
                DebugToolBox.ShowLine(Content, SpriteBatch,
                    poMo.Piece.PieceType + " / " + poMo.Rate * 100 + " %",
                    new Vector2(ChessBoard.BoardSize + 15, 15 + 15 * i));
            }
        }
        #endregion
    }
}