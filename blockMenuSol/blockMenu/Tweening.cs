using System;

namespace blockMenu
{
    public class Tweening
    {
        // current time, start value, change in value (distance), duration
        public float EaseOutSin(double currentTime, double startValue, double distance, double duration)
        {
            float temp = 0;
            temp = (float)(distance * Math.Sin(currentTime / duration * (Math.PI / 2)) + startValue);
            return temp;
        }
    }
}