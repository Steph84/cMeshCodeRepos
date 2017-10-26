using System;

namespace blockMenu
{
    public class Tweening
    {
        public double EaseOutSin(double t, double b, double c, double d)
        {
            double temp = 0;
            temp = c * Math.Sin(t / d * (Math.PI / 2)) + b;
            return temp;
        }
    }
}