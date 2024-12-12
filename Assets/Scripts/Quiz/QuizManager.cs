using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestionData[] categories;
    private QuestionData selectedCategory;

    [Header("Question Display")]
    [SerializeField]
    private int currentQuestionIndex = 0;

    public TextMeshProUGUI questionText;
    public Image questionImage;
    public Button[] replyButtons;

    [Header("Score Manager")]
    public ScoreManager scoreManager;

    [SerializeField]
    public int correctReplyScore = 10;

    [SerializeField]
    public int wrongReplyScore = 5;
    public TextMeshProUGUI scoreText;

    [Header("correctReplyIndex")]
    public int correctReplyIndex;
    int correctReplies;
    int wrongReplies;

    [Header("Index Benar / salah")]
    public TextMeshProUGUI correctRepliesText;
    public TextMeshProUGUI wrongRepliesText;

    [Header("Game Finished Panel")]
    public GameObject gameFinishedPanel;

    [Header("Time Countdown")]
    public TextMeshProUGUI countdownText;

    [SerializeField]
    public float countdownDuration = 3f;

    public void Start()
    {
        int selectedCategoryIndex = PlayerPrefs.GetInt("SelectedCategory", 0);
        gameFinishedPanel.SetActive(false);
        SelectCategory(selectedCategoryIndex);
        LoadProgress(selectedCategory.category);
    }

    public void SelectCategory(int categoryIndex)
    {
        selectedCategory = categories[categoryIndex];
        currentQuestionIndex = 0;
        scoreManager.selectedCategoryData = selectedCategory;
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        if (selectedCategory == null)
        {
            Debug.LogError("No category selected");
            return;
        }
        ResetButtonColors();
        var question = selectedCategory.questions[currentQuestionIndex];
        questionText.text = question.questionText;
        questionImage.sprite = question.QuestionImage;

        // Validasi jumlah tombol
        if (replyButtons.Length < question.replies.Length)
        {
            Debug.LogError("Not enough buttons for replies");
            return;
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i < question.replies.Length)
            {
                replyButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI buttonText = replyButtons[i]
                    .GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = question.replies[i];

                int replyIndex = i;
                replyButtons[i].onClick.RemoveAllListeners();
                replyButtons[i].onClick.AddListener(() => OnReplySelected(replyIndex));
            }
            else
            {
                // Sembunyikan tombol jika tidak diperlukan
                replyButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnReplySelected(int replyIndex)
    {
        if (
            selectedCategory == null
            || selectedCategory.questions == null
            || selectedCategory.questions.Length == 0
        )
        {
            Debug.LogError("Kategori atau pertanyaan belum diatur.");
            return;
        }
        // Periksa apakah jawaban benar
        if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
        {
            scoreManager.AddScore(correctReplyScore);
            correctReplies++;
            SaveCorrectReplies();
            correctRepliesText.text = correctReplies.ToString();
            Debug.Log("Correct!");
        }
        else
        {
            wrongReplies++;
            scoreManager.SubtractScore(wrongReplyScore);
            SaveWrongReplies();
            wrongRepliesText.text = wrongReplies.ToString();
            Debug.Log("Incorrect!");
        }

        // Perbarui indeks pertanyaan
        currentQuestionIndex++;
        SaveProgress();
        // Cek apakah sudah selesai atau tampilkan pertanyaan berikutnya
        if (currentQuestionIndex < selectedCategory.questions.Length)
        {
            int remainingQuestions = selectedCategory.questions.Length - currentQuestionIndex;
            Debug.Log("Sisa pertanyaan: " + remainingQuestions);
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            ShowGameFinishedPanel();

            if (!string.IsNullOrEmpty(selectedCategory.targetScene))
            {
                // Panggil coroutine untuk memberi jeda sebelum pindah scene
                StartCoroutine(LoadSceneWithDelay(selectedCategory.targetScene));
            }
            else
            {
                Debug.LogWarning("Target scene tidak ditentukan untuk kategori ini.");
            }
        }
    }

    public void SaveCorrectReplies()
    {
        PlayerPrefs.SetInt(
            "CorrectReplies_" + scoreManager.selectedCategoryData.category,
            correctReplies
        );
        PlayerPrefs.Save();
    }

    public void SaveWrongReplies()
    {
        PlayerPrefs.SetInt(
            "WrongReplies_" + scoreManager.selectedCategoryData.category,
            wrongReplies
        );
        PlayerPrefs.Save();
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        float remainingTime = countdownDuration;

        while (remainingTime > 0)
        {
            // Perbarui teks countdown
            if (countdownText != null)
            {
                countdownText.text = $"Memuat dalam {Mathf.Ceil(remainingTime)} detik...";
            }

            // Tunggu satu frame dan kurangi waktu
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        // Bersihkan teks countdown setelah selesai
        if (countdownText != null)
        {
            countdownText.text = string.Empty;
        }

        // Pindah ke scene yang ditentukan
        OnPlayScene(sceneName);
    }

    public void OnPlayScene(string index)
    {
        SceneManager.LoadScene(index);
    }

    public void ShowCorrectReply()
    {
        correctReplyIndex = selectedCategory.questions[currentQuestionIndex].correctReplyIndex;
        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i == correctReplyIndex)
            {
                replyButtons[i].interactable = true;
            }
            else
            {
                replyButtons[i].interactable = false;
            }
        }
    }

    public void ResetButtonColors()
    {
        foreach (var button in replyButtons)
        {
            button.interactable = true;
        }
    }

    public void ShowGameFinishedPanel()
    {
        gameFinishedPanel.SetActive(true);
        scoreText.text = "" + correctReplies + " / " + selectedCategory.questions.Length;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(
            "LastQuestion_Index_" + scoreManager.selectedCategoryData.category,
            currentQuestionIndex
        );

        PlayerPrefs.Save();
        Debug.Log("Progress saved");
        scoreManager.SaveScore(scoreManager.selectedCategoryData.category);
    }

    public void LoadProgress(string categoryName)
    {
        Debug.Log($"Mencoba memuat kategori: {categoryName}");

        QuestionData category = Array.Find(categories, c => c.category == categoryName);

        if (category != null)
        {
            scoreManager.selectedCategoryData = category;
            scoreManager.LoadScore(category.category);
            Debug.Log($"Kategori ditemukan: {category.category}, skor dimuat.");
            correctRepliesText.text = PlayerPrefs
                .GetInt("CorrectReplies_" + category.category, 0)
                .ToString();
            wrongRepliesText.text = PlayerPrefs
                .GetInt("WrongReplies_" + category.category, 0)
                .ToString();
        }
        else
        {
            Debug.LogError($"Kategori dengan nama {categoryName} tidak ditemukan.");
            Debug.Log("Daftar kategori yang tersedia:");
            foreach (var cat in categories)
            {
                Debug.Log($"Kategori: {cat.name}");
            }
        }
        DisplayQuestion();
    }
}
