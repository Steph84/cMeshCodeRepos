using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace blockMenu
{
    public class Menu : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //int windowWidth = 800; // default
        //int windowHeight = 480; // default
        int windowWidth = 960; // optimize
        int windowHeight = 576; // optimize
        SpriteFont font;
        string title1 = "Title 01 oooooooooooooooooooooooooooooooooooooooooooooooooo";

        public Menu()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            WindowDimension MyWindow =
                new WindowDimension(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode,
                                    GraphicsDevice.Viewport,
                                    Window,
                                    graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("title01");
            Vector2 size = font.MeasureString(title1);
            


        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, title1, new Vector2(100, 100), Color.Black);
            spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }
}
