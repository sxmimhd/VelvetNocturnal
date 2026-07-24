using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractionTrigger currentTrigger;

    private void Update()
    {
        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
        {
            return;
        }
        if (currentTrigger == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTrigger.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger != null)
        {
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger != null && trigger == currentTrigger)
        {
            trigger.HidePrompt();
            currentTrigger = null;
        }
    }
}