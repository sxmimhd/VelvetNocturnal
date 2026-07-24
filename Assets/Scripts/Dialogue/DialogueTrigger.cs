using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private string promptText = "Interact";
    [SerializeField] private bool autoInteract = false;
    [SerializeField] private bool oneTimeOnly = false;
    private bool hasInteracted = false;
    public bool AutoInteract => autoInteract;
    public void Interact()
    {
        if (oneTimeOnly && hasInteracted)
            return;

        hasInteracted = true;

        DialogueManager.Instance.StartDialogue(dialogue);
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