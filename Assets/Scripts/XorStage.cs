using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XorStage : MonoBehaviour
{
    [SerializeField] private List<Button> _xorStages = new List<Button>();
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _xorStages.Count; i++)
        {
            if (i != 0) _xorStages[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("XorStage")
        {
            _unlockedLevel = PlayerPrefs.GetInt("XorStage", 1);
        }
        else
            PlayerPrefs.SetInt("XorStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _xorStages[i].interactable = true;
        }
    }
}
