using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManagerTutorial : MonoBehaviour
{
    [SerializeField]
    private TimerTutorial _timer;

    [SerializeField]
    private List<GameObject> _stars = new();

    [SerializeField]
    private TextMeshProUGUI _timerWinUIText;

    public void OnWin()
    {
        _timerWinUIText.SetText(_timer.OnReturnTimeLeftValueAfterWin().ToString() + " Detik");
        foreach (var item in _stars)
        {
            item.gameObject.SetActive(true);
        }
        if (_timer.OnReturnTimeLeftValueAfterWin() <= 150)
        {
            _stars[2].gameObject.SetActive(false);
        }
        if (_timer.OnReturnTimeLeftValueAfterWin() <= 100)
        {
            _stars[1].gameObject.SetActive(false);
        }
    }
}
