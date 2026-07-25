using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private string nextSpawnID;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadScene(string sceneName, string spawnID)
    {
        nextSpawnID = spawnID;

        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        SceneSpawn[] spawns = FindObjectsByType<SceneSpawn>();

        Debug.Log($"Found {spawns.Length} SceneSpawn objects.");

        foreach (SceneSpawn spawn in spawns)
        {
            Debug.Log($"Spawn Object: {spawn.gameObject.name}");
            Debug.Log($"Spawn ID: {spawn.SpawnID}");

            if (spawn.SpawnID == nextSpawnID)
            {
                Debug.Log("Spawn Found!");

                PlayerMovement.Instance.transform.position = spawn.transform.position;
                return;
            }
        }

        Debug.LogWarning($"Spawn '{nextSpawnID}' not found.");
    }
}