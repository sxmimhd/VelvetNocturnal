using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnID;

    public string TargetScene => targetScene;
    public string TargetSpawnID => targetSpawnID;

    public Vector3 Position => transform.position;
}