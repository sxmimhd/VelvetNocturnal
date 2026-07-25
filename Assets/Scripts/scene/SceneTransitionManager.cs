using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private string nextSpawnID;

    private void Awake()
    {
        Debug.Log("SceneTransitionManager Awake on " + gameObject.name);

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Instance assigned.");
        }
        else
        {
            Debug.Log("Duplicate destroyed.");
            Destroy(gameObject);
        }
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
        UpdateCameraBounds();
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
    private void UpdateCameraBounds()
    {
        Debug.Log("UpdateCameraBounds()");
        CameraBounds bounds = FindAnyObjectByType<CameraBounds>();

        if (bounds == null)
        {
            Debug.LogWarning("No CameraBounds found in this scene.");
            return;
        }

        CinemachineConfiner2D confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        if (confiner == null)
        {
            Debug.LogWarning("No CinemachineConfiner2D found.");
            return;
        }

        confiner.BoundingShape2D = bounds.GetComponent<Collider2D>();
        confiner.InvalidateBoundingShapeCache();
    }
    
}