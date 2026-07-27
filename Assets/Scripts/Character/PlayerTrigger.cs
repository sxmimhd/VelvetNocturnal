using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool CanInteract { get; set; }
    private InteractionTrigger currentTrigger;

    private void Update()
    {
        if (!CanInteract)
            return;
        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
        {
            return;
        }
        if (currentTrigger == null)
            return;
        if (currentTrigger.IsInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTrigger.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanInteract)
            return;

        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger == null)
            return;

        currentTrigger = trigger;

        if (trigger.AutoInteract)
        {
            trigger.Interact();
        }
        else
        {
            trigger.ShowPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!CanInteract)
            return;

        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger != null && trigger == currentTrigger)
        {
            trigger.HidePrompt();
            currentTrigger = null;
        }
    }
}