using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public bool IsDialogueActive => isDialogueActive;

    // Fired whenever a dialogue completely finishes.
    public event Action OnDialogueFinished;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource voiceSource;

    private readonly Queue<string> dialogueLines = new();

    private Dialogue currentDialogue;
    private bool isDialogueActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
            return;

        currentDialogue = dialogue;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);

        speakerNameText.text = dialogue.speakerName;
        portraitImage.sprite = dialogue.portrait;

        dialogueLines.Clear();

        foreach (string line in dialogue.lines)
            dialogueLines.Enqueue(line);

        currentLineIndex = 0;
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

        if (currentDialogue.voices != null &&
            currentLineIndex < currentDialogue.voices.Length &&
            currentDialogue.voices[currentLineIndex] != null)
        {
            voiceSource.Stop();
            voiceSource.PlayOneShot(currentDialogue.voices[currentLineIndex]);
        }

        currentLineIndex++;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;

        dialoguePanel.SetActive(false);

        currentDialogue = null;
        voiceSource.Stop();
        // Notify whoever is waiting.
        OnDialogueFinished?.Invoke();
    }

    private void Update()
    {
        if (!isDialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            DisplayNextLine();
    }
    private int currentLineIndex;
}