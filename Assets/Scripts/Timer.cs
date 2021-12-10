using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// keeps track of how much time has passed. Use it to tell when time has passed a given duration
/// </summary>
public class Timer 
{
    float startTime;

    public Timer()
    {
        startTime = Time.time;
    }


    public void ResetTimer()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// returns true if this timer has been running longer than the given duration.
    /// Automatically resets when done, unless otherwise specified
    /// </summary>
    public bool CheckTimer(float duration, bool resetWhenDone = true)
    {
        float currentTime = Time.time - startTime;
        bool isTimerDone = currentTime >= duration;

        if(resetWhenDone && isTimerDone)
            ResetTimer();

        return isTimerDone;
    }
}
