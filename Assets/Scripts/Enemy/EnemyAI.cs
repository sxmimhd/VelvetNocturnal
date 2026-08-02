using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    private EnemyDoor destinationDoor;
    private bool walkingToDoor;

    private Rigidbody2D rb;

    private Transform playerTarget;

    private int patrolIndex;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
            return;
        if (walkingToDoor)
        {
            WalkToDoor();
            return;
        }

        switch (EnemyManager.Instance.State)
        {
            case EnemyManager.EnemyState.Idle:
                break;

            case EnemyManager.EnemyState.Patrol:
                Patrol();
                break;

            case EnemyManager.EnemyState.Chase:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        if (EnemyRoom.Current == null)
            return;

        if (EnemyRoom.Current.PatrolPoints.Count == 0)
            return;
        
        Transform point =
            EnemyRoom.Current.PatrolPoints[patrolIndex].transform;

        rb.MovePosition(
            Vector2.MoveTowards(
                rb.position,
                point.position,
                moveSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, point.position) < .1f)
        {
            patrolIndex++;

            if (patrolIndex >= EnemyRoom.Current.PatrolPoints.Count)
            {
                patrolIndex = 0;

                EnemyManager.Instance.PatrolFinished();
            }
        }
    }

    void Chase()
    {
        // Player still in this scene
        if (playerTarget != null)
        {
            Vector2 direction =
                ((Vector2)playerTarget.position - rb.position).normalized;

            rb.MovePosition(
                rb.position +
                direction * moveSpeed * Time.fixedDeltaTime);

            return;
        }

        // Player changed scene
        if (!walkingToDoor)
        {
            EnemyDoor[] doors = FindObjectsByType<EnemyDoor>();

            foreach (EnemyDoor door in doors)
            {
                if (door.TargetScene == EnemyManager.Instance.ChaseScene &&
                    door.TargetSpawnID == EnemyManager.Instance.ChaseSpawnID)
                {
                    GoToDoor(door);
                    return;
                }
            }

            playerTarget = null;
            EnemyManager.Instance.StopChase();
        }
    }

    public void SetPlayerTarget(Transform player)
    {
        playerTarget = player;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        SetPlayerTarget(player.transform);

        if (EnemyManager.Instance.State == EnemyManager.EnemyState.Idle)
            EnemyManager.Instance.BeginPatrol();

        EnemyManager.Instance.BeginChase();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerMovement>())
            return;

        playerTarget = null;

        EnemyManager.Instance.StopChase();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMovement>())
        {
            Debug.Log("GAME OVER");
            GameOverManager.Instance.TriggerGameOver();
        }
    }

    public void SetPatrolIndex(int index)
    {
        patrolIndex = index;
    }
    public void GoToDoor(EnemyDoor door)
    {
        destinationDoor = door;
        walkingToDoor = true;
    }
    private void WalkToDoor()
    {
        if (destinationDoor == null)
        {
            walkingToDoor = false;
            return;
        }
        rb.MovePosition(
            Vector2.MoveTowards(
                rb.position,
                destinationDoor.Position,
                moveSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(
            rb.position,
            destinationDoor.Position) < .1f)
        {
            EnemyManager.Instance.TravelTo(
                destinationDoor.TargetScene,
                destinationDoor.TargetSpawnID);
            EnemyManager.Instance.SetChaseDoor(null);
            gameObject.SetActive(false);

            walkingToDoor = false;
        }
    }
}