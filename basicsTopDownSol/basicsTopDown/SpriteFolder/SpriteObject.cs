using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace basicsTopDown.SpriteFolder
{
    public class SpriteObject
    {
        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }

        protected double GameSizeCoefficient { get; set; }
        protected Rectangle Size { get; set; }
        protected ContentManager Content { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        
        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, double pGameSizeCoefficient)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            GameSizeCoefficient = pGameSizeCoefficient;
        }

        #region SpriteUpdate
        public virtual void SpriteUpdate(GameTime pGameTime, Map pMap) { }
        #endregion

        public virtual void SpriteDraw(GameTime pGameTime) { }
    }
}
