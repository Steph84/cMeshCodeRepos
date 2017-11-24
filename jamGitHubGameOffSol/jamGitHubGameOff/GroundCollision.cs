using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jamGitHubGameOff
{
    public class GroundCollision
    {
        public static Rectangle StickToTheGround(Rectangle ObjectPosition, List<Vector2> ListMapPoints)
        {
            Rectangle temp = new Rectangle();

            // find the 2 points around DK
            Vector2 leftBoundary = ListMapPoints.Where(x => ObjectPosition.X >= x.X).LastOrDefault();
            if (leftBoundary == null)
                leftBoundary = ListMapPoints.First();
            Vector2 rightBoundary = ListMapPoints.Where(x => ObjectPosition.X < x.X).FirstOrDefault();
            if (rightBoundary == null)
                rightBoundary = ListMapPoints.Last();

            // compute equation coeff
            double a = (rightBoundary.Y - leftBoundary.Y) / (rightBoundary.X - leftBoundary.X);
            double b = leftBoundary.Y - leftBoundary.X * a;

            // modify DK y
            ObjectPosition.Y = (int)(ObjectPosition.X * a + b - ObjectPosition.Height / 2);

            temp = ObjectPosition;
            return temp;
        }
    }
}
