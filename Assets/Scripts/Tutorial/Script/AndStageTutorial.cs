using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndStageTutorial : MonoBehaviour
{
    [SerializeField]
    private List<Button> _andStages;
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _andStages.Count; i++)
        {
            if (i != 0)
                _andStages[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("AndStage"))
        {
            _unlockedLevel = PlayerPrefs.GetInt("AndStage", 1);
        }
        else
            PlayerPrefs.SetInt("AndStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _andStages[i].interactable = true;
        }
    }
}
