using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCrystal : MonoBehaviour
{
    public void SaveGame()
    {
        SaveManager.Instance.SaveGame(
            SceneManager.GetActiveScene().name,
            SceneTransitionManager.Instance.CurrentSpawnID);

        Debug.Log("Game Saved!");
    }
}