using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public QuestionData selectedCategoryData;

    public const string ScoreKey = "Score_Quiz_";

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

    public int GetScore(string nameQuiz)
    {
        return selectedCategoryData.score;
    }

    void UpdateScoreText()
    {
        scoreText.text = "" + score.ToString();
    }

    public void SaveScore(string nameQuiz)
    {
        PlayerPrefs.SetInt(ScoreKey + nameQuiz, score);
        selectedCategoryData.score = score;
        PlayerPrefs.Save();
        Debug.Log("Score for " + nameQuiz + " has been saved.");
    }

    public void LoadScore(string nameQuiz)
    {
        if (PlayerPrefs.HasKey(ScoreKey + nameQuiz))
        {
            score = PlayerPrefs.GetInt(ScoreKey + nameQuiz);
            UpdateScoreText();
            Debug.Log("Score for " + nameQuiz + " has been loaded.");
        }
        else
        {
            Debug.LogWarning("Score for " + nameQuiz + " not found. Setting score to 0.");
            score = 0;
            UpdateScoreText();
        }
    }

    public void ResetScore()
    {
        Debug.Log("Resetting score for " + selectedCategoryData.category);
        string fullKey = ScoreKey + selectedCategoryData.category;

        if (PlayerPrefs.HasKey(fullKey))
        {
            PlayerPrefs.DeleteKey(fullKey);
        }

        score = 0;
        selectedCategoryData.score = 0;
        PlayerPrefs.Save();
        UpdateScoreText();

        Debug.Log("Score reset successfully for " + selectedCategoryData.category);
    }
}
