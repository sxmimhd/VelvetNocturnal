using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void OpenDialogue()
    {
        dialoguePanel.SetActive(true);
    }
    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenDialogue();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CloseDialogue();
        }
    }
}