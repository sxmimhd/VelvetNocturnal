using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool IsDialogueActive => isDialogueActive;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;
    private Queue<string> dialogueLines = new Queue<string>();
    private Dialogue currentDialogue;
    private bool isDialogueActive = false;

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
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);

        speakerNameText.text = dialogue.speakerName;
        portraitImage.sprite = dialogue.portrait;

        dialogueLines.Clear();

        foreach (string line in dialogue.lines)
        {
            dialogueLines.Enqueue(line);
        }

        DisplayNextLine();
    }
    public void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueLines.Dequeue();
    }
    private void EndDialogue()
    {
        isDialogueActive = false;

        dialoguePanel.SetActive(false);

        currentDialogue = null;
    }
    private void Update()
    {
        if (!isDialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }
}