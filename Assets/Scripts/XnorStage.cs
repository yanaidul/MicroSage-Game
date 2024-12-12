using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XnorStage : MonoBehaviour
{
    [SerializeField] private List<Button> _xnorStages = new List<Button>();
    private int _unlockedLevel = 1;

    private void Start()
    {
        for (int i = 0; i < _xnorStages.Count; i++)
        {
            if (i != 0) _xnorStages[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("XnorStage")
        {
            _unlockedLevel = PlayerPrefs.GetInt("XnorStage", 1);
        }
        else
            PlayerPrefs.SetInt("XnorStage", 1);

        for (int i = 0; i < _unlockedLevel; i++)
        {
            _xnorStages[i].interactable = true;
        }
    }
}
