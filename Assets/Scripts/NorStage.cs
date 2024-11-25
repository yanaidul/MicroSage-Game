using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NorStage : MonoBehaviour
{
    [SerializeField] private List<Button> _norStage;
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _norStage.Count; i++)
        {
            if (i != 0) _norStage[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("NorStage"))
        {
            _unlockedLevel = PlayerPrefs.GetInt("NorStage", 1);
        }
        else
            PlayerPrefs.SetInt("NorStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _norStage[i].interactable = true;
        }
    }
}
