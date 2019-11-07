using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Main : Game
{
    public static GraphicsDeviceManager GlobalGraphics;
    public static SpriteBatch GlobalSpriteBatch;
    public static Viewport GlobalViewport;
    public static GameWindow GlobalGameWindow;
    public static DisplayMode GlobalCurrentDisplayMode;
    public static ContentManager GlobalContent;

    public WindowDimension MyWindow { get; set; }
    public Menu MyMenu { get; set; }
    private bool MenuHaveTweening = true;
    private string MyTitleGameWindow = "NeoBlock";
    private EnumMainState MyState = EnumMainState.MenuTitle;

    public enum EnumMainState
    {
        MenuTitle,
        MenuInstructions,
        MenuCredits,
        MenuQuit,
        GameAnimation,
        GamePlayable
    }

    public Main()
    {
        GlobalGraphics = new GraphicsDeviceManager(this);
        GlobalContent = Content;
        GlobalContent.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        Window.Title = MyTitleGameWindow;
        GlobalCurrentDisplayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        GlobalViewport = GraphicsDevice.Viewport;
        GlobalGameWindow = Window;
        MyWindow = new WindowDimension();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        GlobalSpriteBatch = new SpriteBatch(GraphicsDevice);
        
        MyMenu = new Menu(MenuHaveTweening);
    }

    protected override void UnloadContent() { }

    protected override void Update(GameTime gameTime)
    {
        switch (MyState)
        {
            case EnumMainState.MenuTitle:
                MyState = MyMenu.MenuTitleUpdate(gameTime, MyState);
                break;
            case EnumMainState.MenuInstructions:
                MyState = MyMenu.MenuInstructionsUpdate(gameTime, MyState);
                break;
            case EnumMainState.MenuCredits:
                MyState = MyMenu.MenuCreditsUpdate(gameTime, MyState);
                break;
            case EnumMainState.MenuQuit:
                Exit();
                break;
            case EnumMainState.GameAnimation:
                // animation
                break;
            case EnumMainState.GamePlayable:
                //MyGame.GameRunUpdate(gameTime, MyState);
                break;
            default:
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightGreen);

        // SamplerState.PointClamp to avoid blur from rescaling pixel art
        GlobalSpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        switch (MyState)
        {
            case EnumMainState.MenuTitle:
                MyMenu.MenuTitleDraw(gameTime);
                break;
            case EnumMainState.MenuInstructions:
                MyMenu.MenuInstructionsDraw(gameTime);
                break;
            case EnumMainState.MenuCredits:
                MyMenu.MenuCreditsDraw(gameTime);
                break;
            case EnumMainState.MenuQuit:
                break;
            case EnumMainState.GameAnimation:
                break;
            case EnumMainState.GamePlayable:
                break;
            default:
                break;
        }
        
        GlobalSpriteBatch.End();

        base.Draw(gameTime);
    }
}