using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    
    public enum EnemyState
    {
        Sleeping,
        Idle,
        Patrol,
        Chase
    }

    [SerializeField] private EnemyAI enemy;

    public EnemyAI Enemy => enemy;

    public EnemyState State { get; private set; } = EnemyState.Sleeping;

    public bool Activated => State != EnemyState.Sleeping;

    public string CurrentScene { get; private set; }

    public string CurrentSpawnID { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        enemy.gameObject.SetActive(false);
    }

    public void Activate(string sceneName, string spawnID)
    {
        if (Activated)
            return;

        CurrentScene = sceneName;
        CurrentSpawnID = spawnID;

        State = EnemyState.Idle;

        if (SceneManager.GetActiveScene().name == CurrentScene)
        {
            SceneSpawn[] spawns = FindObjectsByType<SceneSpawn>();

            foreach (SceneSpawn spawn in spawns)
            {
                if (spawn.SpawnID != CurrentSpawnID)
                    continue;

                enemy.transform.position = spawn.transform.position;

                enemy.gameObject.SetActive(true);

                

                return;
            }
        }
    }

    public void BeginPatrol()
    {
        if (State == EnemyState.Idle)
            State = EnemyState.Patrol;
    }

    public void BeginChase()
    {
        State = EnemyState.Chase;
    }

    public void StopChase()
    {
        State = EnemyState.Patrol;
    }

    public void TravelTo(string sceneName, string spawnID)
    {
        CurrentScene = sceneName;
        CurrentSpawnID = spawnID;
    }

    public bool IsEnemyInCurrentScene()
    {
        return Activated &&
               CurrentScene == SceneManager.GetActiveScene().name;
    }
    public void PatrolFinished()
    {
        EnemyDoor door = EnemyRoom.Current.GetRandomDoor();

        if (door == null)
            return;

        enemy.GoToDoor(door);
    }
    public void SpawnEnemy(Vector3 position)
    {
        enemy.transform.position = position;
    }

}