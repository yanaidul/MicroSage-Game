using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingUI;

    [SerializeField]
    private GameObject _gameUI;

    [SerializeField]
    private GameObject _winPopUp;

    [SerializeField]
    private GameObject _losePopUp;

    [SerializeField]
    private GameObject _settingPopUp;

    [SerializeField]
    private GameEventNoParam _onPause;

    [SerializeField]
    private GameEventNoParam _onResume;

    [SerializeField]
    private string _stagePlayerPrefs;

    [SerializeField]
    private int _stageID;

    void Start()
    {
        //OnLoading();
        OnGameplay();
    }

    public void OnLoading()
    {
        _onResume.Raise();
        _loadingUI.SetActive(true);
        _gameUI.SetActive(false);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnGameplay()
    {
        _onResume.Raise();
        _gameUI.SetActive(true);
        _loadingUI.SetActive(false);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnWin()
    {
        if (PlayerPrefs.GetInt(_stagePlayerPrefs) < _stageID)
            PlayerPrefs.SetInt(_stagePlayerPrefs, _stageID);

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

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
