using Microsoft.Xna.Framework;

public class WindowDimension : Game
{
    #region Properties
    public int DisplayWidth { get; set; }
    public int DisplayHeight { get; set; }
    public static int GameWindowWidth { get; private set; }
    public static int GameWindowHeight { get; private set; }
    public double GameSizeCoefficient { get; set; }
    private bool IsFullScreen { get; set; }

    // initialize values
    bool AllowAltF4 = true;
    bool IsBorderless = false;

    // Array for the GameWindow in relation to the resolution
    int[,] ArrayResolution = new int[4, 5] {  // displayRes/gameWindowRes/GameSizeCoefficient/nb tiles 32x32
                                                { 1366, 768, 1152, 640, 10 }, // 36x20
                                                { 1920, 1080, 1728, 960, 15 }, // 54x30
                                                { 2560, 1440, 2304, 1280, 20 }, // 72x40
                                                { 3840, 2160, 3456, 1920, 30 } // 108x60
                                            };
    #endregion

    #region WindowDimension Constructor     
    public WindowDimension(bool pIsFullScreen = false)
    {
        // get the different current dimensions
        DisplayWidth = Main.GlobalCurrentDisplayMode.Width;
        DisplayHeight = Main.GlobalCurrentDisplayMode.Height;
        GameWindowWidth = Main.GlobalViewport.Width;
        GameWindowHeight = Main.GlobalViewport.Height;
        IsFullScreen = pIsFullScreen;

        // initialize gameWindow parameters
        Main.GlobalGameWindow.AllowAltF4 = AllowAltF4;
        Main.GlobalGameWindow.IsBorderless = IsBorderless;

        if (IsFullScreen == true)
        {
            Main.GlobalGraphics.PreferredBackBufferWidth = DisplayWidth;
            Main.GlobalGraphics.PreferredBackBufferHeight = DisplayHeight;
            Main.GlobalGraphics.IsFullScreen = IsFullScreen;
        }
        else
            ResizeGameWindow();

        // update the size in the object
        GameWindowWidth = Main.GlobalGraphics.PreferredBackBufferWidth;
        GameWindowHeight = Main.GlobalGraphics.PreferredBackBufferHeight;

        // apply changes ;-)
        Main.GlobalGraphics.ApplyChanges();
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
        if (newGameWindowWidth > DisplayWidth)
        {
            // if so, don t bother, switch to fullScreen
            newGameWindowWidth = DisplayWidth;
            newGameWindowHeight = DisplayHeight;
            Main.GlobalGraphics.IsFullScreen = true;
        }
        else
        {
            // if not set the dimension then move the gameWindow to center it
            int newPosX = 0;
            int newPosY = 0;
            newPosX = (DisplayWidth - newGameWindowWidth) / 2;
            newPosY = (DisplayHeight - newGameWindowHeight) / 3;
            Main.GlobalGameWindow.Position = new Point(newPosX, newPosY);
        }

        // update the GameWindow
        Main.GlobalGraphics.PreferredBackBufferWidth = newGameWindowWidth;
        Main.GlobalGraphics.PreferredBackBufferHeight = newGameWindowHeight;
    }
    #endregion
}