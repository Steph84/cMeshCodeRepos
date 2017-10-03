using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockMenu
{
    public class WindowDimension
    {
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }
        private GraphicsDeviceManager Graphics { get; set; }
        private GameWindow GameWindow { get; set; }

        private bool AllowAltF4 = true;
        private bool IsBorderless = false;
        private bool IsFullScreen = false;
        private string MyTitleGameWindow = "This is a game !";

        private int [,] ArrayResolution = new int [4,4] {  // displayRes / gameWindowRes
                                                            { 1024, 768, 1152, 576 },
                                                            { 1920, 1080, 1728, 864 },
                                                            { 2560, 1440, 2304, 1152 },
                                                            { 3840, 2160, 3456, 1728 }
                                                       };
                          
        public WindowDimension(DisplayMode pCurrentDisplayMode, Viewport pViewport, GameWindow pGameWindow, GraphicsDeviceManager pGraphics)
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
            pGameWindow.Title = MyTitleGameWindow;

            pGraphics.IsFullScreen = IsFullScreen;

            // apply changes ;-)
            pGraphics.ApplyChanges();
        }

        private void ResizeGameWindow()
        {
            int newGameWindowWidth;
            int newGameWindowHeight;

            // change the resolution in relation to the display size



            Graphics.PreferredBackBufferWidth = windowWidth;
            Graphics.PreferredBackBufferHeight = windowHeight;
            GameWindow.Position = new Point(0, 0);

        }
    }
}
