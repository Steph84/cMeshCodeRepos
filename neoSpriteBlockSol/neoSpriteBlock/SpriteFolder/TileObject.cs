using Microsoft.Xna.Framework;

class TileObject: SpriteObject
{
    public int Id { get; set; }
    public Vector2 Coordinate { get; set; }

    public TileObject(Rectangle pPosition, string pSpriteName) : base(pPosition, pSpriteName)
    {
        Position = pPosition;
    }
}
