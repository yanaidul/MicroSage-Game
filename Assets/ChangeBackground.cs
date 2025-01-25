using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> backgrounds; // Daftar GameObject latar belakang

    [SerializeField]
    private float interval = 5f; // Waktu interval dalam detik

    private int currentIndex; // Indeks latar belakang aktif
    private float timer = 0f; // Timer untuk interval waktu

    // Start is called before the first frame update
    void Start()
    {
        if (backgrounds.Count > 0)
        {
            // Pilih indeks awal secara acak
            currentIndex = Random.Range(0, backgrounds.Count);

            // Aktifkan latar belakang pertama berdasarkan indeks acak
            ActivateBackground(currentIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (backgrounds.Count > 1)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0f; // Reset timer
                SwitchToNextBackground();
            }
        }
    }

    // Mengaktifkan latar belakang berdasarkan indeks
    private void ActivateBackground(int index)
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].SetActive(i == index);
        }
    }

    // Beralih ke latar belakang berikutnya
    private void SwitchToNextBackground()
    {
        currentIndex = (currentIndex + 1) % backgrounds.Count;
        ActivateBackground(currentIndex);
    }
}
