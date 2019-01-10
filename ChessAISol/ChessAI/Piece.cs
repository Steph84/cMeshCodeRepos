using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ChessAI
{
    public class Piece
    {
        #region Attributes
        public bool IsOnTheBoard { get; set; }
        public int Index { get; set; }
        public PieceColors PieceColor { get; set; }
        public Type PieceType { get; set; }
        public Point Position { get; set; }
        public Texture2D PieceTexture { get; set; }
        public List<Point> ListPossibleMoves { get; set; }
        public int NbMove { get; set; }
        public int Speed { get; set; }
        public List<EnumDirection> ListDirections { get; set; }
        #endregion

        static Dictionary<Type, Texture2D> DictCorresWhitePieceText;
        static Dictionary<Type, Texture2D> DictCorresBlackPieceText;
        
        public enum Type
        {
            Pawn = 1,
            Rook = 2,
            Knight = 3,
            Bishop = 4,
            Queen = 5,
            King = 6
        }

        public enum PieceColors
        {
            Black = 0,
            White = 1
        }
        
        public enum EnumDirection
        {
            None = 0,
            North = 1,
            NorthNorthEast = 2,
            NorthEast = 3,
            NorthEastEast = 4,
            East = 5,
            SouthEastEast = 6,
            SouthEast = 7,
            SouthSouthEast = 8,
            South = 9,
            SouthSouthWest = 10,
            SouthWest = 11,
            SouthWestWest = 12,
            West = 13,
            NorthWestWest = 14,
            NorthWest = 15,
            NorthNorthWest = 16
        }

        public Piece(PieceColors pieceColor, Type pieceType, int index)
        {
            IsOnTheBoard = true;
            PieceColor = pieceColor;
            PieceType = pieceType;
            Index = index;
            NbMove = 0;
            ListPossibleMoves = new List<Point>();
            InitPosition();
            InitTexture();
            InitSpeed();
            InitListDirections();
        }

        #region Methods
        private void InitListDirections()
        {
            switch (PieceType)
            {
                case Type.Pawn:
                    ListDirections = null;
                    break;
                case Type.Knight:
                    ListDirections = new List<EnumDirection>()
                    {
                        EnumDirection.NorthNorthEast,
                        EnumDirection.NorthEastEast,
                        EnumDirection.SouthEastEast,
                        EnumDirection.SouthSouthEast,
                        EnumDirection.SouthSouthWest,
                        EnumDirection.SouthWestWest,
                        EnumDirection.NorthWestWest,
                        EnumDirection.NorthNorthWest
                    };
                    break;
                case Type.Rook:
                    ListDirections = new List<EnumDirection>()
                    {
                        EnumDirection.North,
                        EnumDirection.East,
                        EnumDirection.South,
                        EnumDirection.West,
                    };
                    break;
                case Type.Bishop:
                    ListDirections = new List<EnumDirection>()
                    {
                        EnumDirection.NorthEast,
                        EnumDirection.SouthEast,
                        EnumDirection.SouthWest,
                        EnumDirection.NorthWest
                    };
                    break;
                case Type.King:
                case Type.Queen:
                    ListDirections = new List<EnumDirection>()
                    {
                        EnumDirection.North,
                        EnumDirection.NorthEast,
                        EnumDirection.East,
                        EnumDirection.SouthEast,
                        EnumDirection.South,
                        EnumDirection.SouthWest,
                        EnumDirection.West,
                        EnumDirection.NorthWest
                    };
                    break;
                default:
                    break;
            }
        }

        private void InitSpeed()
        {
            switch (PieceType)
            {
                case Type.Pawn:
                    Speed = 2;
                    break;
                case Type.Knight:
                    Speed = -1; // specific movement
                    break;
                case Type.King:
                    Speed = 1;
                    break;
                case Type.Rook:
                case Type.Bishop:
                case Type.Queen:
                    Speed = 7; // max movement in a 8x8 board
                    break;
                default:
                    break;
            }
        }

        private void InitPosition()
        {
            switch (PieceColor)
            {
                #region Black
                case PieceColors.Black:
                    switch (PieceType)
                    {
                        case Type.Pawn:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(0, 1);
                                    break;
                                case 2:
                                    Position = new Point(1, 1);
                                    break;
                                case 3:
                                    Position = new Point(2, 1);
                                    break;
                                case 4:
                                    Position = new Point(3, 1);
                                    break;
                                case 5:
                                    Position = new Point(4, 1);
                                    break;
                                case 6:
                                    Position = new Point(5, 1);
                                    break;
                                case 7:
                                    Position = new Point(6, 1);
                                    break;
                                case 8:
                                    Position = new Point(7, 1);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Rook:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(0, 0);
                                    break;
                                case 2:
                                    Position = new Point(7, 0);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Knight:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(1, 0);
                                    break;
                                case 2:
                                    Position = new Point(6, 0);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Bishop:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(2, 0);
                                    break;
                                case 2:
                                    Position = new Point(5, 0);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Queen:
                            Position = new Point(3, 0);
                            break;
                        case Type.King:
                            Position = new Point(4, 0);
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region White
                case PieceColors.White:
                    switch (PieceType)
                    {
                        case Type.Pawn:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(0, 6);
                                    break;
                                case 2:
                                    Position = new Point(1, 6);
                                    break;
                                case 3:
                                    Position = new Point(2, 6);
                                    break;
                                case 4:
                                    Position = new Point(3, 6);
                                    break;
                                case 5:
                                    Position = new Point(4, 6);
                                    break;
                                case 6:
                                    Position = new Point(5, 6);
                                    break;
                                case 7:
                                    Position = new Point(6, 6);
                                    break;
                                case 8:
                                    Position = new Point(7, 6);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Rook:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(0, 7);
                                    break;
                                case 2:
                                    Position = new Point(7, 7);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Knight:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(1, 7);
                                    break;
                                case 2:
                                    Position = new Point(6, 7);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Bishop:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Point(2, 7);
                                    break;
                                case 2:
                                    Position = new Point(5, 7);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Queen:
                            Position = new Point(3, 7);
                            break;
                        case Type.King:
                            Position = new Point(4, 7);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
                    #endregion
            }
        }

        private void InitTexture()
        {
            switch (PieceColor)
            {
                case PieceColors.Black:
                    PieceTexture = DictCorresBlackPieceText[PieceType];
                    break;
                case PieceColors.White:
                    PieceTexture = DictCorresWhitePieceText[PieceType];
                    break;
                default:
                    break;
            }
        }

        public static void LoadPieceTextures(ContentManager Content)
        {
            DictCorresBlackPieceText = new Dictionary<Type, Texture2D>()
            {
                {Type.Pawn, Content.Load<Texture2D>("b_pawn_1x") },
                {Type.Rook, Content.Load<Texture2D>("b_rook_1x") },
                {Type.Knight, Content.Load<Texture2D>("b_knight_1x") },
                {Type.Bishop, Content.Load<Texture2D>("b_bishop_1x") },
                {Type.Queen, Content.Load<Texture2D>("b_queen_1x") },
                {Type.King, Content.Load<Texture2D>("b_king_1x") }
            };

            DictCorresWhitePieceText = new Dictionary<Type, Texture2D>()
            {
                {Type.Pawn, Content.Load<Texture2D>("w_pawn_1x") },
                {Type.Rook, Content.Load<Texture2D>("w_rook_1x") },
                {Type.Knight, Content.Load<Texture2D>("w_knight_1x") },
                {Type.Bishop, Content.Load<Texture2D>("w_bishop_1x") },
                {Type.Queen, Content.Load<Texture2D>("w_queen_1x") },
                {Type.King, Content.Load<Texture2D>("w_king_1x") }
            };
        }
        #endregion
    }
}
