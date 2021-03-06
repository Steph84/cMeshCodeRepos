﻿using basicsTopDown.MenuFolder;
using basicsTopDown.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        private WindowDimension MyWindow { get; set; }
        private Menu MyMenu { get; set; }
        private GameRun MyGame { get; set; }

        private string MyTitleGameWindow = "TopDown basics";
        private EnumMainState MyState = EnumMainState.MenuTitle;
        private double GameSizeCoefficient = 1.0d;
        
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
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = MyTitleGameWindow;

            MyWindow = new WindowDimension(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode,
                                           GraphicsDevice.Viewport,
                                           Window,
                                           graphics);

            // update GameSizeCoefficient properties
            GameSizeCoefficient = MyWindow.GameSizeCoefficient;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MyMenu = new Menu(MyWindow, Content, spriteBatch);
            MyGame = new GameRun(MyWindow, Content, spriteBatch);
        }

        protected override void UnloadContent()
        {
        }

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

                case EnumMainState.GameAnimation:
                    // animation
                    break;

                case EnumMainState.GamePlayable:
                    MyGame.GameRunUpdate(gameTime, MyState);
                    break;

                case EnumMainState.MenuQuit:
                    Exit();
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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

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

                case EnumMainState.GameAnimation:
                    // animation
                    break;

                case EnumMainState.GamePlayable:
                    MyGame.GameRunDraw(gameTime);
                    break;

                default:
                    break;
            }
            
            spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }
}