using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimasiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Universal")]
    [SerializeField]
    float tweenDuration;

    [Header("Settings/Pause")]
    [SerializeField]
    public RectTransform PausePanelRect;

    [SerializeField]
    private CanvasGroup canvasGroupPause; //dark panel canvas group

    [SerializeField]
    public float topPosYPause = 1628.2f,
        middlePosYPause = 0;

    [Header("Menang")]
    public RectTransform WinPanelRect;
    public float middlePosXWin,
        topPosXWin = -1080f;

    [SerializeField]
    private CanvasGroup canvasGroupWin; //dark panel canvas group

    [Header("Kalah")]
    [SerializeField]
    private CanvasGroup defeatPanelCanvasGroup; // Untuk fade

    [SerializeField]
    private RectTransform defeatPanelRect; // Untuk skala dan posisi

    [SerializeField]
    private float fadeDuration = 0.3f; // Durasi animasi fade

    [SerializeField]
    private float scaleDuration = 0.3f; // Durasi animasi scale

    [SerializeField]
    private Vector2 offScreenPosition = new Vector2(0, -500); // Posisi awal panel

    [SerializeField]
    private Vector2 centerPosition = Vector2.zero; // Posisi akhir panel

    // [Header("Tutorial")]
    // [Header("Quiz")]
    //lose Panel
    public void StartLose()
    {
        defeatPanelCanvasGroup.alpha = 0f; // Transparan
        defeatPanelRect.anchoredPosition = offScreenPosition; // Posisi awal
        defeatPanelRect.localScale = Vector3.zero; // Skala nol (tidak terlihat)
    }

    public void LosePanelIntro()
    {
        defeatPanelCanvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutSine);

        // Pindahkan panel ke tengah dengan efek bounce
        defeatPanelRect.DOAnchorPos(centerPosition, scaleDuration).SetEase(Ease.OutBounce);

        // Perbesar panel dari skala nol ke normal
        defeatPanelRect.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }

    public void LosePanelOutro()
    {
        Debug.Log("LosePanelOutro triggered");
        // Fade-out
        defeatPanelCanvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InOutSine);

        // Pindahkan panel keluar layar
        defeatPanelRect.DOAnchorPos(offScreenPosition, fadeDuration).SetEase(Ease.InBack);

        // Kembalikan skala ke nol
        defeatPanelRect.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.InBack);
    }

    //win Panel

    public void WinPanelIntro()
    {
        // Fade-in untuk CanvasGroup
        canvasGroupWin.DOFade(1f, tweenDuration).SetUpdate(true);
        WinPanelRect.DOAnchorPos(new Vector2(middlePosXWin, 0f), tweenDuration).SetUpdate(true);
    }

    // setings panel

    public void PausePanelIntro()
    {
        canvasGroupPause.DOFade(1f, tweenDuration).SetUpdate(true);
        PausePanelRect.DOAnchorPosY(middlePosYPause, tweenDuration).SetUpdate(true);
    }

    public async Task PausePanelOutro()
    {
        canvasGroupPause.DOFade(0f, tweenDuration).SetUpdate(true);
        await PausePanelRect
            .DOAnchorPosY(topPosYPause, tweenDuration)
            .SetUpdate(true)
            .AsyncWaitForCompletion();
    }
}
