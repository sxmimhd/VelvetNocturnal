using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CompanionFollower : MonoBehaviour
{
    [SerializeField] private float followDistance = 2f;
    [SerializeField] private float followSpeed = 3f;

    private PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
            return;

        PlayerMovement leader = CharacterManager.Instance.CurrentCharacter;

        if (leader == movement)
            return;

        float distance = Vector2.Distance(transform.position, leader.transform.position);

        if (distance <= followDistance)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            leader.transform.position,
            followSpeed * Time.deltaTime);
    }
}