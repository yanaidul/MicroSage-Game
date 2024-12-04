using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuUI;

    [SerializeField]
    private GameObject _stageSelectionUI;

    [SerializeField]
    private GameObject _icAND;

    [SerializeField]
    private GameObject _icOR;

    [SerializeField]
    private GameObject _icNAND;

    [SerializeField]
    private GameObject _icNOR;

    [SerializeField]
    private GameObject _icNOT;

    [SerializeField]
    private GameObject _icXOR;

    [SerializeField]
    private GameObject _icXNOR;

    [SerializeField]
    private GameObject _ResetButton;

    private void Start()
    {
        OnOpenMainMenu();
        CheckResetButtonVisibility();
    }

    public void CheckResetButtonVisibility()
    {
        if (
            PlayerPrefs.HasKey("NandStage")
            || PlayerPrefs.HasKey("AndStage")
            || PlayerPrefs.HasKey("OrStage")
            || PlayerPrefs.HasKey("NorStage")
            || PlayerPrefs.HasKey("NotStage")
            || PlayerPrefs.HasKey("XorStage")
            || PlayerPrefs.HasKey("XnorStage")
        )
        {
            _ResetButton.SetActive(true);
        }
        else
        {
            _ResetButton.SetActive(false);
        }
    }

    public void OnResetButton()
    {
        PlayerPrefs.DeleteKey("NandStage");
        PlayerPrefs.DeleteKey("NorStage");
        PlayerPrefs.DeleteKey("NotStage");
        PlayerPrefs.DeleteKey("OrStage");
        PlayerPrefs.DeleteKey("AndStage");
        PlayerPrefs.DeleteKey("XorStage");
        PlayerPrefs.DeleteKey("XnorStage");
        _ResetButton.SetActive(false);
        Debug.Log("All stage data has been reset.");
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
        _icAND.SetActive(false);
        _icOR.SetActive(false);
        _icNAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
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
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
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
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
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
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
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
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
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
        _icXOR.SetActive(false);
        _icXNOR.SetActive(false);
    }

    public void OnOpenICXor()
    {
        _icXOR.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icNAND.SetActive(false);
        _icOR.SetActive(false);
        _icAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
        _icXNOR.SetActive(false);
    }

    public void OnOpenICXnor()
    {
        _icXNOR.SetActive(true);

        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _icNAND.SetActive(false);
        _icOR.SetActive(false);
        _icAND.SetActive(false);
        _icNOR.SetActive(false);
        _icNOT.SetActive(false);
        _icXOR.SetActive(false);
    }

    public void OnCategorySelected(int categoryIndex)
    {
        PlayerPrefs.SetInt("SelectedCategory", categoryIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
