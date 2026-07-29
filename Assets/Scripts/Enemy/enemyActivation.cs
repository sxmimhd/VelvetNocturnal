using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] private string startScene;
    [SerializeField] private string startSpawnID;

    private bool activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered: " + other.name);

        if (activated)
            return;

        if (!other.GetComponent<PlayerMovement>())
            return;

        activated = true;

        Debug.Log("Enemy Activated!");

        EnemyManager.Instance.Activate(startScene, startSpawnID);
    }
}