using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour
{
    private Stopwatch _timer;
    private TextMeshProUGUI _timerText;
    private bool _isDescending;

    [SerializeField]
    int IntervalSeconds = 10;

    [SerializeField]
    GameObject TimerImage;

    [SerializeField]
    GameObject TimerText;

    public event Action TimerTick;

    // Start is called before the first frame update
    void Start()
    {
        _timerText = TimerText.GetComponent<TextMeshProUGUI>();

        TimerImage.SetActive(false);
        TimerText.SetActive(false);

        _timer = new Stopwatch();
        _isDescending = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void StartTimer()
    {
        TimerImage.SetActive(true);
        TimerText.SetActive(true);

        _timerText.text = IntervalSeconds.ToString();

        _timer.Start();
    }

    public void EndTimer()
    {
        TimerImage.SetActive(false);
        TimerText.SetActive(false);

        _timer.Stop();
        _timer.Reset();

        TimerTick?.Invoke();
    }

    public void ChangeTimerDirection(bool isDescending)
    {
        _isDescending = isDescending;
    }

    private void UpdateTimer()
    {
        if (!_timer.IsRunning)
        {
            return;
        }

        var timerTextValue = _isDescending
            ? IntervalSeconds - _timer.Elapsed.Seconds
            : IntervalSeconds + _timer.Elapsed.Seconds;

        _timerText.text = timerTextValue.ToString();

        if (timerTextValue <= 0)
        {
            EndTimer();
        }
    }
}
