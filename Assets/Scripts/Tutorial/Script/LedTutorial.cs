using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedTutorial : MonoBehaviour
{
    [SerializeField]
    private int _amountOfSwitchToOn = 2;

    [SerializeField]
    private GameObject _onLed;

    [SerializeField]
    private GameObject _offLed;

    [SerializeField]
    private GameEventNoParam _onWin;
    private int _currentOnSwitch = 0;

    private void Start()
    {
        _onLed.SetActive(false);
        _offLed.SetActive(true);
        _currentOnSwitch = 0;
    }

    public void AddCurrentOnSwitch(int value)
    {
        _currentOnSwitch += value;
        CheckSwitchOnValue();
    }

    public void CheckSwitchOnValue()
    {
        if (_currentOnSwitch == _amountOfSwitchToOn)
        {
            _onLed.SetActive(true);
            _offLed.SetActive(false);
            _onWin.Raise();
        }
    }
}
