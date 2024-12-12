using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum StageType
{
    AND,
    OR,
    NOT,
    NAND,
    NOR,
    XOR,
    XNOR
}


public class ResultManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private List<GameObject> _stars = new();
    [SerializeField] private TextMeshProUGUI _timerWinUIText;
    [SerializeField] private TextMeshProUGUI _cableLeftText;

    [SerializeField] private int _switchPatternToWin = 4;
    [SerializeField] private GameEventNoParam _onTurnOnLED;
    [SerializeField] private GameEventNoParam _onTurnOffLED;
    [SerializeField] private GameEventNoParam _onWin;
    [SerializeField] private StageType _currentStageType;

    private bool _isHighxHighClear = false;
    private bool _isHighxLowClear = false;
    private bool _isLowxHighClear = false;
    private bool _isLowxLowClear = false;

    public StageType CurrentStageType => _currentStageType;

    public void OnCheckCable(bool isSwitch1On, bool isSwitch2On)
    {
        switch (_currentStageType)
        {
            case StageType.AND:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;

            case StageType.OR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;

            case StageType.NOT:
                Debug.Log("Check not");
                if (isSwitch1On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();

                }
                break;
            case StageType.NAND:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
            case StageType.NOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
            case StageType.XOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;
            case StageType.XNOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();

                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Cable Left: " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
        }
        if (_switchPatternToWin == 0) _onWin.Raise();
    }

    public void OnWin()
    {
        _timerWinUIText.SetText(_timer.OnReturnTimeLeftValueAfterWin().ToString() + " Detik");
        foreach (var item in _stars)
        {
            item.gameObject.SetActive(true);
        }
        if(_timer.OnReturnTimeLeftValueAfterWin() <= 150)
        {
            _stars[2].gameObject.SetActive(false);
        }
        if (_timer.OnReturnTimeLeftValueAfterWin() <= 100)
        {
            _stars[1].gameObject.SetActive(false);
        }
    }
}
