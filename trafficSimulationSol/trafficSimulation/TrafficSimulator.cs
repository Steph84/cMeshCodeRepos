using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// This is the main type for your game.
/// </summary>
public class TrafficSimulator : Game
{
    public static GraphicsDeviceManager GlobalGraphics;
    public static SpriteBatch spriteBatch;
    public static ContentManager GlobalContent;
    public static Map MyMap;
    public static Fleet MyFleet;

    public TrafficSimulator()
    {
        GlobalGraphics = new GraphicsDeviceManager(this);
        GlobalContent = Content;
        GlobalContent.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
        GlobalGraphics.PreferredBackBufferWidth = 1152;
        GlobalGraphics.PreferredBackBufferHeight = 640;
        GlobalGraphics.ApplyChanges();

        MyMap = new Map();
        MyFleet = new Fleet(1);

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

        MyMap.InitializeMap();

        int a = 2;
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

        MyFleet.FleetUpdate(gameTime);

        base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // SamplerState.PointClamp to avoid blur from rescaling pixel art
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        MyMap.MapDraw(gameTime);
        MyFleet.FleetDraw(gameTime);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}

public static class Constantes
{
    public const int RowNumber = 20;
    public const int ColumnNumber = 32; // 36 - 4 (hud)
    public const int SquareNumbers = RowNumber * ColumnNumber;
    public const int SquareSize = 32;
    public static readonly int[] TilesTurn = { 3, 6, 9, 12 };
    public static readonly int[] TilesTurnAround = { 1, 2, 4, 8 };
    public static readonly int[] TilesTTurn = { 3, 6, 9, 12 };
}
