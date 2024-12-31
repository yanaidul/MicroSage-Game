using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
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
        SetLeaderboardText(_and, "Score_Category_AND");
        SetLeaderboardText(_or, "Score_Category_OR");
        SetLeaderboardText(_nand, "Score_Category_NAND");
        SetLeaderboardText(_nor, "Score_Category_NOR");
        SetLeaderboardText(_not, "Score_Category_NOT");
        SetLeaderboardText(_xor, "Score_Category_XOR");
        SetLeaderboardText(_xnor, "Score_Category_XNOR");
    }

    private void SetLeaderboardText(TextMeshProUGUI text, string key)
    {
        int score = PlayerPrefs.GetInt(key, 0);
        text.text = score > 0 ? score.ToString() : "0";
    }
}
