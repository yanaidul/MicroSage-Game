using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject _loadingUI;

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

    [SerializeField]
    private TutorialManager _tutorialManager;

    [Header("Animasi Manager")]
    [SerializeField]
    private AnimasiManager _animasiManager;

    void Start()
    {
        //OnLoading();
        OnGameplay();
    }

    public void OnLoading()
    {
        _loadingUI.SetActive(true);
        _onResume.Raise();

        _gameUI.SetActive(false);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    // public async void OnMasukGame()
    // {
    //     _onResume.Raise();
    //     _gameUI.SetActive(true);
    //     _loadingUI.SetActive(false);
    //     _winPopUp.SetActive(false);
    //     _settingPopUp.SetActive(false);
    //     _losePopUp.SetActive(false);
    // }

    public async void OnGameplay()
    {
        // _tutorialManager.OnEnable();
        await _animasiManager.PausePanelOutro();

        _onResume.Raise();
        _gameUI.SetActive(true);
        _loadingUI.SetActive(false);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnWin()
    {
        _animasiManager.WinPanelIntro();
        if (PlayerPrefs.GetInt(_stagePlayerPrefs) < _stageID)
            PlayerPrefs.SetInt(_stagePlayerPrefs, _stageID);

        _winPopUp.SetActive(true);
        _settingPopUp.SetActive(false);
        _losePopUp.SetActive(false);
    }

    public void OnLose()
    {
        _animasiManager.LosePanelIntro();
        _losePopUp.SetActive(true);
        _winPopUp.SetActive(false);
        _settingPopUp.SetActive(false);
    }

    public void OnSetting()
    {
        _animasiManager.PausePanelIntro();
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
