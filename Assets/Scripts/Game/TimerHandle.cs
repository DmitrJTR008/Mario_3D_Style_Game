using System;
public class TimerHandle
{
    public event Action OnTimerEnd;
    public bool isDown;
    public float maxTime;
    public float timeSecond;

    public void TimerEndCallBack()
    {
        OnTimerEnd?.Invoke();
        OnTimerEnd = null;
    }
}