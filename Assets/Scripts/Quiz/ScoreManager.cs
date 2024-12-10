using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public QuestionData selectedCategoryData;

    public const string ScoreKey = "Score_Category_";

    void Start()
    {
        UpdateScoreText();
        Debug.Log("Start: Score is " + score);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
        if (selectedCategoryData != null)
        {
            SaveScore(selectedCategoryData.category);
        }
    }

    public void SubtractScore(int points)
    {
        score -= points;
        UpdateScoreText();
        if (selectedCategoryData != null)
        {
            SaveScore(selectedCategoryData.category);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "" + score.ToString();
    }

    public void SaveScore(string nameCategory)
    {
        PlayerPrefs.SetInt(ScoreKey + nameCategory, score);

        PlayerPrefs.Save();
        Debug.Log("Score for " + nameCategory + " has been saved.");
    }

    public void LoadScore(string nameCategory)
    {
        if (PlayerPrefs.HasKey(ScoreKey + nameCategory))
        {
            score = PlayerPrefs.GetInt(ScoreKey + nameCategory);
            UpdateScoreText();
            Debug.Log("Score for " + nameCategory + " has been loaded.");
        }
        else
        {
            Debug.LogWarning("Score for " + nameCategory + " not found. Setting score to 0.");
            score = 0;
            UpdateScoreText();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(ScoreKey);
        score = 0;
        UpdateScoreText();
    }
}
