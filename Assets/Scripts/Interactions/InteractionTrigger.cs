using System.Collections;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string promptText = "Interact";
    [SerializeField] private bool autoInteract;
    [SerializeField] private bool oneTimeOnly;

    private bool hasInteracted;

    [Header("Dialogue")]
    [SerializeField] private bool useDialogue;
    [SerializeField] private Dialogue[] dialogues;

    [Header("Pickup")]
    [SerializeField] private bool usePickup;
    [SerializeField] private string itemID;

    [Header("Scene Transition")]
    [SerializeField] private bool useSceneTransition;
    [SerializeField] private string sceneName;
    [SerializeField] private string spawnID;
    [SerializeField] private float fadeDuration = 1f;

    public bool AutoInteract => autoInteract;

    public void Interact()
    {
        if (oneTimeOnly && hasInteracted)
            return;

        hasInteracted = true;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        // -----------------------------
        // Dialogues
        // -----------------------------
        if (useDialogue && dialogues.Length > 0)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                bool finished = false;

                void DialogueFinished()
                {
                    finished = true;
                }

                DialogueManager.Instance.OnDialogueFinished += DialogueFinished;

                DialogueManager.Instance.StartDialogue(dialogue);

                yield return new WaitUntil(() => finished);

                DialogueManager.Instance.OnDialogueFinished -= DialogueFinished;
            }
        }

        // -----------------------------
        // Pickup (future)
        // -----------------------------
        if (usePickup)
        {
            Debug.Log("Picked up: " + itemID);

            // InventoryManager.Instance.Add(itemID);
        }

        // -----------------------------
        // Scene Transition
        // -----------------------------
        if (useSceneTransition)
        {
            SceneTransitionManager.Instance.LoadScene(
                sceneName,
                spawnID,
                fadeDuration);
        }
    }

    public void ShowPrompt()
    {
        if (autoInteract)
            return;

        UIManager.Instance.ShowPrompt(promptText);
    }

    public void HidePrompt()
    {
        UIManager.Instance.HidePrompt();
    }
}