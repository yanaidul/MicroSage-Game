using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PalingAwal : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuUI;

    [SerializeField]
    private GameObject _LoadingUI;

    private void Start()
    {
        OnOpenMainMenu();
    }

    public void OnOpenMainMenu()
    {
        _mainMenuUI.SetActive(true);
        _LoadingUI.SetActive(false);
    }

    public void OnOpenLoading()
    {
        _LoadingUI.SetActive(true);
        _mainMenuUI.SetActive(false);
    }
}
