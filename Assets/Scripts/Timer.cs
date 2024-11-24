using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    //[SerializeField] private GameEventNoParam _onGameOver;

    public bool stopTimer = false;
    [SerializeField] private float _countdownTime = 60;

    private void Start()
    {
        //stopTimer = true;
    }

    public void OnResetTimer()
    {
        _countdownTime = 60;
        stopTimer = false;
        UpdateTimerText();
    }

    public void OnPause()
    {
        stopTimer = true;
    }

    public void OnResume()
    {
        stopTimer = false;
    }

    public int OnReturnTimeLeftValueAfterWin()
    {
        stopTimer = true;
        return Mathf.FloorToInt(_countdownTime);
    }

    void Update()
    {
        if (stopTimer) return;

        if (_countdownTime > 0)
        {
            _countdownTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            Debug.Log("Timer expired!");
            //_onGameOver.Raise();
            _countdownTime = 0;
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_countdownTime / 60);
        int seconds = Mathf.FloorToInt(_countdownTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        _timerText.SetText(timerString);
    }
}
