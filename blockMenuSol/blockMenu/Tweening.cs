using System;

namespace blockMenu
{
    public class Tweening
    {
        public double Time { get; set; }
        public double Duration { get; set; }

        //public Tweening(double pTime, double pDuration)
        //{
        //    Time = pTime;
        //    Duration = pDuration;
        //}

        // current time, start value, change in value (distance), duration
        public float EaseOutSin(double currentTime, double startValue, double distance, double duration)
        {
            float temp = 0;
            temp = (float)(distance * Math.Sin(currentTime / duration * (Math.PI / 2)) + startValue);
            return temp;
        }

        public float EaseInSin(double currentTime, double startValue, double distance, double duration)
        {
            float temp = 0;
            temp = (float)(- distance * Math.Cos(currentTime / duration * (Math.PI / 2)) + startValue + distance);
            return temp;
        }
    }
}