using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private Dialogue[] dialogues;

    [Header("Ending Image")]
    [SerializeField] private CanvasGroup endingImage;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float holdDuration = 3f;

    [Header("Next Scene")]
    [SerializeField] private string menuScene = "Menu";

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.GetComponent<PlayerMovement>())
            return;

        triggered = true;

        StartCoroutine(EndingRoutine());
    }

    private IEnumerator EndingRoutine()
    {
        
        if (dialogues.Length > 0)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                bool finished = false;

                void OnFinished()
                {
                    finished = true;
                }

                DialogueManager.Instance.OnDialogueFinished += OnFinished;

                DialogueManager.Instance.StartDialogue(dialogue);

                yield return new WaitUntil(() => finished);

                DialogueManager.Instance.OnDialogueFinished -= OnFinished;
            }
        }

        
        endingImage.gameObject.SetActive(true);
        endingImage.alpha = 0f;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            endingImage.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(holdDuration);

        SceneManager.LoadScene(menuScene);
    }
}