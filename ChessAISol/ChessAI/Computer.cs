using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static ChessAI.Piece;

namespace ChessAI
{
    public class Computer
    {
        #region Attributes
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private double OriginalDensity { get; set; }
        
        Random pickMove;
        int pickMoveSeed;
        #endregion

        public Computer(ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            SpriteBatch = pSpriteBatch;
            Content = pContent;
            OriginalDensity = ComputeDensityInListWeighted(ChessBoard.Board);
        }

        #region ComputeDensity
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

        private double ComputeDensityInListWeighted(List<ChessBoard.BoardSquare> tempBoard)
        {
            double currentDensity = tempBoard
                .Where(x => x.Piece != null && x.Piece.PieceColor == PieceColors.Black)
                .ToList().Sum(x => (x.Row + 1) * x.Piece.Value);

            currentDensity = currentDensity / ChessBoard.ColumnNumber;

            return currentDensity;
        }

        private double ComputeDensityInArrayWeighted(ChessBoard.BoardSquare[,] tempBoard)
        {
            double currentDensity = 0;
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                ChessBoard.BoardSquare[] sqrRow = Enumerable.Range(0, tempBoard.GetLength(1))
                .Select(x => tempBoard[row, x])
                .ToArray();

                double tempDens = sqrRow
                    .Where(x => x.Piece != null && x.Piece.PieceColor == PieceColors.Black)
                    .ToList().Sum(x => (x.Row + 1) * x.Piece.Value);

                tempDens = tempDens / ChessBoard.ColumnNumber;

                currentDensity += tempDens;
            }
            return currentDensity;
        }
        #endregion
        public GameRun.PlayerTurn ComputerUpdate(GameTime pGameTime, GameRun.PlayerTurn pTurn)
        {
            ChessBoard.GetPossibleMovesForeachSide();
            ChessBoard.CheckEating();

            if (ChessBoard.ListPossibleBlackMoves.Count > 0)
            {
                #region manage density
                OriginalDensity = ComputeDensityInListWeighted(ChessBoard.Board);

                // Copy the Board from List to Array because cannot clone xna objects
                ChessBoard.BoardSquare[,] CopyBoard = new ChessBoard.BoardSquare[ChessBoard.RowNumber, ChessBoard.ColumnNumber];
                foreach (ChessBoard.BoardSquare item in ChessBoard.Board)
                {
                    CopyBoard[item.Row, item.Column] = item;
                }

                foreach (ChessBoard.PossibleMove poMoDensity in ChessBoard.ListPossibleBlackMoves)
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
                    poMoDensity.Density = ComputeDensityInArrayWeighted(CopyBoard);

                    // ChessBoard back to original
                    CopyBoard[poMoDensity.From.Y, poMoDensity.From.X].Piece = CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece;
                    CopyBoard[poMoDensity.To.Y, poMoDensity.To.X].Piece = pieceToPutback;
                }
                #endregion

                #region Compute Probability Rate
                // en fonction de la densité et des boolean et d'autres choses peut être...
                double sumDensity = ChessBoard.ListPossibleBlackMoves.Sum(x => x.Density);
                foreach (ChessBoard.PossibleMove poMoProbRate in ChessBoard.ListPossibleBlackMoves)
                {
                    if(poMoProbRate.WillEat)
                    {
                        poMoProbRate.Density++;
                    }

                    if (poMoProbRate.WillBeEaten)
                    {
                        poMoProbRate.Density--;
                    }

                    poMoProbRate.Rate = Math.Round(poMoProbRate.Density / sumDensity, 5);
                }
                #endregion

                #region Pick a Move
                // dans la liste, en fonction du taux, choisir un mouvement
                // order by rate value
                ChessBoard.ListPossibleBlackMoves = ChessBoard.ListPossibleBlackMoves.OrderByDescending(x => x.Rate).ToList();

                // sum of the probability rates
                double sum = ChessBoard.ListPossibleBlackMoves.Sum(x => x.Rate);

                // get a random number between the original density to the sum of densities
                pickMoveSeed = Environment.TickCount + (int)sum;
                pickMove = new Random(pickMoveSeed);
                //double pickValue = pickMove.Next((int)(OriginalDensity * 100), (int)(sum * 100)) / 100.0d;
                double pickValue = pickMove.Next(0, 100) / 100.0d;

                // find the move and do it
                double cumulRate = ChessBoard.ListPossibleBlackMoves.First().Rate;
                foreach (ChessBoard.PossibleMove poMoPickMove in ChessBoard.ListPossibleBlackMoves)
                {
                    if (cumulRate >= pickValue)
                    {
                        // if there is a piece on the target square, put off the board
                        ChessBoard.BoardSquare targetSqr = ChessBoard.Board.Where(x => x.Row == poMoPickMove.To.Y && x.Column == poMoPickMove.To.X).Single();
                        if (targetSqr.Piece != null)
                        {
                            ChessBoard.OffBoardPieces.Add(targetSqr.Piece);
                            // if the king is dead, gameOver
                            if (targetSqr.Piece.PieceType == PieceTypes.King)
                            {
                                pTurn = GameRun.PlayerTurn.None;
                            }
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
                    cumulRate += poMoPickMove.Rate;
                }
                #endregion
            }

            if (pTurn == GameRun.PlayerTurn.Computer)
            {
                pTurn = GameRun.PlayerTurn.Player;
            }
            return pTurn;
        }
    }
}
