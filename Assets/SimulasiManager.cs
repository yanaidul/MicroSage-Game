using System.Collections.Generic;
using UnityEngine;

public class SimulasiManager : MonoBehaviour
{
    [Header("Kesuluran IC")]
    public List<GameObject> _StarsAND = new();
    public List<GameObject> _StarsOR = new();
    public List<GameObject> _StarsNOT = new();
    public List<GameObject> _StarsNOR = new();
    public List<GameObject> _StarsNAND = new();
    public List<GameObject> _StarsXOR = new();
    public List<GameObject> _StarsXNOR = new();

    // Dictionary untuk menyimpan nilai bintang berdasarkan StageType dan TypeIC
    public Dictionary<(StageType, TypeIC), int> _nilaiBintang = new();

    [SerializeField]
    private BintangData bintangData; // Referensi ke ScriptableObject

    private void Start()
    {
        // Memuat nilai bintang dari PlayerPrefs ke ScriptableObject
        bintangData.LoadData(); // Muat data dari PlayerPrefs
        LoadNilaiBintang();
        UpdateStars();
    }

    public void ResetStars()
    {
        // Tampilkan konfirmasi kepada pengguna
        if (ConfirmReset())
        {
            // Nonaktifkan semua bintang
            foreach (StageType stage in (StageType[])System.Enum.GetValues(typeof(StageType)))
            {
                List<GameObject> stars = GetStarsForStage(stage);
                foreach (var star in stars)
                {
                    star.SetActive(false);
                }
            }

            // Hapus data yang berkaitan dengan SimulasiManager dari PlayerPrefs
            foreach (StageType stage in (StageType[])System.Enum.GetValues(typeof(StageType)))
            {
                foreach (TypeIC type in (TypeIC[])System.Enum.GetValues(typeof(TypeIC)))
                {
                    string key = $"{stage}_{type}"; // Ganti dengan format kunci yang sesuai
                    PlayerPrefs.DeleteKey(key); // Hapus kunci spesifik
                }
            }
            PlayerPrefs.Save(); // Pastikan untuk menyimpan perubahan

            Debug.Log(
                "Semua bintang dinonaktifkan dan data SimulasiManager telah dihapus dari PlayerPrefs."
            );
        }
        else
        {
            Debug.Log("Reset dibatalkan oleh pengguna.");
        }
    }

    // Metode untuk menampilkan konfirmasi reset
    private bool ConfirmReset()
    {
        // Di sini Anda bisa menggunakan UI untuk meminta konfirmasi dari pengguna
        // Misalnya, menggunakan dialog atau popup
        // Untuk contoh ini, kita akan mengembalikan true sebagai placeholder
        return true; // Ganti dengan logika konfirmasi yang sesuai
    }

    private void LoadNilaiBintang()
    {
        foreach (StageType stage in (StageType[])System.Enum.GetValues(typeof(StageType)))
        {
            foreach (TypeIC type in (TypeIC[])System.Enum.GetValues(typeof(TypeIC)))
            {
                if (bintangData.nilaiBintang.TryGetValue((stage, type), out int score))
                {
                    _nilaiBintang[(stage, type)] = score; // Simpan nilai bintang
                }
            }
        }
    }

    private void UpdateStars()
    {
        // Mengupdate bintang berdasarkan nilai bintang yang ada
        foreach (StageType stage in (StageType[])System.Enum.GetValues(typeof(StageType)))
        {
            int totalCompleted = 0;

            // Hitung berapa banyak IC yang selesai
            foreach (TypeIC type in (TypeIC[])System.Enum.GetValues(typeof(TypeIC)))
            {
                if (_nilaiBintang.TryGetValue((stage, type), out int score) && score > 0)
                {
                    totalCompleted++;
                }
            }

            // Aktifkan bintang berdasarkan jumlah IC yang selesai
            ActivateStars(stage, totalCompleted);
        }
    }

    private void ActivateStars(StageType stageType, int completedCount)
    {
        List<GameObject> stars = GetStarsForStage(stageType);

        // Nonaktifkan semua bintang terlebih dahulu
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // Aktifkan bintang sesuai dengan jumlah IC yang selesai
        for (int i = 0; i < completedCount; i++)
        {
            if (i < stars.Count)
            {
                stars[i].SetActive(true);
            }
        }

        Debug.Log($"Bintang untuk {stageType} diaktifkan: {completedCount}");
    }

    private List<GameObject> GetStarsForStage(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.AND:
                return _StarsAND;
            case StageType.OR:
                return _StarsOR;
            case StageType.NOT:
                return _StarsNOT;
            case StageType.NOR:
                return _StarsNOR;
            case StageType.NAND:
                return _StarsNAND;
            case StageType.XOR:
                return _StarsXOR;
            case StageType.XNOR:
                return _StarsXNOR;
            default:
                return new List<GameObject>();
        }
    }

    public int GetNilaiBintang(StageType stageType, TypeIC typeIC)
    {
        return _nilaiBintang.TryGetValue((stageType, typeIC), out int score) ? score : 0;
    }

    public void CheckOverallBintang(StageType stageType)
    {
        int totalBintang = 0;

        // Cek nilai bintang untuk semua TypeIC dalam stageType yang sama
        foreach (TypeIC type in (TypeIC[])System.Enum.GetValues(typeof(TypeIC)))
        {
            totalBintang = Mathf.Max(totalBintang, GetNilaiBintang(stageType, type));
        }

        Debug.Log($"Total bintang untuk {stageType} adalah {totalBintang}");
        // Lakukan sesuatu dengan totalBintang, misalnya simpan ke PlayerPrefs
    }

    public void SaveBintangData()
    {
        // Simpan data bintang ke PlayerPrefs
        bintangData.SaveData();
    }
}
