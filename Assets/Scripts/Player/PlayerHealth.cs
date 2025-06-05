using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action<int> OnDamageTaken;
    public Action<int> OnUpgradeMaxHealth;
    public Action<int> OnHealCurrentHealth;

    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnDamageTaken?.Invoke(currentHealth);
        Debug.Log($"Current Health {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Player is Dead");
        }
    }

    public void UpgradeMaxHealth(int healthAmount)
    {
        maxHealth += healthAmount;
        OnUpgradeMaxHealth?.Invoke(maxHealth);
        Debug.Log($"Max Health {maxHealth}");
    }

    public void HealCurrentHealth(int heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        OnHealCurrentHealth?.Invoke(currentHealth);
        Debug.Log($"Current Health {currentHealth}");
    }
}
