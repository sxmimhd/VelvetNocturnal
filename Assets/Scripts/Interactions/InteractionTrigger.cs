using System.Collections;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string promptText = "Interact";
    [SerializeField] private bool autoInteract;
    [SerializeField] private bool oneTimeOnly;
    [SerializeField] private string interactionID;

    private bool isInteracting;
    public bool IsInteracting => isInteracting;

    [Header("Dialogue")]
    [SerializeField] private bool useDialogue;
    [SerializeField] private Dialogue[] dialogues;

    [Header("Pickup")]
    [SerializeField] private bool usePickup;
    [SerializeField] private InventoryItem item;

    [Header("Scene Transition")]
    [SerializeField] private bool useSceneTransition;
    [SerializeField] private string sceneName;
    [SerializeField] private string spawnID;
    [SerializeField] private bool enemyCanUse = true;
    [SerializeField] private float fadeDuration = 1f;
    [Header("Scene Requirements")]
    [SerializeField] private bool requireItem;
    [SerializeField] private InventoryItem requiredItem;
    [SerializeField] private bool useMissingItemDialogue;
    [SerializeField] private Dialogue missingItemDialogue;
    private bool hasInteracted;
    public bool AutoInteract => autoInteract;
    private void Start()
    {
        if (oneTimeOnly &&
            !string.IsNullOrEmpty(interactionID) &&
            InteractionManager.Instance.IsCompleted(interactionID))
        {
            gameObject.SetActive(false);
        }
    }
    public void Interact()
    {
        if (oneTimeOnly &&
            !string.IsNullOrEmpty(interactionID) &&
            InteractionManager.Instance.IsCompleted(interactionID))
        {
            return;
        }
        if (isInteracting)
            return;

        if (oneTimeOnly && hasInteracted)
            return;

        hasInteracted = true;

        isInteracting = true;

        HidePrompt();

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        
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

    
        if (usePickup)
        {
            InventoryManager.Instance.AddItem(item);

            if (oneTimeOnly && !string.IsNullOrEmpty(interactionID))
                InteractionManager.Instance.Complete(interactionID);

            Destroy(gameObject);
        }

        if (useSceneTransition)
        {
            if (requireItem && !InventoryManager.Instance.HasItem(requiredItem))
            {
                if (useMissingItemDialogue && missingItemDialogue != null)
                {
                    bool finished = false;

                    void DialogueFinished()
                    {
                        finished = true;
                    }

                    DialogueManager.Instance.OnDialogueFinished += DialogueFinished;

                    DialogueManager.Instance.StartDialogue(missingItemDialogue);

                    yield return new WaitUntil(() => finished);

                    DialogueManager.Instance.OnDialogueFinished -= DialogueFinished;
                }
                hasInteracted = false;
                isInteracting = false;

                if (!oneTimeOnly)
                    ShowPrompt();

                yield break;
            }
            if (EnemyManager.Instance != null &&
                EnemyManager.Instance.State == EnemyManager.EnemyState.Chase)
            {
                if (enemyCanUse)
                {
                    EnemyDoor door = GetComponent<EnemyDoor>();

                    if (door != null)
                        EnemyManager.Instance.SetChaseDoor(door);
                }
                else
                {
                    EnemyManager.Instance.StopChase();
                }
            }
            SceneTransitionManager.Instance.LoadScene(
            sceneName,
            spawnID,
            fadeDuration);
        }
        if (!usePickup &&
            oneTimeOnly &&
            !string.IsNullOrEmpty(interactionID))
        {
            InteractionManager.Instance.Complete(interactionID);
        }

        isInteracting = false;
        
        if (!oneTimeOnly)
            ShowPrompt();
    }

    public void ShowPrompt()
    {
        if (autoInteract)
            return;

        if (isInteracting)
            return;

        UIManager.Instance.ShowPrompt(promptText);
    }

    public void HidePrompt()
    {
        UIManager.Instance.HidePrompt();
    }
}