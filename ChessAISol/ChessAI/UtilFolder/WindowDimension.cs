using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessAI.UtilFolder
{
    public class WindowDimension
    {
        #region Properties
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }
        public double GameSizeCoefficient { get; set; }
        private GraphicsDeviceManager Graphics { get; set; }
        private GameWindow GameWindow { get; set; }

        // initialize values
        bool AllowAltF4 = true;
        bool IsBorderless = false;
        bool IsFullScreen = false;

        // Array for the GameWindow in relation to the resolution
        int [,] ArrayResolution = new int [4,5] {  // displayRes / gameWindowRes
                                                    { 1024, 768, 1152, 576, 10 },
                                                    { 1920, 1080, 1728, 864, 15 },
                                                    { 2560, 1440, 2304, 1152, 20 },
                                                    { 3840, 2160, 3456, 1728, 30 }
                                                };
        #endregion

        #region WindowDimension Constructor
        public WindowDimension(DisplayMode pCurrentDisplayMode,
                               Viewport pViewport,
                               GameWindow pGameWindow,
                               GraphicsDeviceManager pGraphics)
        {
            // get the different obbjects
            Graphics = pGraphics;
            GameWindow = pGameWindow;

            // get the different current dimensions
            DisplayWidth = pCurrentDisplayMode.Width;
            DisplayHeight = pCurrentDisplayMode.Height;
            GameWindowWidth = pViewport.Width;
            GameWindowHeight = pViewport.Height;

            // initialize gameWindow parameters
            pGameWindow.AllowAltF4 = AllowAltF4;
            pGameWindow.IsBorderless = IsBorderless;

            if(IsFullScreen == true)
            {
                Graphics.PreferredBackBufferWidth = DisplayWidth;
                Graphics.PreferredBackBufferHeight = DisplayHeight;
                pGraphics.IsFullScreen = IsFullScreen;
            }
            else
                ResizeGameWindow();

            // apply changes ;-)
            pGraphics.ApplyChanges();
        }
        #endregion

        #region Method to Resize the GameWindow
        private void ResizeGameWindow()
        {
            int newGameWindowWidth = 0;
            int newGameWindowHeight = 0;

            // foreach height value of display, choose the correct resolution
            for (int line = 0; line < ArrayResolution.GetLength(0); line++)
            {
                if (DisplayHeight >= ArrayResolution[line, 1])
                {
                    newGameWindowWidth = ArrayResolution[line, 2];
                    newGameWindowHeight = ArrayResolution[line, 3];
                    GameSizeCoefficient = ArrayResolution[line, 4] / 10.0d;
                }
                else
                    break;
            }

            // check if the GameWindow overlap the Display
            if(newGameWindowWidth > DisplayWidth)
            {
                // if so, don t bother, switch to fullScreen
                newGameWindowWidth = DisplayWidth;
                newGameWindowHeight = DisplayHeight;
                Graphics.IsFullScreen = true;
            }
            else
            {
                // if not set the dimension then move the gameWindow to center it
                int newPosX = 0;
                int newPosY = 0;
                newPosX = (DisplayWidth - newGameWindowWidth) / 2;
                newPosY = (DisplayHeight - newGameWindowHeight) / 3;
                GameWindow.Position = new Point(newPosX, newPosY);
            }
            
            // update the GameWindow
            Graphics.PreferredBackBufferWidth = newGameWindowWidth;
            Graphics.PreferredBackBufferHeight = newGameWindowHeight;

            // update the size in the object
            GameWindowWidth = newGameWindowWidth;
            GameWindowHeight = newGameWindowHeight;
        }
        #endregion
    }
}
