using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private string spawnID;

    public string SpawnID => spawnID;
}