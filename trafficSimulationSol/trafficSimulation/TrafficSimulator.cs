using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// This is the main type for your game.
/// </summary>
public class TrafficSimulator : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public TrafficSimulator()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = 1152;
        graphics.PreferredBackBufferHeight = 640;
        graphics.ApplyChanges();

        base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);

        #region Load json file for map
        string myDirectory = "";
        // if exe has the json file in the same location
        myDirectory = Environment.CurrentDirectory;
        if (!File.Exists(myDirectory + "/map.json"))
        {
            // if the exe is in the release or debug directories
            myDirectory = Directory.GetParent(myDirectory).Parent.Parent.Parent.FullName;
        }
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(myDirectory + "/map.json")));
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Map));
        Map MyMap = (Map)ser.ReadObject(stream);
        #endregion
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
