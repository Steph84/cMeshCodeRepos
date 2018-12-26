using Microsoft.Xna.Framework.Input;

namespace ChessAI.UtilFolder
{
    public class MouseManager
    {
        public MouseState NewState { get; set; }

        MouseState oldState = new MouseState();
        EnumMouse temp = EnumMouse.NoAction;

        public enum EnumMouse
        {
            NoAction = 0,
            Press = 1,
            Hold = 2,
            Up = 3
        }

        public MouseManager()
        {
            NewState = Mouse.GetState();
        }
        
        public EnumMouse MouseAction()
        {
            temp = EnumMouse.NoAction;
            NewState = Mouse.GetState();

            if (NewState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Released)
            {
                temp = EnumMouse.Press;
            }
            else if (NewState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Hold;
            }
            else if (NewState.LeftButton != ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                temp = EnumMouse.Up;
            }

            oldState = NewState;
            return temp;
        }
    }
}