// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class QuizManagerBackup : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public QuestionData[] categories;
//     private QuestionData selectedCategory;

//     [Header("Question Display")]
//     [SerializeField]
//     private int currentQuestionIndex = 0;

//     public TextMeshProUGUI questionText;
//     public Image questionImage;
//     public Button[] replyButtons;

//     [Header("Score Manager")]
//     public ScoreManager scoreManager;

//     [SerializeField]
//     public int correctReplyScore = 10;

//     [SerializeField]
//     public int wrongReplyScore = 5;
//     public TextMeshProUGUI scoreText;

//     [Header("correctReplyIndex")]
//     public int correctReplyIndex;
//     int correctReplies;
//     int wrongReplies;

//     [Header("Index Benar / salah")]
//     public TextMeshProUGUI correctRepliesText;
//     public TextMeshProUGUI wrongRepliesText;

//     [Header("Game Finished Panel")]
//     public GameObject gameFinishedPanel;

//     [Header("Time Countdown")]
//     public TextMeshProUGUI countdownText;

//     [SerializeField]
//     public float countdownDuration = 3f;

//     public void Start()
//     {
//         int selectedCategoryIndex = PlayerPrefs.GetInt("SelectedCategory", 0);
//         gameFinishedPanel.SetActive(false);
//         SelectCategory(selectedCategoryIndex);
//         LoadProgress(selectedCategory.category);
//     }

//     public void SelectCategory(int categoryIndex)
//     {
//         selectedCategory = categories[categoryIndex];
//         currentQuestionIndex = 0;
//         scoreManager.selectedCategoryData = selectedCategory;
//         DisplayQuestion();
//     }

//     public void DisplayQuestion()
//     {
//         if (selectedCategory == null)
//         {
//             Debug.LogError("No category selected");
//             return;
//         }

//         ResetButtonColors(); // Reset warna tombol jika diperlukan

//         // Ambil pertanyaan berdasarkan indeks
//         var question = selectedCategory.questions[currentQuestionIndex];

//         // Tampilkan teks pertanyaan
//         questionText.text = question.questionText;

//         // Tampilkan gambar pertanyaan jika ada
//         if (question.QuestionImage != null)
//         {
//             questionImage.sprite = question.QuestionImage;
//         }

//         // Validasi jumlah tombol yang sesuai dengan jumlah jawaban
//         int totalReplies = question.replies.Length;
//         for (int i = 0; i < replyButtons.Length; i++)
//         {
//             if (i < totalReplies)
//             {
//                 // Jika tombol diperlukan, pastikan tombol aktif dan terhubung dengan jawaban
//                 replyButtons[i].gameObject.SetActive(true);
//                 TextMeshProUGUI buttonText = replyButtons[i]
//                     .GetComponentInChildren<TextMeshProUGUI>();
//                 buttonText.text = question.replies[i];

//                 // Mendaftarkan event listener hanya jika belum ada
//                 int replyIndex = i;
//                 replyButtons[i].onClick.RemoveAllListeners();
//                 replyButtons[i].onClick.AddListener(() => OnReplySelected(replyIndex));
//             }
//             else
//             {
//                 // Sembunyikan tombol jika tidak diperlukan
//                 replyButtons[i].gameObject.SetActive(false);
//             }
//         }
//     }

//     public void OnReplySelected(int replyIndex)
//     {
//         if (
//             selectedCategory == null
//             || selectedCategory.questions == null
//             || selectedCategory.questions.Length == 0
//         )
//         {
//             Debug.LogError("Kategori atau pertanyaan belum diatur.");
//             return;
//         }
//         // Periksa apakah jawaban benar
//         if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
//         {
//             scoreManager.AddScore(correctReplyScore);
//             correctReplies++;
//             SaveProgress();
//             correctRepliesText.text = correctReplies.ToString();
//             Debug.Log("Correct!");
//         }
//         else
//         {
//             wrongReplies++;
//             scoreManager.SubtractScore(wrongReplyScore);
//             SaveProgress();
//             wrongRepliesText.text = wrongReplies.ToString();
//             Debug.Log("Incorrect!");
//         }

//         // Perbarui indeks pertanyaan
//         currentQuestionIndex++;
//         SaveProgress();
//         // Cek apakah sudah selesai atau tampilkan pertanyaan berikutnya
//         if (currentQuestionIndex < selectedCategory.questions.Length)
//         {
//             int remainingQuestions = selectedCategory.questions.Length - currentQuestionIndex;
//             Debug.Log("Sisa pertanyaan: " + remainingQuestions);
//             DisplayQuestion();
//         }
//         else
//         {
//             Debug.Log("Quiz completed!");
//             ShowGameFinishedPanel();

//             if (!string.IsNullOrEmpty(selectedCategory.targetScene))
//             {
//                 // Panggil coroutine untuk memberi jeda sebelum pindah scene
//                 StartCoroutine(LoadSceneWithDelay(selectedCategory.targetScene));
//             }
//             else
//             {
//                 Debug.LogWarning("Target scene tidak ditentukan untuk kategori ini.");
//             }
//         }
//     }

