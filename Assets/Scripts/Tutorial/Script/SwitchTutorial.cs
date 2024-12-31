using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTutorial : MonoBehaviour
{
    [SerializeField]
    private GameEventNoParam _onPlaySwitchSFX;

    [SerializeField]
    private Tile _referencedTile;

    [SerializeField]
    private LedTutorial _led;

    [SerializeField]
    private Field _field;

    [SerializeField]
    private GameObject _onSwitch;

    [SerializeField]
    private GameObject _offSwitch;

    private bool _isClicked = false;

    private void Start()
    {
        _onSwitch.SetActive(false);
        _offSwitch.SetActive(true);
        _isClicked = false;
    }

    private void OnMouseDown()
    {
        if (!_referencedTile.isSolved || !_field.CheckCurrentSolvedStatus())
            return;
        _onPlaySwitchSFX.Raise();
        if (_isClicked)
        {
            _onSwitch.SetActive(false);
            _offSwitch.SetActive(true);
            _led.AddCurrentOnSwitch(-1);
            _isClicked = false;
        }
        else
        {
            _onSwitch.SetActive(true);
            _offSwitch.SetActive(false);
            _led.AddCurrentOnSwitch(1);
            _isClicked = true;
        }
    }
}
