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
    public class Piece
    {
        #region Attributes
        public bool IsOnTheBoard { get; set; }
        public int Index { get; set; }
        public Color PieceColor { get; set; }
        public Type PieceType { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D PieceTexture { get; set; }
        private static Dictionary<Type, Texture2D> DictCorresWhitePieceText { get; set; }
        private static Dictionary<Type, Texture2D> DictCorresBlackPieceText { get; set; }

        public enum Type
        {
            Pawn = 1,
            Rook = 2,
            Knight = 3,
            Bishop = 4,
            Queen = 5,
            King = 6
        }

        public enum Color
        {
            Black = 0,
            White = 1
        }
        #endregion

        public Piece(Color pieceColor, Type pieceType, int index)
        {
            IsOnTheBoard = true;
            PieceColor = pieceColor;
            PieceType = pieceType;
            Index = index;
            InitPosition();
            InitTexture();
        }

        // TODO
        private void InitPosition()
        {
            switch (PieceColor)
            {
                #region Black
                case Color.Black:
                    switch (PieceType)
                    {
                        case Type.Pawn:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Vector2(0, 1);
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Rook:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Knight:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Bishop:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Queen:
                            break;
                        case Type.King:
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region White
                case Color.White:
                    switch (PieceType)
                    {
                        case Type.Pawn:
                            switch (Index)
                            {
                                case 1:
                                    Position = new Vector2(0, 6);
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Type.Rook:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Knight:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Bishop:
                            switch (Index)
                            {
                                case 1:
                                    break;
                                case 2:
                                default:
                                    break;
                            }
                            break;
                        case Type.Queen:
                            break;
                        case Type.King:
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
                case Color.Black:
                    PieceTexture = DictCorresBlackPieceText[PieceType];
                    break;
                case Color.White:
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
            //DictCorresWhitePieceText = new Dictionary<Type, Texture2D>();
            //DictCorresWhitePieceText[Type.Pawn] = Content.Load<Texture2D>("w_pawn_1x");
            //DictCorresWhitePieceText[Type.Rook] = Content.Load<Texture2D>("w_rook_1x");
            //DictCorresWhitePieceText[Type.Knight] = Content.Load<Texture2D>("w_knight_1x");
            //DictCorresWhitePieceText[Type.Bishop] = Content.Load<Texture2D>("w_bishop_1x");
            //DictCorresWhitePieceText[Type.Queen] = Content.Load<Texture2D>("w_queen_1x");
            //DictCorresWhitePieceText[Type.King] = Content.Load<Texture2D>("w_king_1x");
        }
    }
}
