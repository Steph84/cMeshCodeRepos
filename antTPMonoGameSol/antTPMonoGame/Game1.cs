using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace antTPMonoGame
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D antPic; // declarer le membre
        int picWidth;
        int picHeight;
        Vector2 picOrigin;
        Vector2 antPosition;
        float antRot;
        string antDir;
        int antSpeed;
        string antMov;
        Random antChoice;
        int antRdSeed;
        float myPI = 3.14159f;

        public Game1()
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            antPic = this.Content.Load<Texture2D>("ant01");
            picWidth = antPic.Width;
            picHeight = antPic.Height;
            picOrigin = new Vector2(picWidth / 2, picHeight / 2);

            antPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            antRot = 0;
            antDir = "North";
            antSpeed = 2;
            antMov = "OnTheMove";

        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            antRdSeed = Environment.TickCount + (int)antPosition.X;
            antChoice = new Random(antRdSeed);
            
            if (antMov == "OnTheMove")
            {
                if (antDir == "North")
                {
                    antRot = 0;
                    antPosition.Y = antPosition.Y - antSpeed;
                    if (antPosition.Y < (0 + picOrigin.Y + 10))
                    {
                        antMov = "Stopped";
                    }
                }

                if (antDir == "South")
                {
                    antRot = myPI;
                    antPosition.Y = antPosition.Y + antSpeed;
                    if (antPosition.Y > (GraphicsDevice.Viewport.Height - picOrigin.Y - 10))
                    {
                        antMov = "Stopped";
                    }
                }

                if (antDir == "West")
                {
                    antRot = - myPI/2;
                    antPosition.X = antPosition.X - antSpeed;
                    if (antPosition.X < (0 + picOrigin.X + 10))
                    {
                        antMov = "Stopped";
                    }
                }

                if (antDir == "East")
                {
                    antRot = myPI/2;
                    antPosition.X = antPosition.X + antSpeed;
                    if (antPosition.X > (GraphicsDevice.Viewport.Width - picOrigin.X - 10))
                    {
                        antMov = "Stopped";
                    }
                }
            }

            if (antMov == "Stopped")
            {
                int tempRd = antChoice.Next(1, (8 + 1));
                switch (tempRd)
                {
                    case 1:
                        antDir = "North";
                        antMov = "OnTheMove";
                        break;
                    case 2:
                        antDir = "NorthEast";
                        antMov = "OnTheMove";
                        break;
                    case 3:
                        antDir = "East";
                        antMov = "OnTheMove";
                        break;
                    case 4:
                        antDir = "SouthEast";
                        antMov = "OnTheMove";
                        break;
                    case 5:
                        antDir = "South";
                        antMov = "OnTheMove";
                        break;
                    case 6:
                        antDir = "SouthWest";
                        antMov = "OnTheMove";
                        break;
                    case 7:
                        antDir = "West";
                        antMov = "OnTheMove";
                        break;
                    case 8:
                        antDir = "NorthWest";
                        antMov = "OnTheMove";
                        break;
                }
            }
            

            base.Update(gameTime);
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green); // background color

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(antPic, antPosition, null, Color.White, antRot, picOrigin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
