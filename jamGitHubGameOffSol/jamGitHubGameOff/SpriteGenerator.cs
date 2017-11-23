using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jamGitHubGameOff
{
    public enum EnumSpriteDirection
    {
        Right = 1,
        Left = -1
    }

    public class SpriteGenerator
    {
        SpriteBatch SpriteBatch;

        public Texture2D SpritePicture { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int FrameNumber { get; set; }
        public double CurrentFrame { get; set; }
        public string ParseQuads { get; set; } // back or forth if there is half of the animation
        public Rectangle SourceQuad { get; set; }
        public Vector2 SpriteOrigin { get; set; }
        public double SpeedAnimation { get; set; }
        public SpriteEffects SpriteDirection { get; set; }
        public bool HasCompleteAnimation { get; set; }

        public SpriteGenerator(SpriteBatch pSpriteBatch, Texture2D pSpritePicture, int pFrameNumber, bool pHasCompleteAnimation)
        {
            SpriteBatch = pSpriteBatch;
            SpritePicture = pSpritePicture;
            FrameNumber = pFrameNumber;
            HasCompleteAnimation = pHasCompleteAnimation;

            FrameHeight = SpritePicture.Height;
            FrameWidth = SpritePicture.Width / pFrameNumber;
            CurrentFrame = 0;
            ParseQuads = "forth";
            SourceQuad = new Rectangle(0, 0, FrameWidth, FrameHeight);
            SpriteOrigin = new Vector2(FrameWidth / 2, FrameHeight / 2);
            SpeedAnimation = 1;
            SpriteDirection = SpriteEffects.None;
        }

        public void SpriteGeneratorUpdate(GameTime pGameTime, EnumSpriteDirection pCharacterDirection)
        {
            #region Manage the sprite direction in relation to the character direction
            if(pCharacterDirection == EnumSpriteDirection.Right)
                SpriteDirection = SpriteEffects.None;

            if (pCharacterDirection == EnumSpriteDirection.Left)
                SpriteDirection = SpriteEffects.FlipHorizontally;
            #endregion

            #region Manage the back and forth movement if the tileSet doesn t have the complete animation
            if (HasCompleteAnimation == false)
            {
                if (ParseQuads == "forth")
                    CurrentFrame = CurrentFrame + (SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

                if (ParseQuads == "back")
                    CurrentFrame = CurrentFrame - (SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);

                if (CurrentFrame > FrameNumber - 1)
                {
                    CurrentFrame = FrameNumber - 1;
                    ParseQuads = "back";
                }

                if (CurrentFrame < 0)
                {
                    CurrentFrame = 0;
                    ParseQuads = "forth";
                }
            }
            #endregion

            #region Manage the movement with a complete animation
            if (HasCompleteAnimation == true)
            {
                CurrentFrame = CurrentFrame + (SpeedAnimation * pGameTime.ElapsedGameTime.Milliseconds / 1000.0d);
                
                if (CurrentFrame > FrameNumber - 1)
                    CurrentFrame = 0;
            }
            #endregion
            
            SourceQuad = new Rectangle((int)Math.Floor(CurrentFrame) * FrameWidth, SourceQuad.Y, SourceQuad.Width, SourceQuad.Height);
        }

        public void SpriteGeneratorDraw(GameTime pGameTime, Rectangle pDonkeyKongPosition)
        {
            SpriteBatch.Draw(SpritePicture, pDonkeyKongPosition, SourceQuad, Color.White, 0, SpriteOrigin, SpriteDirection, 0);
        }
    }
}
