using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _stageSelectionUI;
    [SerializeField] private GameObject _icAND;
    [SerializeField] private GameObject _icOR;
    [SerializeField] private GameObject _icNAND;
    [SerializeField] private GameObject _icNOR;
    [SerializeField] private GameObject _icNOT;

    private void Start()
    {
        OnOpenMainMenu();
    }

    public void OnOpenMainMenu()
    {
        _mainMenuUI.SetActive(true);

        _stageSelectionUI.SetActive(false);
    }    

    public void OnOpenStageSelection()
    {
        _stageSelectionUI.SetActive(true);

        _mainMenuUI.SetActive(false);
    }

    public void OnOpenICAnd()
    {
        _icAND.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icOR.SetActive(false);
        _icNAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
    }

    public void OnOpenICOr()
    {
        _icOR.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icAND.SetActive(false);
        _icNAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
    }

    public void OnOpenICNand()
    {
        _icNAND.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icOR.SetActive(false);
        _icAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
    }
    public void OnOpenICNor()
    {
        _icNOR.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icNAND.SetActive(false);
        _icOR.SetActive(false);
        _icAND.SetActive(false);
        _icNOT.SetActive(false);
    }

    public void OnOpenICNot()
    {
        _icNOT.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icNAND.SetActive(false);
        _icOR.SetActive(false);
        _icAND.SetActive(false);
        _icNOR.SetActive(false);
    }
}
