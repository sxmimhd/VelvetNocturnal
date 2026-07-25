using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private InteractionType interactionType;

    [SerializeField] private string promptText = "Interact";

    [SerializeField] private bool autoInteract;

    [SerializeField] private bool oneTimeOnly;

    private bool hasInteracted;

    [Header("Dialogue")]
    [SerializeField] private Dialogue dialogue;

    [Header("Scene")]
    [SerializeField] private string sceneName;
    [SerializeField] private string spawnID = "Default";

    public bool AutoInteract => autoInteract;
    public void Interact()
    {
        if (oneTimeOnly && hasInteracted)
            return;

        hasInteracted = true;

        switch (interactionType)
        {
            case InteractionType.Dialogue:
                DialogueManager.Instance.StartDialogue(dialogue);
                break;

            case InteractionType.Scene:

                Debug.Log(SceneTransitionManager.Instance == null
                    ? "SceneTransitionManager IS NULL"
                    : "SceneTransitionManager EXISTS");
                Debug.Log("Scene Name: " + sceneName);
                Debug.Log("Spawn ID: " + spawnID);

                SceneTransitionManager.Instance.LoadScene(sceneName, spawnID);

                break;

            case InteractionType.Pickup:
                Debug.Log("Pickup");
                break;

            case InteractionType.Inspect:
                Debug.Log("Inspect");
                break;

            case InteractionType.Custom:
                Debug.Log("Custom");
                break;
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