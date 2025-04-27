using System;
using UnityEngine;

public class PongManager : MonoBehaviour
{
    public static PongManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;
    public event Action<int, int> OnHealthChanged;

    [SerializeField] private int maxHealth = 3;

    private GameState gameState;
    private RoomDataSO currentRoomData;
    private int currentHealth;

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

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnGameStateChanged?.Invoke(gameState);
    }

    public void GainExtraLife(int amount)
    {
        currentHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage()
    {
        currentHealth--;
        currentHealth = Mathf.Max(0, currentHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            EndGame();
    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;

        if (OnGameStateChanged == null) return;

        // Copy the invocation list so we can modify the event safely
        var invocationList = OnGameStateChanged.GetInvocationList();
        foreach (var d in invocationList)
        {
            var handler = (Action<GameState>)d;
            try
            {
                handler(newGameState);
            }
            catch (MissingReferenceException)
            {
                // Subscriber was destroyed—remove it
                OnGameStateChanged -= handler;
            }
        }
    }

    public void SetCurrentRoomData(RoomDataSO roomData)
    {
        currentRoomData = roomData;
    }
    public RoomDataSO GetCurrentRoomData()
    {
        return currentRoomData;
    }

    public void StartRoom(RoomDataSO roomData)
    {
        BallManager.Instance.SpawnBalls(roomData.ballCount,
         roomData.ballSpeed,
         roomData.ballSpeedIncrease,
         roomData.maxBallSpeed);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void EndRoom()
    {
        BallManager.Instance.DestroyBalls();
        SetGameState(GameState.Upgrading);
    }


    private void EndGame()
    {
        // destroy any balls left
        BallManager.Instance.DestroyBalls();
        SetGameState(GameState.GameOver);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

    }

}
