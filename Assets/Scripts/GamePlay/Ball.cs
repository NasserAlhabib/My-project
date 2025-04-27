using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionEffectPrefab;

    private float ballSpeed;
    private float ballSpeedIncrease;
    private float maxBallSpeed;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(float initialSpeed, float speedIncrease, float maxSpeed)
    {
        ballSpeed = initialSpeed;
        ballSpeedIncrease = speedIncrease;
        maxBallSpeed = maxSpeed;

        Invoke(nameof(LaunchBall), 1.5f);
    }

    private void LaunchBall()
    {
        // Generate a direction within 25-65 degrees from the x-axis, then randomize the quadrant
        float baseAngle = Random.Range(25f, 65f);
        float quadrant = 90f * Random.Range(0, 4);
        float angle = baseAngle + quadrant;

        Vector2 direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        );

        rb.linearVelocity = direction * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();

        // 1) Spawn the effect at the first contact point
        ContactPoint2D contact = collision.GetContact(0);
        var ps = Instantiate(
            collisionEffectPrefab,
            contact.point,
            Quaternion.LookRotation(Vector3.forward, contact.normal)
        );

        rb.linearVelocity += rb.linearVelocity * ballSpeedIncrease;
        if (rb.linearVelocity.magnitude > maxBallSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxBallSpeed;
        }

        //add small random angle to the ball's direction
        float randomAngle = Random.Range(-5f, 5f);
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg + randomAngle;

        Vector2 newDirection = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        );

        rb.linearVelocity = newDirection * rb.linearVelocity.magnitude;
    }
}
