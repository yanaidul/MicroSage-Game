using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TextMeshProUGUI _timerWinUIText;

    public void OnWin()
    {
        _timerWinUIText.SetText(_timer.OnReturnTimeLeftValueAfterWin().ToString() + " Detik");
    }
}
