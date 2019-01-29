﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            //OriginalDensity = ComputeDensity(ChessBoard.Board);
            OriginalDensity = ComputeDensityInList(ChessBoard.Board);
        }

        private double ComputeDensityInList(List<ChessBoard.BoardSquare> tempBoard)
        {
            double currentDensity = 0;
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                int pieceNbByRow = tempBoard
                    .Where(x => x.Row == row)
                    .Where(x => x.Piece != null && x.Piece.PieceColor == PieceColors.Black)
                    .ToList().Count();
                
                currentDensity += (double)(pieceNbByRow * (row + 1)) / ChessBoard.ColumnNumber;
            }
            return currentDensity;
        }

        private double ComputeDensityInArray(ChessBoard.BoardSquare[,] tempBoard)
        {
            double currentDensity = 0;
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                ChessBoard.BoardSquare[] sqrRow = Enumerable.Range(0, tempBoard.GetLength(1))
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
                    //ChessBoard.BoardSquare sqrToHarvest = ChessBoard.Board[row, column];
                    ChessBoard.BoardSquare sqrToHarvest = ChessBoard.Board.Where(x => x.Row == row && x.Column == column).Single();
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

            if (ListPossibleBlackMoves.Count > 0)
            {
                #region manage density
                OriginalDensity = ComputeDensityInList(ChessBoard.Board);

                // Copy the Board from List to Array because cannot clone xna objects
                ChessBoard.BoardSquare[,] CopyBoard = new ChessBoard.BoardSquare[ChessBoard.RowNumber, ChessBoard.ColumnNumber];
                foreach(ChessBoard.BoardSquare item in ChessBoard.Board)
                {
                    CopyBoard[item.Row, item.Column] = item;
                }

                foreach (PossibleMove poMoDensity in ListPossibleBlackMoves)
                {
                    Piece pieceToPutback = null;
                    ChessBoard.BoardSquare targetSqr = CopyBoard[poMoDensity.To.Y, poMoDensity.To.X];
                    if (targetSqr.Piece != null)
                    {
                        pieceToPutback = CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece;
                    }

                    // change ChessBoard to possible (cannot clone because of Rectangle not serializable)
                    CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece = CopyBoard[poMoDensity.From.Y, poMoDensity.From.X].Piece;
                    CopyBoard[poMoDensity.From.Y, poMoDensity.From.X].Piece = null;

                    // compute density
                    poMoDensity.Density = ComputeDensityInArray(CopyBoard);

                    // ChessBoard back to original
                    CopyBoard[poMoDensity.From.Y, poMoDensity.From.X].Piece = CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece;
                    CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece = pieceToPutback;
                }
                #endregion

                #region check the booleans WillBeEaten and WillEat
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
                #endregion

                #region Compute Probability Rate
                // en fonction de la densité et des boolean et d'autres choses peut être...
                foreach (PossibleMove poMoProbRate in ListPossibleBlackMoves)
                {
                    poMoProbRate.Rate = poMoProbRate.Density;
                }
                #endregion

                #region Pick a Move
                // dans la liste, en fonction du taux, choisir un mouvement
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
                double cumulDensity = ListPossibleBlackMoves.First().Density;
                foreach (PossibleMove poMoPickMove in ListPossibleBlackMoves)
                {
                    if (cumulDensity >= pickValue)
                    {
                        // if there is a piece on the target square, put off the board
                        ChessBoard.BoardSquare targetSqr = ChessBoard.Board.Where(x => x.Row == poMoPickMove.To.Y && x.Column == poMoPickMove.To.X).Single();
                        if (targetSqr.Piece != null)
                        {
                            ChessBoard.OffBoardPieces.Add(targetSqr.Piece);
                        }

                        // move the Piece
                        ChessBoard.BoardSquare origSqr = ChessBoard.Board.Where(x => x.Row == poMoPickMove.From.Y && x.Column == poMoPickMove.From.X).Single();
                        targetSqr.Piece = origSqr.Piece;
                        origSqr.Piece = null;

                        // update some properties of the Piece
                        if (targetSqr.Piece.PieceType == PieceTypes.Pawn && targetSqr.Piece.Speed == 2)
                        {
                            targetSqr.Piece.Speed = 1;
                        }
                        targetSqr.Piece.NbMove++;
                        targetSqr.Piece.Position = new Point(poMoPickMove.To.X, poMoPickMove.To.Y);

                        ChessBoard.SearchPossibleMoves();

                        break;
                    }
                    cumulDensity += poMoPickMove.Rate;
                }
            }
            else
            {
                // black game over
            }
            #endregion

            pTurn = GameRun.PlayerTurn.Player;
            return pTurn;
        }
    }
}
