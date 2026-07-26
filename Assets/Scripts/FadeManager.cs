using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private float fadeDuration = 1f;

    private bool isFading;

    public bool IsFading => isFading;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Always start fully visible
        fadeGroup.alpha = 0f;
    }

    public void SetFadeDuration(float duration)
    {
        fadeDuration = Mathf.Max(0.01f, duration);
    }

    public IEnumerator FadeOut()
    {
        if (isFading)
            yield break;

        isFading = true;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 1f;

        isFading = false;
    }

    public IEnumerator FadeIn()
    {
        if (isFading)
            yield break;

        isFading = true;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 0f;

        isFading = false;
    }

    public void SetBlack()
    {
        fadeGroup.alpha = 1f;
    }

    public void SetClear()
    {
        fadeGroup.alpha = 0f;
    }
}