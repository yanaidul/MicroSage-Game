using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Image loadingBarImage;
    public GameObject _loadingUI;
    public float fillDuration = 5f;

    public float targetFill = 1f;
    private string filePath;
    public float delay = 3f;
    public GameObject buttonSkip; // Tambahkan referensi ke buttonSkip di Inspector
    private int currentStageID;

    public void Start()
    {
        // _loadingUI.SetActive(true);
        loadingBarImage.fillAmount = 0f;
        buttonSkip.SetActive(false); // Pastikan buttonSkip disembunyikan saat awal
    }

    public void ResetTutorial()
    {
        filePath = Path.Combine(Application.persistentDataPath, "visited.txt");

        if (File.Exists(filePath))
        {
            File.Delete(filePath); // Menghapus file "visited.txt"
            Debug.Log("File 'visited.txt' berhasil dihapus. Status aplikasi direset.");
            LoadScene(4); // Load scene tutorial
        }
        else
        {
            Debug.Log("File 'visited.txt' tidak ditemukan. Tidak ada yang perlu dihapus.");
            LoadScene(1);
        }
    }

    public void Awalan()
    {
        _loadingUI.SetActive(true);
        filePath = Path.Combine(Application.persistentDataPath, "visited.txt");

        if (File.Exists(filePath))
        {
            Debug.Log("Tutorial sudah pernah dibuka.");
            // Lakukan sesuatu untuk pengguna lama
            LoadScene(1);
        }
        else
        {
            Debug.Log("Tutorial belum pernah dibuka.");
            LoadScene(4);
        }
    }

    public void LoadScene(int stageID)
    {
        _loadingUI.SetActive(true);
        currentStageID = stageID; // Simpan stageID yang dimuat
        CheckSkipButton(stageID); // Periksa apakah buttonSkip harus muncul
        StartCoroutine(LoadScene_Couroutine(stageID));
    }

    private void CheckSkipButton(int stageID)
    {
        // Periksa apakah stage sudah selesai
        int completedStage = PlayerPrefs.GetInt("StageCompleted", -1);
        if (completedStage >= stageID)
        {
            buttonSkip.SetActive(true); // Munculkan buttonSkip jika sudah pernah diselesaikan
        }
        else
        {
            buttonSkip.SetActive(false);
        }
    }

    public IEnumerator LoadScene_Couroutine(int sceneIndex)
    {
        if (loadingBarImage == null)
        {
            Debug.LogError("loadingBarImage belum dihubungkan!");
            yield break;
        }

        // Mulai memuat scene secara asinkron, tanpa mengaktifkan langsung
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        // Tunggu sebelum memulai animasi pengisian
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        Debug.Log("Persiapan memulai pengisian loading bar...");
        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;

            // Hitung nilai fillAmount berdasarkan waktu yang telah berlalu
            float progress = Mathf.Lerp(0f, targetFill, elapsedTime / fillDuration);
            loadingBarImage.fillAmount = progress;

            yield return null; // Tunggu frame berikutnya
        }

        // Pastikan loading bar penuh setelah selesai
        loadingBarImage.fillAmount = targetFill;

        Debug.Log("Scene siap diaktifkan...");

        operation.allowSceneActivation = true; // Aktifkan scene setelah loading selesai
    }

    public void OnStageCompleted(int stageID)
    {
        // Simpan stage yang sudah diselesaikan
        int currentCompleted = PlayerPrefs.GetInt("StageCompleted", -1);
        if (stageID > currentCompleted)
        {
            PlayerPrefs.SetInt("StageCompleted", stageID);
        }
    }

    public void loadSceneLangsung(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
