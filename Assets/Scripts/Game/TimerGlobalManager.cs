using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimerGlobalManager : MonoBehaviour
{
    public static TimerGlobalManager StaticClass;
    private List<KeyValuePair<ITimerUser, TimerHandle>> TimerPairs;

    private void Awake()
    {
        if (StaticClass == null)
        {
            TimerPairs = new List<KeyValuePair<ITimerUser, TimerHandle>>();
            StaticClass = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetTimer(TimerHandle handle, ITimerUser rootObject, bool isRepeater = false)
    {
        TimerPairs.Add(new KeyValuePair<ITimerUser, TimerHandle>(rootObject, handle));
    }

    public void StopTimer(TimerHandle handle, ITimerUser rootObject)
    {
        var timerPair = TimerPairs.FirstOrDefault(pair => pair.Key == rootObject && pair.Value == handle);
        if (!timerPair.Equals(default(KeyValuePair<ITimerUser, TimerHandle>)))
        {
            TimerPairs.Remove(timerPair);
        }
    }

    private void Update()
    {
        for (int i = TimerPairs.Count - 1; i >= 0; i--)
        {
            var pair = TimerPairs[i];
            var value = pair.Value;

            value.maxTime -= Time.deltaTime;
            if (value.maxTime <= 0)
            {
                value.TimerEndCallBack();
                TimerPairs.RemoveAt(i);
            }
        }
    }
}