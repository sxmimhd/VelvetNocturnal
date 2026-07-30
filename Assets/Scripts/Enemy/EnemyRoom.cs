using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public static EnemyRoom Current { get; private set; }

    private readonly List<EnemyPatrolPoint> patrolPoints = new();
    private readonly List<EnemyDoor> doors = new();

    public IReadOnlyList<EnemyPatrolPoint> PatrolPoints => patrolPoints;
    public IReadOnlyList<EnemyDoor> Doors => doors;

    private void Awake()
    {
        Current = this;

        patrolPoints.Clear();
        doors.Clear();

        patrolPoints.AddRange(GetComponentsInChildren<EnemyPatrolPoint>());
        doors.AddRange(GetComponentsInChildren<EnemyDoor>());
    }

    public EnemyDoor GetRandomDoor()
    {
        if (doors.Count == 0)
            return null;

        return doors[Random.Range(0, doors.Count)];
    }
    private void OnDestroy()
    {
        if (Current == this)
            Current = null;
    }
}