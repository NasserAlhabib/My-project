using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Base Values (tweak in inspector)")]
    [SerializeField] private float baseMoveSpeed = 7f;
    [SerializeField] private float basePaddleHighet = 1f;

    // Runtime values
    public float CurrentMoveSpeed { get; private set; }
    public float CurrentPaddleHighet { get; private set; }

    // Remember every picked upgrade
    private readonly List<UpgradeDataSO> appliedUpgrades = new List<UpgradeDataSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // initialize from base
        ResetStats();
    }

    /// <summary>
    /// Call this whenever the player selects an upgrade.
    /// </summary>
    public void AddUpgrade(UpgradeDataSO upgrade)
    {
        appliedUpgrades.Add(upgrade);
        // immediately update the stored stats
        ApplySingleUpgrade(upgrade);
    }

    private void ResetStats()
    {
        CurrentMoveSpeed = baseMoveSpeed;
        CurrentPaddleHighet = basePaddleHighet;

        // re‐apply all past upgrades (if any)
        foreach (UpgradeDataSO upgrade in appliedUpgrades)
            ApplySingleUpgrade(upgrade);
    }

    private void ApplySingleUpgrade(UpgradeDataSO upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.PaddleExtension:
                CurrentPaddleHighet += upgrade.value;
                break;
            case UpgradeType.PaddleSpeed:
                CurrentMoveSpeed += upgrade.value;
                break;
        }
    }

    public void ResetAll()
    {
        appliedUpgrades.Clear();
        CurrentMoveSpeed = baseMoveSpeed;
        CurrentPaddleHighet = basePaddleHighet;
        SceneManager.sceneLoaded += OnSceneLoadedOnce;
    }


    private void OnSceneLoadedOnce(Scene scene, LoadSceneMode mode)
    {
        // after the new room scene loads, reapply defaults
        ResetStats();
        SceneManager.sceneLoaded -= OnSceneLoadedOnce;
    }


    // After any new scene loads, re‐apply so Paddle can pick it up in Start()
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetStats();
    }
}
