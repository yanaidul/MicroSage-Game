using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NandStage : MonoBehaviour
{
    [SerializeField] private List<Button> _nandStages;
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _nandStages.Count; i++)
        {
            if (i != 0) _nandStages[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("NandStage"))
        {
            _unlockedLevel = PlayerPrefs.GetInt("NandStage", 1);
        }
        else
            PlayerPrefs.SetInt("NandStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _nandStages[i].interactable = true;
        }
    }
}
