using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 70f;

    [Header("Colisiones")]
    [Tooltip("Tag de los objetos que se destruyen al tocar al jugador. Si lo dejas vacío, se destruye cualquier objeto.")]
    [SerializeField] private string destroyTag = "Destructible";

    private Rigidbody2D rb;
    private float moveInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;                  // Sin gravedad
        rb.freezeRotation = true;              // No rota al chocar
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; // Solo se mueve en X
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        FlipSprite();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        float rate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;

        float newX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, rate * Time.fixedDeltaTime); // Unity 2022 o anterior: rb.velocity.x
        rb.linearVelocity = new Vector2(newX, 0f); // Unity 2022 o anterior: rb.velocity
    }

    private void FlipSprite()
    {
        if ((moveInput > 0f && !isFacingRight) || (moveInput < 0f && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    // Se llama si el otro objeto tiene un Collider2D normal (no Trigger)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyIfValid(collision.gameObject);
    }

    // Se llama si el otro objeto tiene un Collider2D marcado como "Is Trigger"
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyIfValid(other.gameObject);
    }

    private void DestroyIfValid(GameObject obj)
    {
        if (string.IsNullOrEmpty(destroyTag) || obj.CompareTag(destroyTag))
        {
            Destroy(obj);
        }
    }
}