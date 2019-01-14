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
            return pTurn;
        }
    }
}
