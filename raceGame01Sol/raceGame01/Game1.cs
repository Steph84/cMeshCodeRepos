using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace raceGame01
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D map01;
        Texture2D car01;
        Vector2 posMap;
        Vector2 posCar;
        Vector2 originCar;
        Vector2 speed = Vector2.Zero;
        Vector2 gForce = Vector2.Zero;
        int spriteSize = 128;
        float carDir = 0;
        int maxSpeed = 10;
        float gForcePond = 0.1f;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            float initX = -26.5f;
            float initY = -6.5f;
            posMap = new Vector2(initX*spriteSize, initY*spriteSize);
            posCar = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2);
            originCar = new Vector2(car01.Width / 2, 0);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map01 = this.Content.Load<Texture2D>("raceMap01");
            car01 = this.Content.Load<Texture2D>("car_red_small_3");
            
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            KeyboardState state = Keyboard.GetState();
            
           
            if (state.IsKeyDown(Keys.Down))
            {
                speed.X = (float)(maxSpeed * Math.Sin(-carDir));
                speed.Y = (float)(maxSpeed * Math.Cos(-carDir));
                posMap.X = posMap.X - speed.X;
                posMap.Y = posMap.Y - speed.Y;
                originCar.Y = 3 * car01.Height / 5;
            }

            
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up))
            {
                speed.X = (float)(maxSpeed * Math.Sin(-carDir));
                speed.Y = (float)(maxSpeed * Math.Cos(-carDir));
                posMap.X = posMap.X + speed.X + gForce.X;
                posMap.Y = posMap.Y + speed.Y + gForce.Y;
                originCar.Y = 2 * car01.Height / 5;
                carDir += 0.07f;
                gForce.X = speed.X * gForcePond;
                gForce.Y = -speed.Y * gForcePond;
            }
            else if(state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up))
            {
                speed.X = (float)(maxSpeed * Math.Sin(-carDir));
                speed.Y = (float)(maxSpeed * Math.Cos(-carDir));
                posMap.X = posMap.X + speed.X + gForce.X;
                posMap.Y = posMap.Y + speed.Y + gForce.Y;
                originCar.Y = 2 * car01.Height / 5;
                carDir -= 0.07f;
                gForce.X = -speed.X * gForcePond;
                gForce.Y = speed.Y * gForcePond;
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                speed.X = (float)(maxSpeed * Math.Sin(-carDir));
                speed.Y = (float)(maxSpeed * Math.Cos(-carDir));
                posMap.X = posMap.X + speed.X;
                posMap.Y = posMap.Y + speed.Y;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(map01, posMap);
            spriteBatch.Draw(car01, posCar, null, Color.White, carDir, originCar, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
