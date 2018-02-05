using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown.CharacterFolder
{
    [Flags]
    public enum EnumCharacterDirection
    {
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
    }

    public class CharacterObject : SpriteObject
    {
        public string Name { get; set; }
        public bool HasMirror { get; set; }
        public Rectangle FrameSize { get; set; }
        public int FrameNumber { get; set; }
        public Rectangle SourceQuad { get; set; }
        public EnumCharacterDirection Direction { get; set; }

        // for animation 
        private double CurrentFrame { get; set; }
        private double SpeedAnimation { get; set; }
        private SpriteEffects SpriteDirection { get; set; }

        Vector2 SpriteOrigin = new Vector2();

        // properties for the movement

        public CharacterObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {
            FrameSize = pFrameSize;
            SpriteData = Content.Load<Texture2D>(pSpriteName);
            Direction = EnumCharacterDirection.East;

            // for animation
            CurrentFrame = 0;
            SpeedAnimation = 1;
            SpriteDirection = SpriteEffects.None;

            #region Determine the line number
            // an animation tileSet is made of 3 or 4 lines
            // 1 animation toward top
            // 2 animation toward right (could be the mirror of the left)
            // 3 animation toward bottom
            // 4 if exist animation toward left

            if (SpriteData.Height / FrameSize.Height == 3)
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

        public void CharacterUpdate(GameTime pGameTime)
        {
            // en fonction de la direction, quelle ligne choisir
            if(IsMoving)
            {

            }
            else
            {
                if(HasMirror) // if there is only 3 lines
                {
                    if(Direction == EnumCharacterDirection.West)
                    {
                        SpriteDirection = SpriteEffects.FlipHorizontally; // correct the Direction
                    }
                    else
                    {
                        SpriteDirection = SpriteEffects.None;
                    }
                }
                else
                {
                    // nothing to do
                }
                SourceQuad = new Rectangle(0, (int)Direction, SourceQuad.Width, SourceQuad.Height);
            }
            

            switch (Direction)
            {
                case EnumCharacterDirection.None:
                    break;
                case EnumCharacterDirection.North:
                    SourceQuad = new Rectangle(0, 0, SourceQuad.Width, SourceQuad.Height);
                    break;
                case EnumCharacterDirection.East:
                    SourceQuad = new Rectangle(0, 1 * FrameSize.Height, SourceQuad.Width, SourceQuad.Height);
                    break;
                case EnumCharacterDirection.South:
                    SourceQuad = new Rectangle(0, 0, SourceQuad.Width, SourceQuad.Height);
                    break;
                case EnumCharacterDirection.West:
                    SourceQuad = new Rectangle(0, 0, SourceQuad.Width, SourceQuad.Height);
                    break;
                default:
                    break;
            }

            // en fontion du temps quelle frame choisir
        }
        
        public void CharacterDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, SourceQuad, Color.White, 0, SpriteOrigin, SpriteDirection, 0);
        }
    }
}
