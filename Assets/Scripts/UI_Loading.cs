using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
    [SerializeField] private GameEventNoParam _onLoadingDone;
    [SerializeField] private Timer _timer;
    public Image loadingBarImage;  
    public float fillDuration = 5f; 
    private float targetFill = 1f; 

    private void Start()
    {
        loadingBarImage.fillAmount = 0f;
        StartCoroutine(FillBarAfterDelay(0.1f));
    }

    private IEnumerator FillBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            loadingBarImage.fillAmount = Mathf.Lerp(0f, targetFill, elapsedTime / fillDuration);
            yield return null;
        }

        loadingBarImage.fillAmount = targetFill;
        _onLoadingDone.Raise();
        _timer.OnResume();
    }
}
