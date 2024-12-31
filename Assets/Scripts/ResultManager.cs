using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum StageType
{
    AND,
    OR,
    NOT,
    NAND,
    NOR,
    XOR,
    XNOR,
}

public enum TypeIC
{
    TTL,
    CMOS,
    HCMOS,
}

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private List<GameObject> _stars = new();

    // [SerializeField]
    // private TextMeshProUGUI _timerWinUIText;
    [SerializeField]
    private BintangData bintangData;

    [SerializeField]
    private TextMeshProUGUI _cableLeftText;

    [SerializeField]
    private int _switchPatternToWin = 4;

    [SerializeField]
    private GameEventNoParam _onTurnOnLED;

    [SerializeField]
    private GameEventNoParam _onTurnOffLED;

    [SerializeField]
    private GameEventNoParam _onWin;

    [SerializeField]
    private StageType _currentStageType;

    [SerializeField]
    private TypeIC _typeIC;

    private bool _isHighxHighClear = false;
    private bool _isHighxLowClear = false;
    private bool _isLowxHighClear = false;
    private bool _isLowxLowClear = false;

    [Header("Penentuan Bintang")]
    [SerializeField]
    private int _Bintang2Detik = 150;

    [SerializeField]
    private int _Bintang1Detik = 100;

    public StageType CurrentStageType => _currentStageType;
    public TypeIC CurrentTypeIc => _typeIC;
    private bool[] _clearStates = new bool[4]; // Array untuk menyimpan status clear

    public void OnCheckCable(bool isSwitch1On, bool isSwitch2On)
    {
        switch (_currentStageType)
        {
            case StageType.AND:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;

            case StageType.OR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;

            case StageType.NOT:
                Debug.Log("Check not");
                if (isSwitch1On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
            case StageType.NAND:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
            case StageType.NOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
            case StageType.XOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }
                break;
            case StageType.XNOR:
                if (isSwitch1On && isSwitch2On)
                {
                    if (!_isHighxHighClear)
                    {
                        _isHighxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }

                if (isSwitch1On && !isSwitch2On)
                {
                    if (!_isHighxLowClear)
                    {
                        _isHighxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && isSwitch2On)
                {
                    if (!_isLowxHighClear)
                    {
                        _isLowxHighClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOffLED.Raise();
                }

                if (!isSwitch1On && !isSwitch2On)
                {
                    if (!_isLowxLowClear)
                    {
                        _isLowxLowClear = true;
                        _switchPatternToWin--;
                        _cableLeftText.SetText("Tugas Utama : " + _switchPatternToWin.ToString());
                    }
                    _onTurnOnLED.Raise();
                }
                break;
        }
        if (_switchPatternToWin == 0)
            _onWin.Raise();
    }

    public void OnWin()
    {
        // Menampilkan semua bintang
        foreach (var star in _stars)
        {
            star.SetActive(true);
        }

        // Menyimpan nilai berdasarkan waktu yang tersisa
        int playerScore = CalculatePlayerScore();
        PlayerPrefs.SetInt($"PlayerNilai_{_currentStageType}_{_typeIC}", playerScore);
        Debug.Log(
            $"Nilai disimpan: {playerScore} untuk StageType: {_currentStageType}, TypeIC: {_typeIC}"
        );

        // Simpan nilai bintang ke ScriptableObject
        bintangData.nilaiBintang[(_currentStageType, _typeIC)] = playerScore;

        // Simpan data bintang ke PlayerPrefs
        bintangData.SaveData();

        // Log untuk memastikan nilai bintang disimpan
        Debug.Log(
            $"Nilai bintang disimpan: {playerScore} untuk StageType: {_currentStageType}, TypeIC: {_typeIC}"
        );
    }

    private int CalculatePlayerScore()
    {
        int playerScore = 3; // Nilai maksimum

        if (_timer.OnReturnTimeLeftValueAfterWin() <= _Bintang2Detik)
        {
            _stars[2].SetActive(false);
            playerScore = 2; // Nilai untuk 2 bintang
        }
        if (_timer.OnReturnTimeLeftValueAfterWin() <= _Bintang1Detik)
        {
            _stars[1].SetActive(false);
            playerScore = 1; // Nilai untuk 1 bintang
        }

        return playerScore;
    }
}
