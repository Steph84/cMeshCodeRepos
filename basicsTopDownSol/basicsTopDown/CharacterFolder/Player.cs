using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace basicsTopDown.CharacterFolder
{
    public class Player : CharacterObject
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient)
        {

        }

        public void PlayerControl(GameTime pGameTime)
        {
            newState = Keyboard.GetState();

            IsMoving = false;

            if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - 2, Position.Y, Position.Width, Position.Height);
            }

            if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + 2, Position.Y, Position.Width, Position.Height);
            }

            if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyDown(Keys.Up))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y - 2, Position.Width, Position.Height);
            }

            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyDown(Keys.Down))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y + 2, Position.Width, Position.Height);
            }

            oldState = newState;
        }
        
    }
}