//     private IEnumerator LoadSceneWithDelay(string sceneName)
//     {
//         float remainingTime = countdownDuration;

//         while (remainingTime > 0)
//         {
//             // Perbarui teks countdown
//             if (countdownText != null)
//             {
//                 countdownText.text = $"Memuat dalam {Mathf.Ceil(remainingTime)} detik...";
//             }

//             // Tunggu satu frame dan kurangi waktu
//             yield return new WaitForSeconds(1f);
//             remainingTime--;
//         }

//         // Bersihkan teks countdown setelah selesai
//         if (countdownText != null)
//         {
//             countdownText.text = string.Empty;
//         }

//         // Pindah ke scene yang ditentukan
//         OnPlayScene(sceneName);
//     }

//     public void OnPlayScene(string index)
//     {
//         SceneManager.LoadScene(index);
//     }

//     public void ShowCorrectReply()
//     {
//         correctReplyIndex = selectedCategory.questions[currentQuestionIndex].correctReplyIndex;
//         for (int i = 0; i < replyButtons.Length; i++)
//         {
//             if (i == correctReplyIndex)
//             {
//                 replyButtons[i].interactable = true;
//             }
//             else
//             {
//                 replyButtons[i].interactable = false;
//             }
//         }
//     }

//     public void ResetButtonColors()
//     {
//         foreach (var button in replyButtons)
//         {
//             button.interactable = true;
//         }
//     }

//     public void ShowGameFinishedPanel()
//     {
//         gameFinishedPanel.SetActive(true);
//         var scoreAkhir =
//             PlayerPrefs.GetInt("CorrectReplies_" + selectedCategory.category, correctReplies)
//             - PlayerPrefs.GetInt("WrongReplies_" + selectedCategory.category, wrongReplies);
//         scoreText.text = "" + scoreAkhir.ToString() + " / " + selectedCategory.questions.Length;
//     }

//     public void SaveProgress()
//     {
//         PlayerPrefs.SetInt(
//             "CorrectReplies_" + scoreManager.selectedCategoryData.category,
//             correctReplies
//         );
//         PlayerPrefs.SetInt(
//             "LastQuestion_Index_" + scoreManager.selectedCategoryData.category,
//             currentQuestionIndex
//         );
//         PlayerPrefs.SetInt(
//             "WrongReplies_" + scoreManager.selectedCategoryData.category,
//             wrongReplies
//         );
//         PlayerPrefs.Save();
//         Debug.Log("Progress saved");
//         scoreManager.SaveScore(scoreManager.selectedCategoryData.category);
//     }

//     public void LoadProgress(string categoryName)
//     {
//         Debug.Log($"Mencoba memuat kategori: {categoryName}");

//         // Temukan kategori berdasarkan nama
//         QuestionData category = Array.Find(categories, c => c.category == categoryName);

//         if (category != null)
//         {
//             // Set data kategori yang dipilih
//             scoreManager.selectedCategoryData = category;

//             // Muat skor untuk kategori
//             scoreManager.LoadScore(category.category);
//             Debug.Log($"Kategori ditemukan: {category.category}, skor dimuat.");

//             // Muat jumlah jawaban benar dan salah dari PlayerPrefs
//             int correct = PlayerPrefs.GetInt("CorrectReplies_" + category.category, 0);
//             int wrong = PlayerPrefs.GetInt("WrongReplies_" + category.category, 0);

//             // Update tampilan teks jawaban benar dan salah
//             correctRepliesText.text = correct.ToString();
//             wrongRepliesText.text = wrong.ToString();

//             // Ambil indeks pertanyaan terakhir dari PlayerPrefs dan lanjutkan dari sana
//             currentQuestionIndex = PlayerPrefs.GetInt("LastQuestion_Index_" + category, 0);
//             Debug.Log(
//                 $"Indeks pertanyaan terakhir di progress PlayerPrefs: {currentQuestionIndex}"
//             );
//             if (currentQuestionIndex == category.questions.Length)
//             {
//                 ShowGameFinishedPanel();
//                 if (!string.IsNullOrEmpty(selectedCategory.targetScene))
//                 {
//                     // Panggil coroutine untuk memberi jeda sebelum pindah scene
//                     StartCoroutine(LoadSceneWithDelay(selectedCategory.targetScene));
//                 }
//                 else
//                 {
//                     Debug.LogWarning("Target scene tidak ditentukan untuk kategori ini.");
//                 }
//             }
//             else
//             {
//                 DisplayQuestion();
//             }
//             // Debug.Log($"Indeks pertanyaan terakhir: {currentQuestionIndex}");
//         }
//         else
//         {
//             Debug.LogError($"Kategori dengan nama {categoryName} tidak ditemukan.");
//             Debug.Log("Daftar kategori yang tersedia:");
//             foreach (var cat in categories)
//             {
//                 Debug.Log($"Kategori: {cat.category}");
//             }
//         }

//         // Menampilkan pertanyaan berdasarkan indeks yang diambil
//         DisplayQuestion();
//     }
// }
