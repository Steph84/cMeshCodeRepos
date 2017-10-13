using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
