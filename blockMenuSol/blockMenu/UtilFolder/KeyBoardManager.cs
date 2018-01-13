using Microsoft.Xna.Framework.Input;

namespace blockMenu
{
    public class KeyBoardManager
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();
        EnumKeyBoard temp = EnumKeyBoard.NoAction;

        public enum EnumKeyBoard
        {
            NoAction = 0,
            Press = 1,
            Hold = 2,
            Up = 3
        }

        #region Method to determine the action depend on the keyboard inputs
        public EnumKeyBoard KeyBoardAction(Keys pKey)
        {
            temp = EnumKeyBoard.NoAction;
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(pKey) && !oldState.IsKeyDown(pKey))
            {
                temp = EnumKeyBoard.Press;
            }
            else if (newState.IsKeyDown(pKey) && oldState.IsKeyDown(pKey))
            {
                temp = EnumKeyBoard.Hold;
            }
            else if (!newState.IsKeyDown(pKey) && oldState.IsKeyDown(pKey))
            {
                temp = EnumKeyBoard.Up;
            }
            oldState = newState;
            return temp;
        }
        #endregion
    }
}
