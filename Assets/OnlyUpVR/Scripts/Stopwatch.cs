using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float _time = 0;
    private bool _timeIsRunning = false;

    public string FormattedTime
    {
        get
        {
            return FormatTime(_time);
        }
    }

    private void Update()
    {
        if (_timeIsRunning)
        {
            _time += Time.deltaTime;
        }
    }

    public void StartCounting()
    {
        _timeIsRunning = true;
    }

    public void StopCounting()
    {
        _timeIsRunning = false;
        _time = 0;
    }

    private string FormatTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
