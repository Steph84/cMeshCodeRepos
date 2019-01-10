using ChessAI.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessAI.ChessBoard;
using static ChessAI.UtilFolder.MouseManager;

namespace ChessAI
{
    public class Player
    {
        private SpriteBatch SpriteBatch { get; set; }
        public int MouseX { get; set; }
        public int MouseY { get; set; }
        public bool IsHolding { get; set; }
        public Piece Piece { get; set; }

        //MouseManager myMouse = new MouseManager();
        MouseState oldState = new MouseState();
        MouseState newState = new MouseState();
        EnumMouse temp = EnumMouse.NoAction;

        public Player(SpriteBatch pSpriteBatch)
        {
            SpriteBatch = pSpriteBatch;
            IsHolding = false;
            Piece = null;
        }

        public void PlayerUpdate(GameTime pGameTime)
        {
            temp = EnumMouse.NoAction;
            newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Released)
            {
                temp = EnumMouse.Press;
                BoardSquare tempSquare = InWhichSquareAreWeByPixel(newState);

                // if we are on the chess board
                if (tempSquare != null)
                {
                    // if I don't have a piece in my hand
                    if (Piece == null)
                    {
                        if (tempSquare.Piece != null && tempSquare.Piece.PieceColor == Piece.PieceColors.White)
                        {
                            Piece = tempSquare.Piece;
                            IsHolding = true;
                            tempSquare.Piece = null;

                            // as soon as I have the piece, I find all the possible moves

                        }
                    }
                    // if I do have a piece in my hand
                    else
                    {
                        // if we are on an empty square
                        if (tempSquare.Piece == null)
                        {
                            // check if the move is correct
                        }
                        // if we are on square with a piece
                        else
                        {

                        }
                    }
                }
            }
            else if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Hold;
            }
            else if (newState.LeftButton != ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Up;
            }

            oldState = newState;
        }


        public void PlayerDraw(GameTime pGameTime)
        {
            if (IsHolding == true)
            {
                SpriteBatch.Draw(Piece.PieceTexture, new Rectangle(newState.X - (SquareSize / 2), newState.Y - (SquareSize / 2), SquareSize, SquareSize), null, Color.White);
            }
        }
    }
}
