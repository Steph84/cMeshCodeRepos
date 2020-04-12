using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static TileSetData;

public class AlgoKingDomino : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    TileSetData MyTileSetData;
    Plateau MyPlateau;
    List<Tuile> TuilesDeDepart;
    List<Tuile> TileSetToUse;

    public AlgoKingDomino()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        MyTileSetData = new TileSetData();
        MyPlateau = new Plateau();

        // initialization tile set
        TuilesDeDepart = MyTileSetData.LoadHardData();

        TileSetToUse = MyTileSetData.TilesShuffle(TuilesDeDepart);

        MyPlateau.ComputePossibleCases(MyPlateau.BluePlayer);
    }

    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    protected override void Update(GameTime gameTime)
    {



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