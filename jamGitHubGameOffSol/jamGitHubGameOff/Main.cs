using jamGitHubGameOff.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace jamGitHubGameOff
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WindowDimension MyWindow;
        Menu MyMenu;
        GameClass MyGameClass;

        Tuple<int, int> GameWindowSize;
        private string MyTitleGameWindow = "This is a game !";
        private EnumMainState MyState = EnumMainState.MenuTitle;
        public float GameSizeCoefficient = 1.0f;

        public enum EnumMainState
        {
            MenuTitle,
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
            
            // update Main properties
            GameWindowSize = new Tuple<int, int>(MyWindow.GameWindowWidth, MyWindow.GameWindowHeight);
            GameSizeCoefficient = MyWindow.GameSizeCoefficient;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MyMenu = new Menu(GameWindowSize, Content, spriteBatch);
            MyGameClass = new GameClass(GameWindowSize, Content, spriteBatch);
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

                case EnumMainState.MenuCredits:
                    MyState = MyMenu.MenuCreditsUpdate(gameTime, MyState);
                    break;

                case EnumMainState.GameAnimation:
                    // animation
                    break;

                case EnumMainState.GamePlayable:
                    // let's play
                    MyGameClass.GameClassUpdate(gameTime);
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
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (MyState)
            {
                case EnumMainState.MenuTitle:
                    MyMenu.MenuTitleDraw(gameTime);
                    break;

                case EnumMainState.MenuCredits:
                    MyMenu.MenuCreditsDraw(gameTime);
                    break;

                case EnumMainState.GameAnimation:
                    // animation
                    break;

                case EnumMainState.GamePlayable:
                    // let's play
                    MyGameClass.GameClassDraw(gameTime);
                    break;

                default:
                    break;
            }
            
            spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }
}