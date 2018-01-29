using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace basicsTopDown.CharacterFolder
{
    public class Player : CharacterObject
    {
        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {

        }

        public void PlayerUpdate()
        {
            // control the player to move
        }
        
    }
}
