using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    [SerializeField] private GameObject ballPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnBalls(int ballCount, float initialSpeed, float speedIncrease, float maxSpeed)
    {
        for (int i = 0; i < ballCount; i++)
        {
            Ball ball = Instantiate(ballPrefab, transform).GetComponent<Ball>();
            ball.Initialize(initialSpeed, speedIncrease, maxSpeed);
        }
    }

    public void DestroyBalls()
    {
        foreach (Transform ball in transform)
        {
            Destroy(ball.gameObject);
        }
    }

}
