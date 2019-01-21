using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessAI.Piece;

namespace ChessAI
{
    public class Computer
    {
        #region Attributes
        private List<PossibleMove> ListPossibleBlackMoves { get; set; }
        private List<PossibleMove> ListPossibleWhiteMoves { get; set; }
        private double OriginalDensity { get; set; }

        private class PossibleMove
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
            public PieceColors PieceColor { get; set; }
            public PieceTypes PieceType { get; set; }
        }

        Random pickMove;
        int pickMoveSeed;
        #endregion

        public Computer()
        {
            ListPossibleBlackMoves = new List<PossibleMove>();
            ListPossibleWhiteMoves = new List<PossibleMove>();
            OriginalDensity = ComputeDensity(ChessBoard.Board);
        }

        private double ComputeDensity(ChessBoard.BoardSquare[,] tempBoard)
        {
            double currentDensity = 0;
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                ChessBoard.BoardSquare[] sqrRow = Enumerable.Range(0, ChessBoard.Board.GetLength(1))
                .Select(x => tempBoard[row, x])
                .ToArray();

                int pieceNbByRow = sqrRow.Where(x => x.Piece != null && x.Piece.PieceColor == PieceColors.Black).Count();
                currentDensity += (double)(pieceNbByRow * (row + 1)) / ChessBoard.ColumnNumber;
            }
            return currentDensity;
        }

        public GameRun.PlayerTurn ComputerUpdate(GameTime pGameTime, GameRun.PlayerTurn pTurn)
        {
            ListPossibleBlackMoves = new List<PossibleMove>();
            ListPossibleWhiteMoves = new List<PossibleMove>();

            #region get possible moves foreach side
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                for (int column = 0; column < ChessBoard.ColumnNumber; column++)
                {
                    ChessBoard.BoardSquare sqrToHarvest = ChessBoard.Board[row, column];
                    if (sqrToHarvest.Piece != null)
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
            }
            #endregion

            #region manage density
            if (ListPossibleBlackMoves.Count > 0)
            {
                foreach (PossibleMove poMoDensity in ListPossibleBlackMoves)
                {
                    // change ChessBoard to possible (cannot clone because of Rectangle not serializable)
                    ChessBoard.Board[poMoDensity.To.Y, poMoDensity.To.X].Piece = ChessBoard.Board[poMoDensity.From.Y, poMoDensity.From.X].Piece;
                    ChessBoard.Board[poMoDensity.From.Y, poMoDensity.From.X].Piece = null;

                    // compute density
                    poMoDensity.Density = ComputeDensity(ChessBoard.Board);

                    // ChessBoard back to original
                    ChessBoard.Board[poMoDensity.From.Y, poMoDensity.From.X].Piece = ChessBoard.Board[poMoDensity.To.Y, poMoDensity.To.X].Piece;
                    ChessBoard.Board[poMoDensity.To.Y, poMoDensity.To.X].Piece = null;
                }
            }
            #endregion

            #region check the booleans WillBeEaten and WillEat
            //if (ListPossibleBlackMoves.Count > 0)
            //{
            //    List<Point> listCurrentPosWhite = ListPossibleWhiteMoves.Select(x => x.From).Distinct().ToList();
            //    //List<Point> listPossiblePosWhite = ListPossibleWhiteMoves.Select(x => x.To).Distinct().ToList();

            //    foreach (PossibleMove posMov in ListPossibleBlackMoves)
            //    {
            //        //if (listPossiblePosWhite.Contains(posMov.To))
            //        //{
            //        //    posMov.WillBeEaten = true;
            //        //}

            //        if (listCurrentPosWhite.Contains(posMov.To))
            //        {
            //            posMov.WillEat = true;
            //        }
            //    }
            //}
            #endregion

            #region Compute Probability Rate
            // en fonction de la densité et des boolean et d'autres choses peut être...
            if (ListPossibleBlackMoves.Count > 0)
            {
                foreach (PossibleMove poMoProbRate in ListPossibleBlackMoves)
                {
                    poMoProbRate.Rate = poMoProbRate.Density;
                }
            }
            #endregion

            #region Pick a Move
            // dans la liste, en fonction du taux, choisir un mouvement
            if (ListPossibleBlackMoves.Count > 0)
            {
                // order by rate value
                ListPossibleBlackMoves = ListPossibleBlackMoves.OrderByDescending(x => x.Rate).ToList();

                // sum of the probability rates
                double sum = ListPossibleBlackMoves.Sum(x => x.Rate);

                // get a random number between the original density to the sum of densities
                pickMoveSeed = Environment.TickCount + (int)sum;
                pickMove = new Random(pickMoveSeed);
                double pickValue = pickMove.Next((int)(OriginalDensity * 100), (int)(sum * 100)) / 100.0d;

                // TODO FIX IT
                // find the move and do it
                double cumulDensity = 0;
                foreach (PossibleMove poMoPickMove in ListPossibleBlackMoves)
                {
                    if(cumulDensity >= pickValue)
                    {
                        // move the Piece
                        ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece = ChessBoard.Board[poMoPickMove.From.Y, poMoPickMove.From.X].Piece;
                        ChessBoard.Board[poMoPickMove.From.Y, poMoPickMove.From.X].Piece = null;

                        // update some properties of the Piece
                        if (ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece.PieceType == PieceTypes.Pawn
                            && ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece.Speed == 2)
                        {
                            ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece.Speed = 1;
                        }
                        ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece.NbMove++;
                        ChessBoard.Board[poMoPickMove.To.Y, poMoPickMove.To.X].Piece.Position = new Point(poMoPickMove.To.X, poMoPickMove.To.Y);

                        break;
                    }
                    cumulDensity += poMoPickMove.Rate;
                }
            }
            #endregion

            pTurn = GameRun.PlayerTurn.Player;
            return pTurn;
        }
    }
}
