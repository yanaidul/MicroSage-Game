using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMainManager : MonoBehaviour
{
    [SerializeField]
    private TutorialManager _tutorialManager;
    public FieldTutorial fieldTutorial;

    [Header("Tile")]
    public GameObject[] tileAwal;
    public GameObject[] tileTujuan;

    [Header("Penutup Tutorial")]
    public GameObject tutupBawah;
    public RectTransform penutupBawah;
    public Vector2[] positionsBawah;

    [Header("Index Game Tutorial")]
    public int _currentIndexTutorial;
    public bool canProceed;

    [SerializeField]
    private int patternTile = 0;

    public void Start()
    {
        tutupBawah.SetActive(false);
        ResetPositions();
        UpdateMissions();
    }

    public void UpdateMissions()
    {
        if (_tutorialManager.currentPageIndex >= 8)
        {
            UpdateTutorial();
        }
        else if (_tutorialManager.currentPageIndex < 8)
        {
            MovePanel(positionsBawah[0]);
            tutupBawah.SetActive(false);

            Debug.Log("Kembali ke posisi awal tutorial, index 0.");
        }
    }

    public void DisableTile()
    {
        foreach (GameObject tileAwal in tileAwal)
        {
            tileAwal.SetActive(false);
        }
        foreach (GameObject tileTujuan in tileTujuan)
        {
            tileTujuan.SetActive(false);
        }
    }

    public void EnableTile()
    {
        foreach (GameObject tileAwal in tileAwal)
        {
            tileAwal.SetActive(true);
        }
        foreach (GameObject tileTujuan in tileTujuan)
        {
            tileTujuan.SetActive(true);
        }
    }

    private void UpdateTutorial()
    {
        List<int> solvedConnectionIds = _tutorialManager.fieldTutorial.GetSolvedConnections();

        if (solvedConnectionIds.Count > 0)
        {
            foreach (int connectionId in solvedConnectionIds)
            {
                // Cek apakah tutorial sebelumnya telah selesai

                switch (connectionId)
                {
                    case 1: // Tutorial kedua
                        patternTile = 1;
                        canProceed = _tutorialManager.fieldTutorial.IsSolved(patternTile);
                        Debug.Log("Pattern di case 1 sekarang bernilai : " + patternTile);
                        _tutorialManager.prevButton.interactable = true;
                        _tutorialManager.nextButton.interactable = canProceed;
                        _tutorialManager.nextButton.onClick.AddListener(NextButtonClicked);

                        break;

                    case 2: // Tutorial ketiga
                        patternTile = 2;
                        canProceed = _tutorialManager.fieldTutorial.IsSolved(patternTile);
                        Debug.Log("Pattern di case 2 sekarang bernilai : " + patternTile);
                        _tutorialManager.prevButton.interactable = true;
                        _tutorialManager.nextButton.interactable = canProceed;
                        _tutorialManager.nextButton.onClick.AddListener(NextButtonClicked);
                        _tutorialManager.prevButton.onClick.AddListener(PrevButtonClicked);

                        Debug.Log("Tutorial ketiga aktif, index 2.");
                        Debug.Log("Apakah sudah bisa maju index 2? : " + canProceed);
                        break;

                    case 3: // Tutorial keempat
                        patternTile = 3;
                        Debug.Log("Pattern di case 3 sekarang bernilai : " + patternTile);
                        canProceed = _tutorialManager.fieldTutorial.IsSolved(patternTile);
                        _tutorialManager.prevButton.interactable = true;
                        _tutorialManager.nextButton.interactable = canProceed;
                        _tutorialManager.nextButton.onClick.AddListener(NextButtonClicked);
                        _tutorialManager.prevButton.onClick.AddListener(PrevButtonClicked);

                        Debug.Log("Tutorial keempat aktif, index 3.");
                        Debug.Log("Apakah sudah bisa maju index 3? : " + canProceed);
                        break;

                    case 4: // Tutorial kelima
                        patternTile = 4;
                        Debug.Log("Pattern di case 4 sekarang bernilai : " + patternTile);
                        canProceed = _tutorialManager.fieldTutorial.IsSolved(patternTile);
                        _tutorialManager.prevButton.interactable = true;
                        _tutorialManager.nextButton.interactable = canProceed;
                        _tutorialManager.nextButton.onClick.AddListener(NextButtonClicked);
                        _tutorialManager.prevButton.onClick.AddListener(PrevButtonClicked);

                        Debug.Log("Tutorial kelima aktif, index 4.");
                        Debug.Log("Apakah sudah bisa majuindex 4? : " + canProceed);
                        break;

                    case 5: // Tutorial terakhir selesai
                        patternTile = 5;
                        Debug.Log("Pattern di case 5 sekarang bernilai : " + patternTile);
                        canProceed = _tutorialManager.fieldTutorial.IsSolved(patternTile);
                        _tutorialManager.prevButton.interactable = true;
                        _tutorialManager.nextButton.interactable = false; // Tidak bisa maju lebih jauh

                        _tutorialManager.nextButton.onClick.AddListener(NextButtonClicked);
                        _tutorialManager.prevButton.onClick.AddListener(PrevButtonClicked);

                        Debug.Log("Tutorial terakhir selesai, index 5.");
                        Debug.Log("Apakah sudah bisa maju index 5? : " + canProceed);
                        break;

                    default:
                        Debug.Log("Tutorial tidak ditemukan.");
                        break;
                }
            }
        }
        else
        {
            // Jika tidak ada tutorial yang selesai
            _currentIndexTutorial = 0;
            patternTile = 0;
            Debug.Log("Pattern belum di kerjakan sekarang bernilai : " + patternTile);
            _tutorialManager.prevButton.interactable = false;
            _tutorialManager.nextButton.interactable = false;
            MovePanel(positionsBawah[0]);
            Debug.Log("Tutorial pertama baru mulai, index 0.");
        }
    }

    // private void UpdateTutorial()
    // {
    //     List<int> solvedConnectionIds = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     if (solvedConnectionIds.Count > 0)
    //     {
    //         foreach (int connectionId in solvedConnectionIds)
    //         {
    //             // Validasi apakah tutorial sebelumnya sudah diselesaikan
    //             bool canProceed =
    //                 connectionId == 1 || solvedConnectionIds.Contains(connectionId - 1);

    //             if (canProceed)
    //             {
    //                 switch (connectionId)
    //                 {
    //                     case 1: // Tutorial kedua
    //                         _currentIndexTutorial = 1;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial kedua aktif, index 1.");
    //                         break;

    //                     case 2: // Tutorial ketiga
    //                         _currentIndexTutorial = 2;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial ketiga aktif, index 2.");
    //                         break;

    //                     case 3: // Tutorial keempat
    //                         _currentIndexTutorial = 3;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial keempat aktif, index 3.");
    //                         break;

    //                     case 4: // Tutorial kelima
    //                         _currentIndexTutorial = 4;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial kelima aktif, index 4.");
    //                         break;

    //                     case 5: // Tutorial terakhir selesai
    //                         _currentIndexTutorial = 5;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial terakhir selesai, index 5.");
    //                         break;

    //                     default:
    //                         Debug.LogWarning("Tutorial tidak ditemukan.");
    //                         break;
    //                 }
    //             }
    //             else
    //             {
    //                 Debug.LogWarning(
    //                     $"Tidak dapat melanjutkan ke tutorial {connectionId}, tutorial sebelumnya belum selesai."
    //                 );
    //             }
    //         }
    //     }
    //     else
    //     {
    //         // Jika tidak ada solved connections, tetap di tutorial pertama
    //         _currentIndexTutorial = 0;
    //         _tutorialManager.prevButton.interactable = false;
    //         _tutorialManager.nextButton.interactable = false;
    //         MovePanel(positionsBawah[0]);
    //         Debug.Log("Tutorial pertama baru mulai, index 0.");
    //     }
    // }

    // private void UpdateTutorial()
    // {
    //     List<int> solvedConnectionIds = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     if (solvedConnectionIds.Count > 0)
    //     {
    //         foreach (int connectionId in solvedConnectionIds)
    //         {
    //             // Validasi apakah tutorial sebelumnya sudah diselesaikan
    //             bool canProceed =
    //                 connectionId == 1 || solvedConnectionIds.Contains(connectionId - 1);

    //             if (canProceed)
    //             {
    //                 switch (connectionId)
    //                 {
    //                     case 1: // Tutorial kedua
    //                         _currentIndexTutorial = 1;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial kedua aktif, index 1.");
    //                         break;

    //                     case 2: // Tutorial ketiga
    //                         _currentIndexTutorial = 2;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial ketiga aktif, index 2.");
    //                         break;

    //                     case 3: // Tutorial keempat
    //                         _currentIndexTutorial = 3;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial keempat aktif, index 3.");
    //                         break;

    //                     case 4: // Tutorial kelima
    //                         _currentIndexTutorial = 4;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial kelima aktif, index 4.");
    //                         break;

    //                     case 5: // Tutorial terakhir selesai
    //                         _currentIndexTutorial = 5;
    //                         UpdateButtonStates();
    //                         MovePanel(positionsBawah[_currentIndexTutorial]);
    //                         Debug.Log("Tutorial terakhir selesai, index 5.");
    //                         break;

    //                     default:
    //                         Debug.LogWarning("Tutorial tidak ditemukan.");
    //                         break;
    //                 }
    //             }
    //             else
    //             {
    //                 Debug.LogWarning(
    //                     $"Tidak dapat melanjutkan ke tutorial {connectionId}, tutorial sebelumnya belum selesai."
    //                 );
    //             }
    //         }
    //     }
    //     else
    //     {
    //         // Jika tidak ada solved connections, tetap di tutorial pertama
    //         _currentIndexTutorial = 0;
    //         _tutorialManager.prevButton.interactable = false;
    //         _tutorialManager.nextButton.interactable = false;
    //         MovePanel(positionsBawah[0]);
    //         Debug.Log("Tutorial pertama baru mulai, index 0.");
    //     }
    // }
    // private void UpdateTutorial()
    // {
    //     List<int> solvedConnectionIds = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     if (solvedConnectionIds.Count > 0)
    //     {
    //         foreach (int connectionId in solvedConnectionIds)
    //         {
    //             if (connectionId <= _currentIndexTutorial + 1)
    //             {
    //                 _currentIndexTutorial = connectionId;

    //                 // Aktifkan atau matikan tombol berdasarkan progress
    //                 UpdateButtonStates();

    //                 // Pindahkan panel sesuai index
    //                 MovePanel(positionsBawah[_currentIndexTutorial]);

    //                 Debug.Log($"Tutorial {connectionId} aktif, index {_currentIndexTutorial}.");
    //             }
    //         }
    //     }
    //     else
    //     {
    //         // Jika belum ada tantangan yang diselesaikan, mulai dari index 0
    //         _currentIndexTutorial = 0;

    //         UpdateButtonStates();
    //         MovePanel(positionsBawah[0]);
    //         Debug.Log("Tutorial pertama baru mulai, index 0.");
    //     }
    // }

    public void PrevButtonClicked()
    {
        if (_currentIndexTutorial > 0)
        {
            _currentIndexTutorial--;

            // Update tombol sesuai dengan kondisi terbaru
            MovePanel(positionsBawah[_currentIndexTutorial]);

            // Pindahkan panel ke posisi tutorial sebelumnya


            Debug.Log($"Kembali ke tutorial index {_currentIndexTutorial}.");
        }
        else
        {
            Debug.LogWarning("Sudah di tutorial pertama, tidak bisa mundur lagi.");
        }
    }

    public void NextButtonClicked()
    {
        if (_currentIndexTutorial < positionsBawah.Length - 1)
        {
            _currentIndexTutorial++;

            // Update tombol sesuai dengan kondisi terbaru
            MovePanel(positionsBawah[_currentIndexTutorial]);

            // Pindahkan panel ke posisi tutorial berikutnya


            Debug.Log($"Lanjut ke tutorial index {_currentIndexTutorial}.");
        }
        else
        {
            Debug.LogWarning("Sudah di tutorial terakhir, tidak bisa maju lagi.");
        }
    }

    // public void PrevButtonClicked()
    // {
    //     if (_currentIndexTutorial > 0)
    //     {
    //         _currentIndexTutorial--;
    //         _tutorialManager.prevButton.interactable = _currentIndexTutorial > 0;
    //         _tutorialManager.nextButton.interactable =
    //             _currentIndexTutorial < positionsBawah.Length;
    //         MovePanel(positionsBawah[_currentIndexTutorial]);
    //         Debug.Log($"Kembali ke tutorial index {_currentIndexTutorial}.");
    //     }
    // }

    // public void NextButtonClicked()
    // {
    //     // Dapatkan daftar tutorial yang sudah selesai
    //     List<int> solvedConnectionIds = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     // Validasi apakah tutorial sebelumnya selesai
    //     if (
    //         _currentIndexTutorial < positionsBawah.Length - 1
    //         && solvedConnectionIds.Contains(_currentIndexTutorial)
    //     )
    //     {
    //         _currentIndexTutorial++;
    //         _tutorialManager.prevButton.interactable = _currentIndexTutorial > 0;
    //         _tutorialManager.nextButton.interactable =
    //             _currentIndexTutorial < positionsBawah.Length;
    //         MovePanel(positionsBawah[_currentIndexTutorial]);
    //         Debug.Log($"Lanjut ke tutorial index {_currentIndexTutorial}.");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Tidak dapat melanjutkan, tutorial sebelumnya belum selesai.");
    //     }
    // }
    // private void UpdateButtonStates()
    // {
    //     // Tombol Prev hanya aktif jika bukan di index 0
    //     _tutorialManager.prevButton.interactable = _currentIndexTutorial > 0;

    //     // Periksa apakah tantangan yang sesuai sudah selesai
    //     bool canProceed = false;
    //     List<int> solvedConnections = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     // Jika tantangan saat ini sudah selesai (angka currentIndexTutorial+1 ada di GetSolvedConnections)
    //     if (solvedConnections.Contains(_currentIndexTutorial + 1))
    //     {
    //         canProceed = true;
    //     }

    //     // Tombol Next hanya aktif jika tantangan saat ini sudah selesai
    //     _tutorialManager.nextButton.interactable = canProceed;

    //     // Debug log untuk memeriksa status
    //     Debug.Log($"Posisi pengguna saat ini: {_currentIndexTutorial + 1}");
    //     Debug.Log($"Apakah sudah bisa maju ke tantangan selanjutnya? {canProceed}");

    //     // Menyediakan jejak visual untuk posisi saat ini (misalnya update UI atau teks status)
    //     // _tutorialManager.UpdateTutorialProgress(_currentIndexTutorial + 1);
    // }
    // private void UpdateButtonStates()
    // {
    //     // Tombol Prev hanya aktif jika bukan di index 0
    //     _tutorialManager.prevButton.interactable = _currentIndexTutorial > 0;

    //     // Mendapatkan daftar tantangan yang sudah diselesaikan
    //     var solvedConnections = _tutorialManager.fieldTutorial.GetSolvedConnections();

    //     // Memastikan bahwa tantangan saat ini sudah selesai
    //     bool canProceed = solvedConnections.Contains(_currentIndexTutorial);

    //     // Mengatur tombol Next berdasarkan status tantangan
    //     if (solvedConnections.Count == 0)
    //     {
    //         canProceed = false; // Jika tidak ada tantangan yang diselesaikan, tidak bisa maju
    //     }
    //     else
    //     {
    //         // Memastikan pengguna hanya bisa maju jika tantangan sebelumnya sudah diselesaikan
    //         canProceed = solvedConnections.Count >= _currentIndexTutorial;
    //     }

    //     _tutorialManager.nextButton.interactable = canProceed;

    //     // Debug log untuk memeriksa status
    //     Debug.Log(
    //         $"Posisi pengguna saat ini: {_currentIndexTutorial}. "
    //             + $"Apakah sudah bisa maju ke tantangan selanjutnya (index {_currentIndexTutorial + 1})? : {canProceed}"
    //     );
    // }
    // private void UpdateButtonStates()
    // {
    //     bool isTantanganSelesai = _tutorialManager.fieldTutorial.IsConnectionSolved(
    //         _currentIndexTutorial
    //     );
    //     bool isTantanganBelumHabis = _currentIndexTutorial < 5;
    //     bool canProceed = isTantanganSelesai && isTantanganBelumHabis;

    //     _tutorialManager.prevButton.interactable = _currentIndexTutorial > 0;
    //     _tutorialManager.nextButton.interactable = canProceed;

    //     Debug.Log($"Apakah sudah bisa maju index {_currentIndexTutorial}? : {canProceed}");
    // }

    // public void PrevButtonClicked()
    // {
    //     if (_currentIndexTutorial > 0)
    //     {
    //         _currentIndexTutorial--;

    //         UpdateButtonStates();
    //         MovePanel(positionsBawah[_currentIndexTutorial]);

    //         Debug.Log($"Kembali ke tutorial index {_currentIndexTutorial}.");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Sudah di tutorial pertama, tidak bisa mundur lagi.");
    //     }
    // }

    // public void NextButtonClicked()
    // {
    //     if (_currentIndexTutorial < positionsBawah.Length - 1)
    //     {
    //         _currentIndexTutorial++;

    //         UpdateButtonStates();
    //         MovePanel(positionsBawah[_currentIndexTutorial]);

    //         Debug.Log($"Lanjut ke tutorial index {_currentIndexTutorial}.");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Sudah di tutorial terakhir, tidak bisa maju lagi.");
    //     }
    // }

    private void MovePanel(Vector2 positions)
    {
        tutupBawah.SetActive(true);
        Debug.Log("Move Panel to: " + positions);
        penutupBawah.anchoredPosition = positions;
    }

    private void ResetPositions()
    {
        Debug.Log("Mereset posisi bawah ke kondisi awal.");
        MovePanel(positionsBawah[0]);
    }
}
