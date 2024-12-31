using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{
    [SerializeField]
    private int _amountOfSwitchToOn = 2;

    [SerializeField]
    private ResultManager _resultManager;

    [SerializeField]
    private GameObject _onLed;

    [SerializeField]
    private GameObject _offLed;

    [SerializeField]
    private GameEventNoParam _onWin;

    [SerializeField]
    private Switch _switch1;

    [SerializeField]
    private Switch _switch2;

    private void Start()
    {
        _onLed.SetActive(false);
        _offLed.SetActive(true);
    }

    public void CheckSwitchOnValue()
    {
        if (!_switch1.RefencedTile1.isSolved || !_switch1.Field.CheckCurrentSolvedStatus())
            return;
        if (!_switch1.RefencedTile2.isSolved || !_switch1.Field.CheckCurrentSolvedStatus())
            return;
        if (_resultManager.CurrentStageType != StageType.NOT)
        {
            if (!_switch2.RefencedTile1.isSolved || !_switch2.Field.CheckCurrentSolvedStatus())
                return;
            if (!_switch2.RefencedTile2.isSolved || !_switch2.Field.CheckCurrentSolvedStatus())
                return;
        }
        _resultManager.OnCheckCable(_switch1.IsClicked, _switch2.IsClicked);
    }

    public void OnTurnOnLED()
    {
        _onLed.SetActive(true);
        _offLed.SetActive(false);
    }

    public void OnTurnOffLED()
    {
        _onLed.SetActive(false);
        _offLed.SetActive(true);
    }
}
