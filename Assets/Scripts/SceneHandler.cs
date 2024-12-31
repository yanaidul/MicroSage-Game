using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //[SerializeField] GameEventNoParam _onNextLevelEvent;
    //[SerializeField] GameEventNoParam _onOnReturnToLevel1Event;

    public void OnSelectMenu(int indexMenu)
    {
        PlayerPrefs.SetInt("MenuIndex", indexMenu);
    }

    public void OnSelectQuizMenu(string nameQuiz)
    {
        PlayerPrefs.SetInt("QuizMenu", 1);
        PlayerPrefs.SetString("SelectedQuiz", nameQuiz);
    }

    public void OnNextLevel(int sceneIndex)
    {
        //_onNextLevelEvent.Raise();
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void OnMainMenuScene()
    {
        Time.timeScale = 1;
        //if(BGM.instance != null) BGM.instance.DestroyBGMGameObject();
        //_playerData.ResetData();
        SceneManager.LoadScene(2);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void ResetScoreQuiz()
    {
        string nameQuiz = PlayerPrefs.GetString("SelectedQuiz");
        string[] categories = { "AND", "OR", "NAND", "NOR", "NOT", "XOR", "XNOR" };
        if (System.Array.Exists(categories, category => category == nameQuiz))
        {
            Debug.Log("Reset score for " + nameQuiz); // Reset skor untuk quiz yang dipilih
            PlayerPrefs.SetInt("Score_Quiz_" + nameQuiz, 0);
            PlayerPrefs.SetInt("LastQuestion_Index_" + nameQuiz, 0);
            PlayerPrefs.SetInt("CorrectReplies_" + nameQuiz, 0);
            PlayerPrefs.SetInt("WrongReplies_" + nameQuiz, 0);
        }
    }
}
