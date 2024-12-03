using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// public class QuizManager : MonoBehaviour
// {
//     public QuestionData[] categories;
//     private QuestionData selectedCategory;
//     private int currentQuestionIndex = 0;
//     public TMP_Text questionText;
//     public Image questionImage;
//     public Button[] replyButtons;

//     public void SelectCategory(int categoryIndex)
//     {
//         selectedCategory = categories[categoryIndex];
//         currentQuestionIndex = 0;
//         DisplayQuestion();
//         DisplayQuestion();
//     }

//     void Start()
//     {
//         SelectCategory(0);
//     }

//     public void DisplayQuestion()
//     {
//         if (selectedCategory == null)
//             return;

//         var question = selectedCategory.questions[currentQuestionIndex];
//         questionText.text = question.questionText;
//         questionImage.sprite = question.QuestionImage;
//         for (int i = 0; i < replyButtons.Length; i++)
//         {
//             TMP_Text buttonText = replyButtons[i].GetComponentInChildren<TMP_Text>();
//             buttonText.text = question.replies[i];
//         }
//     }

//     public void OnReplySelected(int replyIndex)
//     {
//         if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
//         {
//             Debug.Log("Correct!");
//         }
//         else
//         {
//             Debug.Log("Incorrect!");
//         }
//         currentQuestionIndex++;
//         if (currentQuestionIndex < selectedCategory.questions.Length)
//         {
//             DisplayQuestion();
//         }
//         else
//         {
//             Debug.Log("Quiz completed!");
//         }
//     }
// }
public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestionData[] categories;
    private QuestionData selectedCategory;
    private int currentQuestionIndex = 0;

    public TextMeshProUGUI questionText;
    public Image questionImage;
    public Button[] replyButtons;

    [Header("Score Manager")]
    public ScoreManager scoreManager;
    public int correctReplyScore = 10;
    public int wrongReplyScore = 5;

    void Start()
    {
        SelectCategory(0);
    }

    public void SelectCategory(int categoryIndex)
    {
        selectedCategory = categories[categoryIndex];
        currentQuestionIndex = 0;
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        if (selectedCategory == null)
        {
            Debug.LogError("No category selected");
            return;
        }

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
                // Tampilkan jawaban
                replyButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI buttonText = replyButtons[i]
                    .GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = question.replies[i];

                // Tambahkan event listener untuk tombol
                int replyIndex = i; // Buat salinan untuk menghindari closure issue
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
        // Periksa apakah jawaban benar
        if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
        {
            scoreManager.AddScore(correctReplyScore);
            Debug.Log("Correct!");
        }
        else
        {
            scoreManager.SubtractScore(wrongReplyScore);
            Debug.Log("Incorrect!");
        }

        // Perbarui indeks pertanyaan
        currentQuestionIndex++;

        // Cek apakah sudah selesai atau tampilkan pertanyaan berikutnya
        if (currentQuestionIndex < selectedCategory.questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Di sini, Anda bisa menambahkan logika untuk akhir kuis (misalnya, tampilan hasil)
        }
    }

    // public void DisplayQuestion()
    // {
    //     if (selectedCategory == null)
    //     {
    //         Debug.LogError("No category selected");
    //         return;
    //     }
    //     var question = selectedCategory.questions[currentQuestionIndex];
    //     questionText.text = question.questionText;
    //     questionImage.sprite = question.QuestionImage;
    //     for (int i = 0; i < replyButtons.Length; i++)
    //     {
    //         TextMeshProUGUI buttonText = replyButtons[i].GetComponentInChildren<TextMeshProUGUI>();
    //         buttonText.text = question.replies[i];
    //     }
    // }

    // public void OnReplySelected(int replyIndex)
    // {
    //     if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
    //     {
    //         Debug.Log("Correct!");
    //     }
    //     else
    //     {
    //         Debug.Log("Incorrect!");
    //     }
    //     currentQuestionIndex++;
    //     if (currentQuestionIndex >= selectedCategory.questions.Length)
    //     {
    //         DisplayQuestion();
    //     }
    //     else
    //     {
    //         Debug.Log("Quiz completed!");
    //     }
    // }
}
