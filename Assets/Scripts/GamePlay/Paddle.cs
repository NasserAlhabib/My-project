using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;

    private Vector3 originalScale;

    private Rigidbody2D rb;
    private float moveVertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        originalScale = transform.localScale;

        moveSpeed = PlayerStats.Instance.CurrentMoveSpeed;
        float paddleHighet = PlayerStats.Instance.CurrentPaddleHighet;
        // set the paddle height based on the current stats
        transform.localScale = new Vector3(originalScale.x, paddleHighet, originalScale.z);
    }

    private void Update()
    {
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.linearVelocityY = moveVertical * moveSpeed;
    }
}
