using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jamGitHubGameOff
{
    public class CollisionClass
    {
        public bool IsCollide { get; set; }

        public bool CheckCollision(Rectangle pFirstObject, Rectangle pSecondObject)
        {
            if (pFirstObject == pSecondObject)
                return false;

            int dx = pFirstObject.X - pSecondObject.X;
            int dy = pFirstObject.Y - pSecondObject.Y;

            //if (Math.Abs(dx) < (pFirstObject.Width * pFirstObject.scale + pSecondObject.Width * pSecondObject.scale))
            //{
            //    if (Math.Abs(dy) < (pFirstObject.Height * pFirstObject.scale + pSecondObject.Height * pSecondObject.scale))

            if (Math.Abs(dx) < (pFirstObject.Width + pSecondObject.Width))
            {
                if (Math.Abs(dy) < (pFirstObject.Height + pSecondObject.Height))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
