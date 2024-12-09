using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameEventNoParam _onPlaySwitchSFX;

    [SerializeField] private Tile _referencedTile;
    [SerializeField] private Led _led;
    [SerializeField] private Field _field;
    [SerializeField] private GameObject _onSwitch;
    [SerializeField] private GameObject _offSwitch;

    private bool _isClicked = false;
    public bool IsClicked => _isClicked;
    public Tile RefencedTile => _referencedTile;
    public Field Field => _field;

    private void Start()
    {
        _onSwitch.SetActive(false);
        _offSwitch.SetActive(true);
        _isClicked = false;
    }

    private void OnMouseDown()
    {
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
