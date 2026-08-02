using System.Collections;
using UnityEngine;

public class MenuNotice : MonoBehaviour
{
    [SerializeField] private CanvasGroup notice;
    [SerializeField] private float showTime = 2f;
    [SerializeField] private float fadeTime = 1.5f;

    private IEnumerator Start()
    {
        notice.alpha = 1f;
        notice.blocksRaycasts = true;

        yield return new WaitForSeconds(showTime);

        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            notice.alpha = 1f - (t / fadeTime);

            yield return null;
        }

        notice.alpha = 0f;
        notice.blocksRaycasts = false;
        notice.gameObject.SetActive(false);
    }
}