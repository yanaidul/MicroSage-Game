using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _stageSelectionUI;
    [SerializeField] private GameObject _icAND;
    [SerializeField] private GameObject _icOR;

    private void Start()
    {
        OnOpenMainMenu();
    }

    public void OnOpenMainMenu()
    {
        _mainMenuUI.SetActive(true);

        _stageSelectionUI.SetActive(false);
        _icAND.SetActive(false);
        _icOR.SetActive(false);
    }    

    public void OnOpenStageSelection()
    {
        _stageSelectionUI.SetActive(true);

        _mainMenuUI.SetActive(false);
        _icAND.SetActive(false);
        _icOR.SetActive(false);
    }

    public void OnOpenICAnd()
    {
        _icAND.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icOR.SetActive(false);
    }
}
