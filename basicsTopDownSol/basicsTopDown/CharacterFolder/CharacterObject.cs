using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown.CharacterFolder
{
    public class CharacterObject : SpriteObject
    {
        public string Name { get; set; }
        public bool HasMirror { get; set; }
        public Rectangle FrameSize { get; set; }
        public int FrameNumber { get; set; }
        public Rectangle SourceQuad { get; set; }

        Vector2 SpriteOrigin = new Vector2();

        // properties for the movement

        public CharacterObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {
            FrameSize = pFrameSize;
            SpriteData = Content.Load<Texture2D>(pSpriteName);

            #region Determine the line number
            // an animation tileSet is made of 3 or 4 lines
            // 1 animation toward top
            // 2 animation toward right (could be the mirror of the left)
            // 3 animation toward bottom
            // 4 if exist animation toward left

            if(SpriteData.Height / FrameSize.Height == 3)
            {
                HasMirror = true;
            }
            else if(SpriteData.Height / FrameSize.Height == 4)
            {
                HasMirror = false;
            }
            else
            {
                throw new System.Exception("The animation tileSet don't have the good format");
            }
            #endregion

            #region Determine the frame number
            if ((SpriteData.Width / FrameSize.Width) % 1 == 0)
            {
                FrameNumber = SpriteData.Width / FrameSize.Width;
            }
            else
            {
                throw new System.Exception("The frame number is not an integer");
            }
            #endregion

            int spriteWidthShowing = (int)Math.Round(FrameSize.Width * pGameSizeCoefficient, MidpointRounding.AwayFromZero);
            int spriteHeightShowing = (int)Math.Round(FrameSize.Height * pGameSizeCoefficient, MidpointRounding.AwayFromZero);

            Size = new Rectangle(0, 0, spriteWidthShowing, spriteHeightShowing);
            Position = new Rectangle(pPosition.X, pPosition.Y, spriteWidthShowing, spriteHeightShowing);
            SourceQuad = new Rectangle(0, 0, FrameSize.Width, FrameSize.Height);
        }
        
        public void CharacterDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, SourceQuad, Color.White, 0, SpriteOrigin, SpriteEffects.None, 0);
        }
    }
}
