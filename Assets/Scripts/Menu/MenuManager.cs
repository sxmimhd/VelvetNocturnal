using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("cemetery");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}