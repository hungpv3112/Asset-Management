using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateTimeController : MonoBehaviour
{
    public DateTime nextDay;
    public DateTime nextDayUTC;
    public static Action<DateTime> OnStartNewDay = delegate { };
    public static Action<DateTime> OnStartNewDayUTC = delegate { };

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        nextDay = GetTheNextDay();

        nextDayUTC = GetTheNextDayUTC();
    }

    private void ResetTimeNewDay()
    {
        nextDay = GetTheNextDay();

        OnStartNewDay?.Invoke(nextDay);
    }

    private void ReseTargetUTCTime()
    {
        nextDayUTC = GetTheNextDayUTC();

        OnStartNewDayUTC(nextDayUTC);
    }

    private static DateTime GetTheNextDay()
    {
        return DateTimeUtils.Now.AddDays(1).Date;
    }

    private static DateTime GetTheNextDayUTC()
    {
        return DateTimeUtils.UtcNow.AddDays(1).Date;
    }

    private float _time = 0;
    private void Update() {
        _time += Time.deltaTime;
        if (_time < 1)
        {
            return;
        }

        var now = DateTimeUtils.Now;
        var utcNow = DateTimeUtils.UtcNow;
        if (nextDay < now)
            ResetTimeNewDay();

        if (nextDayUTC < utcNow)
            ReseTargetUTCTime();
        
        _time = 0;
    }
}
