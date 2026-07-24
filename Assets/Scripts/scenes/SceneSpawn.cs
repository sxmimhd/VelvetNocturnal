using UnityEngine;

public class SceneSpawn : MonoBehaviour
{
    [SerializeField] private string spawnID = "Default";

    public string SpawnID => spawnID;
}