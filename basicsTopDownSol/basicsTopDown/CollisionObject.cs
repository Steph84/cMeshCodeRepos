using Microsoft.Xna.Framework;
using System;

namespace basicsTopDown
{
    public class CollisionObject
    {
        public static bool CheckCollision(Rectangle pFirstObject, Rectangle pSecondObject)
        {
            if (pFirstObject == pSecondObject)
                return false;

            int dx = pFirstObject.X - pSecondObject.X;
            int dy = pFirstObject.Y - pSecondObject.Y;

            // if dx < 0, it's 1st / 2nd
            // if dx > 0, it's 2nd / 1st

            // if dy < 0, it's 1st
            //                 2nd
            // if dy > 0, it's 2nd
            //                 1st

            bool hitAlongX = false;
            bool hitAlongY = false;

            if (dx < 0)
            {
                if (Math.Abs(dx) < pFirstObject.Width)
                {
                    hitAlongX = true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Math.Abs(dx) < pSecondObject.Width)
                {
                    hitAlongX = true;
                }
                else
                {
                    return false;
                }
            }
            
            if (dy < 0)
            {
                if (Math.Abs(dy) < pFirstObject.Height)
                {
                    hitAlongY = true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Math.Abs(dy) < pSecondObject.Height)
                {
                    hitAlongY = true;
                }
                else
                {
                    return false;
                }
            }

            //if (Math.Abs(dx) < Math.Max(pFirstObject.Width, pSecondObject.Width))
            //{
            //    if (Math.Abs(dy) < Math.Max(pFirstObject.Height, pSecondObject.Height))
            //    {
            //        return true;
            //    }
            //}

            return hitAlongX && hitAlongY;
        }
    }
}
