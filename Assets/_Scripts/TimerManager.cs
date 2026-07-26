using System;
using System.Collections;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    public static event Action TimerFinished;
    public static event Action<int> SecondPassed;
    public float CurrentTime => _currentTime;
    private float _countdown;
    private float _currentTime;
    private float _timeElapsed => Round(_countdown - _currentTime);
    private Coroutine _currentRoutine;

    private int _nextSecond;

    void Start()
    {
        StartTimer(60);
    }

    private void StartTimer(float amount)
    {
        if (_currentRoutine != null)
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

    public void AddTime(float amount)
    {
        if (_currentRoutine == null)
            return;

        _currentTime += amount;
        _countdown += amount;

        // Update the next second threshold.
        _nextSecond = Mathf.FloorToInt(_currentTime) - 1;

        Debug.Log($"Added {amount}s. New time: {Round(_currentTime)}");
    }

    private IEnumerator TimerRoutine()
    {
        _currentTime = _countdown;
        _nextSecond = Mathf.FloorToInt(_countdown) - 1;

        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= _nextSecond)
            {
                SecondPassed?.Invoke(_nextSecond);
                _nextSecond--;
            }
            yield return null;
        }

        _currentRoutine = null;
        TimerFinished?.Invoke();
    }

    private float Round(float number)
    {
        return (float)Math.Round(number, 1);
    }
}
