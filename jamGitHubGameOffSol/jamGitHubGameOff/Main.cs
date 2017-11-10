using jamGitHubGameOff.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace jamGitHubGameOff
{
    public class Main : Game
    {
        Texture2D line1;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WindowDimension MyWindow;
        Menu MyMenu;
        GameClass MyGameClass;

        Tuple<int, int> GameWindowSize;
        string MyTitleGameWindow = "This is a game !";
        //EnumMainState MyState = EnumMainState.MenuTitle;
        EnumMainState MyState = EnumMainState.GamePlayable;
        
        float GameSizeCoefficient = 1.0f;

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
            
            line1 = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            line1.SetData(new[] { Color.White });
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

            spriteBatch.Draw(line1, new Rectangle(0, 207, 100, 1), Color.White);
            for (int i = 0; i < 40; i++)
            {
                spriteBatch.Draw(line1, new Rectangle((int)Math.Round(100 + i * 4.0, 0, MidpointRounding.AwayFromZero), 207 + i, 1, 1), Color.White);
            }

            spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }
}