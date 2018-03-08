using Microsoft.Xna.Framework;
using System;

namespace blockAStarAlgo
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

            #region Check DX
            if (dx <= 0)
            {
                if (Math.Abs(dx) <= pFirstObject.Width)
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
                if (Math.Abs(dx) <= pSecondObject.Width)
                {
                    hitAlongX = true;
                }
                else
                {
                    return false;
                }
            }
            #endregion

            #region Check DY
            if (dy <= 0)
            {
                if (Math.Abs(dy) <= pFirstObject.Height)
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
                if (Math.Abs(dy) <= pSecondObject.Height)
                {
                    hitAlongY = true;
                }
                else
                {
                    return false;
                }
            }
            #endregion

            return hitAlongX && hitAlongY;
        }
    }
}
