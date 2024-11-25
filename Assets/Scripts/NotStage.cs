using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotStage : MonoBehaviour
{
    [SerializeField] private List<Button> _notStage;
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _notStage.Count; i++)
        {
            if (i != 0) _notStage[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("NotStage"))
        {
            _unlockedLevel = PlayerPrefs.GetInt("NotStage", 1);
        }
        else
            PlayerPrefs.SetInt("NotStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _notStage[i].interactable = true;
        }
    }
}
