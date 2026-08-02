using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string menuScene = "Menu";

    private bool paused;

    public bool IsPaused => paused;

    private void Awake()
    {
        Instance = this;

        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        paused = true;

        pausePanel.SetActive(true);

        CharacterManager.Instance.CurrentCharacter.enabled = false;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        paused = false;

        pausePanel.SetActive(false);

        CharacterManager.Instance.CurrentCharacter.enabled = true;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Game Resumed");
    }

    public void LoadLastSave()
    {
        Time.timeScale = 1f;

        ResumeGame();

        SaveManager.Instance.LoadGame();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(menuScene);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();
    }
}