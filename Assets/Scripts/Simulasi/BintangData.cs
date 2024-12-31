using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BintangData", menuName = "ScriptableObjects/BintangData", order = 1)]
public class BintangData : ScriptableObject
{
    public Dictionary<(StageType, TypeIC), int> nilaiBintang =
        new Dictionary<(StageType, TypeIC), int>();

    public void SaveData()
    {
        foreach (var entry in nilaiBintang)
        {
            string key = $"{entry.Key.Item1}_{entry.Key.Item2}";
            PlayerPrefs.SetInt(key, entry.Value);
        }
        PlayerPrefs.Save(); // Simpan perubahan
    }

    public void LoadData()
    {
        foreach (StageType stage in (StageType[])System.Enum.GetValues(typeof(StageType)))
        {
            foreach (TypeIC type in (TypeIC[])System.Enum.GetValues(typeof(TypeIC)))
            {
                string key = $"{stage}_{type}";
                int score = PlayerPrefs.GetInt(key, 0);
                nilaiBintang[(stage, type)] = score; // Muat nilai bintang
            }
        }
    }
}
