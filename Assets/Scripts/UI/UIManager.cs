using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowPrompt(string text)
    {
        Debug.Log(interactionPrompt);
        interactionPrompt.SetActive(true);
        promptText.text = $"{text}";
    }

    public void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }
}