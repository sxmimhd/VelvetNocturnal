using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SaveManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        if (SaveManager.Instance.SaveExists())
            SaveManager.Instance.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}