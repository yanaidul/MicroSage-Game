using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _winPopUp;
    [SerializeField] private GameObject _losePopUp;
    [SerializeField] private GameObject _settingPopUp;
    [SerializeField] private GameEventNoParam _onPause;
    [SerializeField] private GameEventNoParam _onResume;
    void Start()
    {
        OnGameplay();

    }

    public void OnGameplay()
    {
        _onResume.Raise();
        _gameUI.SetActive(true);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnWin()
    {
        _winPopUp.SetActive(true);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnLose()
    {
        _losePopUp.SetActive(true);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
    }

    public void OnSetting()
    {
        _onPause.Raise();
        _settingPopUp.SetActive(true);
        _losePopUp.SetActive(false);
        _winPopUp.SetActive(false);
    }

}