using System;

class Tweening
{
    public double Time { get; set; }
    public double Duration { get; set; }

    public enum DirectionObject
    {
        None,
        In,
        Out
    }

    public Tweening(double pTime = 0, double pDuration = 1)
    {
        InitializeTweening(pTime, pDuration);
    }

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
        temp = (float)(-distance * Math.Cos(currentTime / duration * (Math.PI / 2)) + startValue + distance);
        return temp;
    }

    // Method to initialize parameters of tweening
    public void InitializeTweening(double pInitializeTime = 0, double pInitializeDuration = 1)
    {
        Time = pInitializeTime;
        Duration = pInitializeDuration;
    }
}
