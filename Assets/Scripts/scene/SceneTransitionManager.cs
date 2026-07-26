using System.Collections;
using Unity.Cinemachine;
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

    public void LoadScene(string sceneName, string spawnID, float fadeDuration)
    {
        nextSpawnID = spawnID;

        StartCoroutine(LoadSceneRoutine(sceneName, fadeDuration));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, float fadeDuration)
    {
        FadeManager.Instance.SetFadeDuration(fadeDuration);

        yield return FadeManager.Instance.FadeOut();

        SceneManager.LoadScene(sceneName);

        yield return null;

        SpawnPlayer();

        UpdateCameraBounds();

        yield return FadeManager.Instance.FadeIn();
    }

    private void SpawnPlayer()
    {
        SceneSpawn[] spawns = FindObjectsByType<SceneSpawn>();

        foreach (SceneSpawn spawn in spawns)
        {
            if (spawn.SpawnID == nextSpawnID)
            {
                PlayerMovement.Instance.transform.position = spawn.transform.position;
                return;
            }
        }

        Debug.LogWarning($"Spawn '{nextSpawnID}' not found.");
    }

    private void UpdateCameraBounds()
    {
        CameraBounds bounds = FindAnyObjectByType<CameraBounds>();

        if (bounds == null)
            return;

        CinemachineConfiner2D confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        if (confiner == null)
            return;

        confiner.BoundingShape2D = bounds.GetComponent<Collider2D>();
        confiner.InvalidateBoundingShapeCache();
    }
}