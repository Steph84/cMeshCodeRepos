using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Main : Game
{
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static Viewport viewport;
    public static GameWindow gameWindow;
    public static DisplayMode currentDisplayMode;
    public static ContentManager content;

    public WindowDimension MyWindow { get; set; }
    public Menu MyMenu { get; set; }
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
        graphics = new GraphicsDeviceManager(this);
        content = Content;
        content.RootDirectory = "Content";
    }
    
    protected override void Initialize()
    {
        Window.Title = MyTitleGameWindow;
        currentDisplayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        viewport = GraphicsDevice.Viewport;
        gameWindow = Window;
        MyWindow = new WindowDimension();

        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        MyMenu = new Menu();
    }
    
    protected override void UnloadContent() { }
    
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch (MyState)
        {
            case EnumMainState.MenuTitle:
                //MyState = MyMenu.MenuTitleUpdate(gameTime, MyState);
                break;
            case EnumMainState.MenuInstructions:
                break;
            case EnumMainState.MenuCredits:
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // SamplerState.PointClamp to avoid blur from rescaling pixel art
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);




        spriteBatch.End();

        base.Draw(gameTime);
    }
}