using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public bool CanMove { get; set; }

    private void Awake()
    {
        

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CanMove)
        {
            movement = Vector2.zero;
            return;
        }

        if (DialogueManager.Instance != null &&
            DialogueManager.Instance.IsDialogueActive)
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}