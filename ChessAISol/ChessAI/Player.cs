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
                BoardSquare tempSquare = InWhichSquareAreWe(newState);
                if (tempSquare != null && tempSquare.Piece != null && tempSquare.Piece.PieceColor == Piece.Color.White)
                {
                    Piece = tempSquare.Piece;
                    IsHolding = true;
                    tempSquare.Piece = null;
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
