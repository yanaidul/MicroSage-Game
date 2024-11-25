using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrStage : MonoBehaviour
{
    [SerializeField] private List<Button> _orStages;
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _orStages.Count; i++)
        {
            if (i != 0) _orStages[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("OrStage"))
        {
            _unlockedLevel = PlayerPrefs.GetInt("OrStage", 1);
        }
        else
            PlayerPrefs.SetInt("OrStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _orStages[i].interactable = true;
        }
    }
}
