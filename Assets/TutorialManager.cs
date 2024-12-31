using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TutorialMainManager tutorialMainManager;
    public LoadingScreen loadingScreen;
    public TimerTutorial timerTutorial;
    public FieldTutorial fieldTutorial;
    public GameObject[] tutorialPages; // Array untuk menyimpan halaman tutorial
    public int currentPageIndex = 0; // Indeks halaman aktif
    public int indexStartTutorialInteractable = 8; // Indeks untuk memulai tutorial interaktif

    [Header("Header dan Tutorial Text")]
    public string[] headerTexts; // Text untuk header tutorial
    public string[] tutorialTexts; // Text untuk deskripsi tutorial

    [Header("UI NYA")]
    public TextMeshProUGUI textKontainerTutorial; //textTutorialPage
    public GameObject triggerSkipTutorial; //panelSkipTutorial
    public GameObject kontainerTutorial; //kontainerPanelTutorial
    public GameObject endTutorial; //SelesaiTutorial

    [Header("Object Tutorial")]
    public GameObject[] tutorialObjects; // Array untuk objek tutorial
    private bool objectsActivated = false;

    [Header("Text simpan tutorial")]
    public TextMeshProUGUI[] textTutorial;

    [Header("Prev / Next Button")]
    public Button nextButton;
    public Button prevButton;

    [Header("Pindah tempat")]
    public RectTransform kontainerTutorialUI;
    public Vector2[] positions;

    [Header("Tujuan selesai scene")]
    public int sceneIndex;
    private string filePath;

    public void Awake()
    {
        PernahTutorial();
    }

    public void Start()
    {
        currentPageIndex = 0; // Mulai dari halaman pertama
        OnEnable();
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    //

    public void PernahTutorial()
    {
        filePath = Path.Combine(Application.persistentDataPath, "visited.txt");

        if (File.Exists(filePath))
        {
            Debug.Log("Aplikasi sudah pernah dibuka.");
            // Lakukan sesuatu untuk pengguna lama
            loadingScreen.Start();
            loadingScreen.LoadScene(sceneIndex);
        }
        else
        {
            Debug.Log("Aplikasi belum pernah dibuka.");
            File.WriteAllText(filePath, "visited");
            // Lakukan sesuatu untuk pengguna baru
        }
    }

    public void OnEnable()
    {
        triggerSkipTutorial.SetActive(true);
        tutorialMainManager.DisableTile();
        endTutorial.SetActive(false);
        kontainerTutorial.SetActive(false);
    }

    public void NextTutorial()
    {
        UpdatePage();
    }

    private void ResetTutorial()
    {
        currentPageIndex = 0; // Set index ke 0

        UpdatePage();
    }

    public void SkipTutorial()
    {
        triggerSkipTutorial.SetActive(false);
        kontainerTutorial.SetActive(false);
        endTutorial.SetActive(false);
        tutorialMainManager.EnableTile();
        // Aktifkan semua tutorialObjects
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(true);
        }

        Debug.Log("Semua objek tutorial diaktifkan.");
    }

    private void UpdatePage()
    {
        Debug.Log("Page aktif: " + currentPageIndex); // Debug
        kontainerTutorial.SetActive(true);
        triggerSkipTutorial.SetActive(false);

        // Perbarui halaman aktif
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == currentPageIndex);
        }

        // Atur teks dan objek untuk halaman aktif
        if (currentPageIndex < textTutorial.Length)
        {
            SetTextForPageIndex(currentPageIndex);
        }

        if (currentPageIndex < tutorialObjects.Length)
        {
            SetObjectsForPageIndex(currentPageIndex);
        }

        timerTutorial.OnPause();
        // Jika currentPageIndex >= 8, panggil UpdateTutorial
        if (currentPageIndex >= indexStartTutorialInteractable)
        {
            tutorialMainManager.EnableTile();
            timerTutorial.OnResume();
            // Aktifkan semua tutorialObjects
            foreach (GameObject obj in tutorialObjects)
            {
                obj.SetActive(true);
            }
            objectsActivated = true; // Tandai objek sebagai aktif

            // Sesuaikan _currentIndexTutorial berdasarkan currentPageIndex
            tutorialMainManager._currentIndexTutorial = 0;

            // Pastikan posisi diatur ulang berdasarkan _currentIndexTutorial
            // tutorialMainManager.UpdateTutorial();
        }
        else
        {
            tutorialMainManager.DisableTile();
            fieldTutorial.OnRestart();

            objectsActivated = false;

            Debug.Log("UpdateTutorial tidak dipanggil karena currentPageIndex < 8.");
        }
        if (currentPageIndex == tutorialPages.Length - 1)
        {
            endTutorial.SetActive(true);
            PernahTutorial();
        }

        // Atur tombol navigasi

        prevButton.interactable = currentPageIndex > 0;
        nextButton.interactable = currentPageIndex < tutorialPages.Length - 1;

        // Perbarui posisi UI hanya jika posisi valid
        if (currentPageIndex < positions.Length)
        {
            MoveUI(positions[currentPageIndex]);
        }
    }

    // private void UpdatePage()
    // {
    //     Debug.Log("Page aktif: " + currentPageIndex);
    //     kontainerTutorial.SetActive(true);
    //     triggerSkipTutorial.SetActive(false);

    //     // Perbarui halaman aktif
    //     for (int i = 0; i < tutorialPages.Length; i++)
    //     {
    //         tutorialPages[i].SetActive(i == currentPageIndex);
    //     }
    //     if (currentPageIndex < textTutorial.Length)
    //     {
    //         SetTextForPageIndex(currentPageIndex);
    //     }

    //     if (currentPageIndex < tutorialObjects.Length)
    //     {
    //         SetObjectsForPageIndex(currentPageIndex);
    //     }
    //     if (currentPageIndex < indexStartTutorialInteractable)
    //     {
    //         tutorialMainManager.DisableTile();
    //         fieldTutorial.OnRestart();
    //         tutorialMainManager.tutupBawah.SetActive(false);
    //         tutorialMainManager._currentIndexTutorial = 0; // Reset tutorial index
    //         Debug.Log(
    //             "TutorialMainManager di-reset karena currentPageIndex < "
    //                 + indexStartTutorialInteractable
    //         );
    //     }
    //     else
    //     {
    //         foreach (GameObject obj in tutorialObjects)
    //         {
    //             obj.SetActive(true);
    //         }
    //         tutorialMainManager.EnableTile();
    //         tutorialMainManager.UpdateTutorial(); // Perbarui logika tutorial
    //     }
    //     if (currentPageIndex >= indexStartTutorialInteractable)
    //     {
    //         prevButton.interactable = tutorialMainManager._currentIndexTutorial > 0;
    //         nextButton.interactable =
    //             tutorialMainManager._currentIndexTutorial
    //             < tutorialMainManager.positionsBawah.Length - 1;
    //     }
    //     else
    //     {
    //         // Atur tombol navigasi berdasarkan currentPageIndex
    //         prevButton.interactable = currentPageIndex > 0;
    //         nextButton.interactable = currentPageIndex < tutorialPages.Length - 1;
    //     }
    //     // Pastikan posisi UI diperbarui
    //     if (currentPageIndex < positions.Length)
    //     {
    //         MoveUI(positions[currentPageIndex]);
    //     }
    // }

    private void SetObjectsForPageIndex(int index)
    {
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            if (i == index)
            {
                tutorialObjects[i].SetActive(true); // Aktifkan jika indeks sesuai
            }
            else
            {
                tutorialObjects[i].SetActive(false); // Nonaktifkan jika indeks tidak sesuai
            }
        }
    }

    private void SetTextForPageIndex(int index)
    {
        if (index >= 0 && index < textTutorial.Length)
        {
            // Mengatur teks header dan deskripsi ke UI yang berbeda
            textKontainerTutorial.text = GetHeaderTextForPageIndex(index);
            textTutorial[index].text = GetTutorialTextForPageIndex(index);
        }
    }

    private string GetHeaderTextForPageIndex(int index)
    {
        if (index >= 0 && index < headerTexts.Length)
        {
            return headerTexts[index];
        }
        else
        {
            return "Header tidak ditemukan.";
        }
    }

    // Fungsi untuk mendapatkan teks deskripsi tutorial
    private string GetTutorialTextForPageIndex(int index)
    {
        if (index >= 0 && index < tutorialTexts.Length)
        {
            return tutorialTexts[index];
        }
        else
        {
            return "Deskripsi tidak ditemukan.";
        }
    }

    public void NextPage() // NextButton
    {
        if (currentPageIndex < tutorialPages.Length - 1)
        {
            currentPageIndex++; // Tambah index
            Debug.Log("Page selanjutnya: " + currentPageIndex); // Debug
            UpdatePage(); // Perbarui tampilan halaman
        }

        // Jika currentPageIndex >= 8, delegasikan ke tutorialMainManager
        if (currentPageIndex >= 8)
        {
            tutorialMainManager.NextButtonClicked(); // Panggil fungsi NextButtonClicked()
        }
    }

    public void PrevPage() // PrevButton
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // Kurangi index
            Debug.Log("Page sebelumnya: " + currentPageIndex); // Debug
            UpdatePage(); // Perbarui tampilan halaman
        }

        // Jika currentPageIndex >= 8, delegasikan ke tutorialMainManager
        if (currentPageIndex >= 8)
        {
            tutorialMainManager.PrevButtonClicked(); // Panggil fungsi PrevButtonClicked()
        }
    }

    private void MoveUI(Vector2 targetPosition)
    {
        kontainerTutorialUI.anchoredPosition = targetPosition;
    }
}
