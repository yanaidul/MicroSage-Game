using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private GameEventNoParam _onPlaySwitchSFX;

    [SerializeField]
    private Tile _referencedTile1;

    [SerializeField]
    private Tile _referencedTile2;

    [SerializeField]
    private Led _led;

    [SerializeField]
    private Field _field;

    [SerializeField]
    private GameObject _onSwitch;

    [SerializeField]
    private GameObject _offSwitch;

    private bool _isClicked = false;
    public bool IsClicked => _isClicked;
    public Tile RefencedTile1 => _referencedTile1;
    public Tile RefencedTile2 => _referencedTile2;
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
            _isClicked = false;
            _led.CheckSwitchOnValue();
        }
        else
        {
            _onSwitch.SetActive(true);
            _offSwitch.SetActive(false);
            _isClicked = true;
            _led.CheckSwitchOnValue();
        }
    }
}
