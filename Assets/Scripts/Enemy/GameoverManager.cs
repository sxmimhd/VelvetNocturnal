using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private CanvasGroup gameOverImage;
    [SerializeField] private float fadeTime = 1.5f;
    [SerializeField] private float holdTime = 2f;

    bool gameOver;

    private void Awake()
    {
        Instance = this;

        gameOverImage.alpha = 0f;
        gameOverImage.gameObject.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameOverImage.gameObject.SetActive(true);

        gameOverImage.blocksRaycasts = true;

        PlayerMovement player =
            CharacterManager.Instance.CurrentCharacter;

        player.enabled = false;

        EnemyManager.Instance.Enemy.enabled = false;

        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            gameOverImage.alpha = Mathf.Clamp01(t / fadeTime);

            yield return null;
        }

        yield return new WaitForSeconds(holdTime);
        gameOver = false;

        gameOverImage.alpha = 0f;
        gameOverImage.gameObject.SetActive(false);

        SaveManager.Instance.LoadGame();
    }
}