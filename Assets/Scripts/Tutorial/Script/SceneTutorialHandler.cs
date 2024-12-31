using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTutorialHandler : MonoBehaviour
{
    //[SerializeField] GameEventNoParam _onNextLevelEvent;
    //[SerializeField] GameEventNoParam _onOnReturnToLevel1Event;
    public void OnNextLevel()
    {
        //_onNextLevelEvent.Raise();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        SceneManager.LoadScene(0);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
