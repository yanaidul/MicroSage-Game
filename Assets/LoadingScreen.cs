using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Image loadingBarImage;

    public float fillDuration = 5f;

    public float targetFill = 1f;

    public float delay = 3f;

    // public void LoadScene(int sceneIndex)
    // {
    //     loadingBarImage.fillAmount = 0f;
    //     StartCoroutine(LoadScene_Couroutine(sceneIndex));
    // }

    // public IEnumerator LoadScene_Couroutine(int sceneIndex)
    // {
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    //     operation.allowSceneActivation = false;
    //     yield return new WaitForSeconds(delay);

    //     float elapsedTime = 0f;

    //     while (elapsedTime < fillDuration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         loadingBarImage.fillAmount = Mathf.Lerp(0f, targetFill, elapsedTime / fillDuration);
    //         if (elapsedTime >= fillDuration)
    //         {
    //             loadingBarImage.fillAmount = targetFill;
    //             operation.allowSceneActivation = true;
    //         }
    //         yield return null;
    //     }
    //     // AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    //     // operation.allowSceneActivation = false;
    //     // float progress = 0;
    //     // yield return new WaitForSeconds(delay);
    //     // while (!operation.isDone)
    //     // {
    //     //     progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
    //     //     progressSlider.value = progress;
    //     //     if (progress >= 0.9f)
    //     //     {
    //     //         loadingBarImage.fillAmount = targetFill;
    //     //         operation.allowSceneActivation = true;
    //     //     }
    //     //     yield return null;
    //     // }
    // }
    public void Start()
    {
        loadingBarImage.fillAmount = 0f;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadScene_Couroutine(sceneIndex));
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
}
