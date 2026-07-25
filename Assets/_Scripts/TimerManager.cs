using System;
using System.Collections;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static event Action TimerFinished;
    public float CurrentTime => _currentTime;
    private float _countdown;
    private float _currentTime;
    private float _timeElapsed => Round(_countdown - _currentTime);
    private Coroutine _currentRoutine;

    void Start()
    {
        StartTimer(1);    
    }

    private void StartTimer(float amount)
    {
        if(_currentRoutine != null)
        {
            Debug.Log($"[{name}] Timer already running! Time left: {_currentTime}");
            return;
        }

        ResetValues();
        _countdown = amount;
        _currentRoutine = StartCoroutine(nameof(TimerRoutine));
    }

    private void ResetValues()
    {
        _countdown = 0;
        _currentTime = 0;
    }

    private IEnumerator TimerRoutine()
    {
        _currentTime = _countdown;
        while(_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            Debug.Log($"[{name}] Time left: {Round(_currentTime)}, Time elapsed: {_timeElapsed}");
            yield return null;
        }
        _currentRoutine = null;
    }

    private float Round(float number)
    {
        return (float)Math.Round(number, 1);
    }
}
