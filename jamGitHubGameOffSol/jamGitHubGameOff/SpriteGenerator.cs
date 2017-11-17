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
        public Texture2D SpritePicture { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public double CurrentFrame { get; set; }
        public string ParseQuads { get; set; } // back or forth if there is half of the animation
        public Rectangle SourceQuad { get; set; }
        public Rectangle DestinationPosition { get; set; }
        public Vector2 SpriteOrigin { get; set; }
        public double SpeedAnimation { get; set; }
        public int SpeedAction { get; set; }
        public EnumSpriteDirection CharacterDirection { get; set; }
        public SpriteEffects SpriteDirection { get; set; }

        public SpriteGenerator(Texture2D pSpritePicture, int pFrameNumber)
        {
            SpritePicture = pSpritePicture;
            FrameHeight = SpritePicture.Height;
            FrameWidth = SpritePicture.Width / pFrameNumber;
            CurrentFrame = 0;
            ParseQuads = "forth";
            SourceQuad = new Rectangle(0, 0, FrameWidth, FrameHeight);
            DestinationPosition = new Rectangle(0, 0, FrameWidth, FrameHeight);
            SpriteOrigin = new Vector2(FrameWidth / 2, FrameHeight / 2);
            SpeedAnimation = 1;
            SpeedAction = 1;
            CharacterDirection = EnumSpriteDirection.Right;
            SpriteDirection = SpriteEffects.None;
        }

        public void SpriteGeneratorUpdate(GameTime pGameTime)
        {

        }

        public void SpriteGeneratorDraw(GameTime pGameTime)
        {

        }
    }
}
