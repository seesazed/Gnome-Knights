using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [Header("Zombie Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    private float healthMultiplier = 1f; // Multiplier for health as waves increase

    void Start()
    {
        currentHealth = maxHealth;  // Initialize health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't drop below 0

        Debug.Log("Zombie health: " + currentHealth);

        // Optionally add logic to handle zombie death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Zombie has died!");

        // Add logic for zombie death here (e.g., destroy the zombie, play death animation, etc.)
        Destroy(gameObject); // Example of destroying the zombie when health reaches 0
    }

    // Method to scale the zombie's health based on the wave number
    public void SetWaveMultiplier(float multiplier)
    {
        healthMultiplier = multiplier;
        maxHealth = Mathf.RoundToInt(maxHealth * healthMultiplier);
        currentHealth = maxHealth;
        Debug.Log("Zombie health set to: " + currentHealth);
    }
}
