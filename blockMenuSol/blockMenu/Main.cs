﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace blockMenu
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WindowDimension MyWindow;
        Menu MyMenu;

        Tuple<int, int> GameWindowSize;
        private string MyTitleGameWindow = "This is a game !";

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = MyTitleGameWindow;

            MyWindow = new WindowDimension(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode,
                                           GraphicsDevice.Viewport,
                                           Window,
                                           graphics);

            GameWindowSize = new Tuple<int, int>(MyWindow.GameWindowWidth, MyWindow.GameWindowHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MyMenu = new Menu(GameWindowSize, Content, spriteBatch);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MyMenu.MenuUpdate(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();


            MyMenu.MenuDraw(gameTime);


            spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }
}