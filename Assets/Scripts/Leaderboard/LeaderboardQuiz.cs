using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardQuiz : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _and;

    [SerializeField]
    private TextMeshProUGUI _or;

    [SerializeField]
    private TextMeshProUGUI _nand;

    [SerializeField]
    private TextMeshProUGUI _nor;

    [SerializeField]
    private TextMeshProUGUI _not;

    [SerializeField]
    private TextMeshProUGUI _xor;

    [SerializeField]
    private TextMeshProUGUI _xnor;

    // Start is called before the first frame update
    void Start()
    {
        SetLeaderboardStatus();
    }

    public void SetLeaderboardStatus()
    {
        SetLeaderboardText(_and, "Score_Quiz_AND");
        SetLeaderboardText(_or, "Score_Quiz_OR");
        SetLeaderboardText(_nand, "Score_Quiz_NAND");
        SetLeaderboardText(_nor, "Score_Quiz_NOR");
        SetLeaderboardText(_not, "Score_Quiz_NOT");
        SetLeaderboardText(_xor, "Score_Quiz_XOR");
        SetLeaderboardText(_xnor, "Score_Quiz_XNOR");
    }

    private void SetLeaderboardText(TextMeshProUGUI text, string key)
    {
        int score = PlayerPrefs.GetInt(key, 0);

        text.text = score > 0 ? score.ToString() : "0";
    }
}
