using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{
    [SerializeField] private int _amountOfSwitchToOn = 2;
    [SerializeField] private GameObject _onLed;
    [SerializeField] private GameObject _offLed;
    [SerializeField] private GameEventNoParam _onWin;
    private int _currentOnSwitch = 0;

    [SerializeField] private Switch _switch1;
    [SerializeField] private Switch _switch2;

    private void Start()
    {
        _onLed.SetActive(false);
        _offLed.SetActive(true);
        _currentOnSwitch = 0;
    }

    private void Update()
    {
        if(_switch1.IsClicked && _switch2.IsClicked) 
        {
            CheckSwitchOnValue();
        }
    }

    public void AddCurrentOnSwitch(int value)
    {
        _currentOnSwitch += value;
        CheckSwitchOnValue();
        
    }
    public void CheckSwitchOnValue()
    {
        if (!_switch1.RefencedTile.isSolved || !_switch1.Field.CheckCurrentSolvedStatus()) return;
        if (!_switch2.RefencedTile.isSolved || !_switch2.Field.CheckCurrentSolvedStatus()) return;
        if (_currentOnSwitch == _amountOfSwitchToOn)
        {
            _onLed.SetActive(true);
            _offLed.SetActive(false);
            _onWin.Raise();
        }
    }
}
