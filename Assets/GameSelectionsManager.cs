using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelectionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    //
    void start()
    {
        InisialisasiSkor();
    }

    public void OnNameSimulationSelected(string nameCategory)
    {
        if (!PlayerPrefs.HasKey("Score_Simulations_" + nameCategory))
        {
            PlayerPrefs.SetInt("Score_Simulations_" + nameCategory, 0);
        }
    }

    public void InisialisasiSkor()
    {
        string[] categories = { "AND", "OR", "NAND", "NOR", "NOT", "XOR", "XNOR" };
        string[] kategoriIC = { "TTL", "CMOS", "HCMOS" };

        foreach (string category in categories)
        {
            foreach (string ic in kategoriIC)
            {
                string key = $"Score_Simulations_{category}_{ic}";
                PlayerPrefs.SetInt(key, 0);
            }
            string keyTotal = $"Score_Simulations_{category}";
            PlayerPrefs.SetInt(keyTotal, 0);
        }
    }

    public void UpdateSkor(string category, string ic, int nilai)
    {
        string key = $"Score_Simulations_{category}_{ic}";
        PlayerPrefs.SetInt(key, nilai);

        string keyTotal = $"Score_Simulations_{category}";
        int total = 0;
        foreach (string icType in new[] { "TTL", "CMOS", "HCMOS" })
        {
            total += PlayerPrefs.GetInt($"Score_Simulations_{category}_{icType}");
        }
        PlayerPrefs.SetInt(keyTotal, total);
    }
}
