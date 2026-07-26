using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    private bool sceneLoaded;
    private float currentFadeDuration;
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
        currentFadeDuration = fadeDuration;

        sceneLoaded = false;

        SceneManager.sceneLoaded += OnSceneLoaded;

        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        FadeManager.Instance.SetFadeDuration(currentFadeDuration);

        yield return FadeManager.Instance.FadeOut();

        FadeManager.Instance.SetBlack();

        SceneManager.LoadScene(sceneName);

        yield return new WaitUntil(() => sceneLoaded);

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.2f);

        yield return FadeManager.Instance.FadeIn();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        SpawnPlayer();

        UpdateCameraBounds();

        sceneLoaded = true;
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