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
        public List<PossibleMove> ListPossibleBlackMoves { get; set; }
        public List<PossibleMove> ListPossibleWhiteMoves { get; set; }

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
            public PieceColors PieceColor { get; set; }
            public PieceTypes PieceType { get; set; }
        }
        #endregion

        public Computer()
        {
            ListPossibleBlackMoves = new List<PossibleMove>();
            ListPossibleWhiteMoves = new List<PossibleMove>();
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
            #region Current Density
            var currentDensity = 0;
            for (int row = 0; row < ChessBoard.RowNumber; row++)
            {
                ChessBoard.BoardSquare[] sqrRow = Enumerable.Range(0, ChessBoard.Board.GetLength(1))
                .Select(x => ChessBoard.Board[row, x])
                .ToArray();

                int pieceNbByRow = sqrRow.Where(x => x.Piece != null && x.Piece.PieceColor == PieceColors.Black).Count();
                currentDensity += (pieceNbByRow * (row + 1))/ ChessBoard.ColumnNumber;
            }
            #endregion
            #region Possible Densities
            // pour chaque possible move black
            // créer nouveau chess board temporaire
            // calculer la densité possible

            #endregion
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
            // en fonction de la dnesité et des boolean et d'autres choses peut être...
            #endregion

            #region Pick a Move
            // dans la liste, en fonction du taux, choisir un mouvement
            #endregion

            pTurn = GameRun.PlayerTurn.Player;
            return pTurn;
        }
    }
}
