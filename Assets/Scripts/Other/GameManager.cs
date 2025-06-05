using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
