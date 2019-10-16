using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Main : Game
{
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static Viewport viewport;
    public static GameWindow gameWindow;
    public static DisplayMode currentDisplayMode;

    public WindowDimension MyWindow { get; set; }
    private string MyTitleGameWindow = "NeoBlock";

    public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
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
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }
    
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }
    
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}