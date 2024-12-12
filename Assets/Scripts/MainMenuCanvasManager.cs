using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    [Header("Main Menu Screen")]
    [SerializeField]
    private GameObject _mainMenuUI;

    [SerializeField]
    private GameObject _stageSelectionUI;

    [SerializeField]
    private GameObject _LoadingUI;

    [SerializeField]
    private GameObject _ResetButton;

    [Header("Leaderboard Screen")]
    public LeaderboardManager leaderboardManager;

    [SerializeField]
    private GameObject _Leaderboard;

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

    public void OnOpenLeaderBoard()
    {
        _Leaderboard.SetActive(true);
        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(true);
        _LoadingUI.SetActive(false);
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
        _Leaderboard.SetActive(false);
        _mainMenuUI.SetActive(true);
        _LoadingUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
    }

    public void OnResetScore()
    {
        string[] categories = { "AND", "OR", "NAND", "NOR", "NOT", "XOR", "XNOR" };

        foreach (string category in categories)
        {
            PlayerPrefs.DeleteKey($"LastQuestion_Index_{category}");
            PlayerPrefs.DeleteKey($"Score_Category_{category}");
            PlayerPrefs.DeleteKey($"CorrectReplies_{category}");
            PlayerPrefs.DeleteKey($"WrongReplies_{category}");
        }

        ResetToDefaultValues();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        leaderboardManager.SetLeaderboardStatus();
    }

    private void ResetToDefaultValues()
    {
        foreach (string category in new[] { "AND", "OR", "NAND", "NOR", "NOT", "XOR", "XNOR" })
        {
            PlayerPrefs.SetInt($"Score_Category_{category}", 0);
            PlayerPrefs.SetInt($"CorrectReplies_{category}", 0);
            PlayerPrefs.SetInt($"WrongReplies_{category}", 0);
        }
    }

    public void closeButton()
    {
        gameObject.SetActive(false);
    }

    public void OnOpenStageSelection()
    {
        _LoadingUI.SetActive(false);
        _Leaderboard.SetActive(false);
        _stageSelectionUI.SetActive(true);
        _mainMenuUI.SetActive(false);
        // _icAND.SetActive(false);
        // _icOR.SetActive(false);
        // _icNAND.SetActive(false);
        // _icNOR.SetActive(false);
        // _icNOT.SetActive(false);
        // _icXOR.SetActive(false);
        // _icXNOR.SetActive(false);
    }

    public void OnOpenLoading()
    {
        _LoadingUI.SetActive(true);
        _mainMenuUI.SetActive(false);
        _stageSelectionUI.SetActive(false);
        _Leaderboard.SetActive(false);
    }

    // public void OnOpenICAnd()
    // {
    //     _icAND.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXOR.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICOr()
    // {
    //     _icOR.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXOR.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICNand()
    // {
    //     _icNAND.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXOR.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICNor()
    // {
    //     _icNOR.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXOR.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICNot()
    // {
    //     _icNOT.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icXOR.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICXor()
    // {
    //     _icXOR.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXNOR.SetActive(false);
    // }

    // public void OnOpenICXnor()
    // {
    //     _icXNOR.SetActive(true);

    //     _mainMenuUI.SetActive(false);
    //     _stageSelectionUI.SetActive(false);
    //     _icNAND.SetActive(false);
    //     _icOR.SetActive(false);
    //     _icAND.SetActive(false);
    //     _icNOR.SetActive(false);
    //     _icNOT.SetActive(false);
    //     _icXOR.SetActive(false);
    // }

    public void OnCategorySelected(int categoryIndex)
    {
        PlayerPrefs.SetInt("SelectedCategory", categoryIndex);
    }

    public void OnStageCompleted(int stageID)
    {
        PlayerPrefs.SetInt("StageCompleted", stageID);
    }
}
