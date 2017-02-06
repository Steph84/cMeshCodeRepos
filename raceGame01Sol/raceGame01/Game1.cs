using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        int spriteSize = 128;

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
            originCar = new Vector2(car01.Width / 2, car01.Height / 2);
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
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(map01, posMap);
            spriteBatch.Draw(car01, posCar, null, Color.White, 0, originCar, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
