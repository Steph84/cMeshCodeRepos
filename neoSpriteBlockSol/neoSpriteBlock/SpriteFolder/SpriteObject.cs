using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteObject
{
    public Rectangle Position { get; set; }

    protected Texture2D SpriteData { get; set; }
    protected Rectangle SourceQuad { get; set; }
    protected SpriteEffects SpriteEffect { get; set; }

    private Vector2 SpriteOrigin { get; set; }

    public SpriteObject(Rectangle pPosition, string pSpriteName)
    {
        SpriteOrigin = new Vector2();
    }

    #region SpriteUpdate
    public virtual void SpriteUpdate(GameTime pGameTime) { }
    #endregion

    #region SpriteDraw
    public virtual void SpriteDraw(GameTime pGameTime) { }
    #endregion
}
