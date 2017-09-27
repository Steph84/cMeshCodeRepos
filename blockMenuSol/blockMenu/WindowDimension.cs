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
        public int WindowWidth { get; private set; }
        public int WindowHeight { get; private set; }
        private DisplayMode CurrentDisplayMode { get; set; }

        public WindowDimension(DisplayMode pCurrentDisplayMode)
        {
            CurrentDisplayMode = pCurrentDisplayMode;
        }

        public void ResizeWindowGame(GraphicsDeviceManager pGraphics)
        {
            // if display size in that range, then graphics change too
            // use enum for 4 sizes possible
            // window sizes - resultion display
            // 960 576 min 1024	768
            // 1440	864 min 1920 1080
            // 1920 1152 min 2560 1440
            // 2880 1728 min 3840 2160




        }

    }
}
