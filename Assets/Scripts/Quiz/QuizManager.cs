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

    [Header("Game Finished Manager")]
    public GameObject gameFinishedPanel;
    public TextMeshProUGUI BenarSalahText;
    public TextMeshProUGUI totalScoreText;

    [Header("LoadGameManager")]
    public GameObject PeringatanPanel;
    public TextMeshProUGUI PeringatanText;

    [Header("correctReplyIndex")]
    public int correctReplyIndex;
    int correctReplies;
    int wrongReplies;

    [Header("Index Benar / salah")]
    public TextMeshProUGUI correctRepliesText;
    public TextMeshProUGUI wrongRepliesText;

    public void Start()
    {
        PeringatanPanel.SetActive(false);
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

        if (selectedCategory.score >= 100)
        {
            ResetCategoryScore();
        }
        else
        {
            if (currentQuestionIndex >= selectedCategory.questions.Length)
            {
                ShowGameFinishedPanel();
                return;
            }
        }

        ResetButtonColors(); // Reset warna tombol jika diperlukan

        var question = selectedCategory.questions[currentQuestionIndex];
        questionText.text = question.questionText;

        if (question.QuestionImage != null)
        {
            questionImage.sprite = question.QuestionImage;
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i < question.replies.Length)
            {
                SetReplyButton(replyButtons[i], question.replies[i], i);
            }
            else
            {
                replyButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void ResetCategoryScore()
    {
        PeringatanPanel.SetActive(true);
        PeringatanText.text = selectedCategory.category.ToString();
        string categoryKey = selectedCategory.category;
        scoreManager.ResetScore();
        PlayerPrefs.SetInt($"CorrectReplies_{categoryKey}", 0);
        PlayerPrefs.SetInt($"WrongReplies_{categoryKey}", 0);
        PlayerPrefs.SetInt($"LastQuestion_Index_{categoryKey}", 0);
        PlayerPrefs.Save();

        Debug.Log("Score sudah di reset dari awal");
    }

    private void SetReplyButton(Button button, string replyText, int replyIndex)
    {
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TextMeshProUGUI>().text = replyText;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnReplySelected(replyIndex));
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
            SaveProgress();

            Debug.Log("Correct!");
        }
        else
        {
            wrongReplies++;
            // Pastikan skor tidak dikurangi jika sudah 0
            if (scoreManager.GetScore(selectedCategory.category) > 0)
            {
                scoreManager.SubtractScore(wrongReplyScore);
            }
            SaveProgress();

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
        }
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

        // Hitung skor akhir berdasarkan correctReplies dan wrongReplies yang sudah diperbarui
        var scoreAkhir = correctReplies - wrongReplies;

        // Pastikan skor akhir tidak kurang dari 0
        scoreAkhir = Mathf.Max(scoreAkhir, 0);

        // Tampilkan skor akhir dan total pertanyaan
        BenarSalahText.text = scoreAkhir.ToString() + " / " + selectedCategory.questions.Length;

        // Ambil skor dari ScoreManager
        int totalScore = scoreManager.GetScore(selectedCategory.category);
        totalScoreText.text = totalScore.ToString();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("CorrectReplies_" + selectedCategory.category, correctReplies);
        PlayerPrefs.SetInt("LastQuestion_Index_" + selectedCategory.category, currentQuestionIndex);
        PlayerPrefs.SetInt("WrongReplies_" + selectedCategory.category, wrongReplies);
        PlayerPrefs.Save();
        Debug.Log("Progress saved");

        // Update tampilan teks jawaban benar dan salah
        correctRepliesText.text = correctReplies.ToString();
        wrongRepliesText.text = wrongReplies.ToString();

        // Simpan skor ke ScoreManager
        scoreManager.SaveScore(selectedCategory.category);
    }

    public void LoadProgress(string categoryName)
    {
        Debug.Log($"Mencoba memuat kategori: {categoryName}");

        // Temukan kategori berdasarkan nama
        QuestionData category = Array.Find(categories, c => c.category == categoryName);

        if (category != null)
        {
            // Set data kategori yang dipilih
            scoreManager.selectedCategoryData = category;

            // Muat skor untuk kategori
            scoreManager.LoadScore(category.category);
            Debug.Log($"Kategori ditemukan: {category.category}, skor dimuat.");

            // Muat jumlah jawaban benar dan salah dari PlayerPrefs
            int correct = PlayerPrefs.GetInt("CorrectReplies_" + category.category, 0);
            int wrong = PlayerPrefs.GetInt("WrongReplies_" + category.category, 0);

            // Update tampilan teks jawaban benar dan salah
            correctRepliesText.text = correct.ToString();
            wrongRepliesText.text = wrong.ToString();

            // Ambil indeks pertanyaan terakhir dari PlayerPrefs dan lanjutkan dari sana
            currentQuestionIndex = PlayerPrefs.GetInt("LastQuestion_Index_" + category.category, 0);
            Debug.Log(
                $"Indeks pertanyaan terakhir di progress PlayerPrefs: {currentQuestionIndex} dan last question {PlayerPrefs.GetInt("LastQuestion_Index_" + category.category)}"
            );
            BenarSalahText.text = correct.ToString() + " / " + category.questions.Length;
            if (currentQuestionIndex == category.questions.Length)
            {
                PeringatanPanel.SetActive(true);
                PeringatanText.text = category.category.ToString();
                BenarSalahText.text =
                    ""
                    + scoreManager.GetScore(category.score.ToString()).ToString()
                    + " / "
                    + category.questions.Length;
                //  BenarSalahText.text =
                // "" + scoreAkhir.ToString() + " / " + selectedCategory.questions.Length;
            }
            else
            {
                DisplayQuestion();
            }
        }
        else
        {
            Debug.LogError($"Kategori dengan nama {categoryName} tidak ditemukan.");
            Debug.Log("Daftar kategori yang tersedia:");
            foreach (var cat in categories)
            {
                Debug.Log($"Kategori: {cat.category}");
            }
        }

        DisplayQuestion();
    }
}
