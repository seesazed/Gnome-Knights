using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Sword Settings")]
    public int baseDamage = 20; // Default sword damage
    public int currentDamage; // Damage that can change with upgrades
    public bool canDamage = true; // Flag to enable/disable damage

    private float nextAttackTime = 0f;
    public float attackCooldown = 1f; // Cooldown time for attacks (in seconds)

    public PlayerStats playerStats;

    void Start()
    {
        // Initialize current damage to base damage at the start
        currentDamage = playerStats.attackDamage; // Set damage based on PlayerStats
    }

    // Detect collisions with zombies
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && canDamage && Time.time >= nextAttackTime)
        {
            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(currentDamage);
                Debug.Log("🗡️ Dealt " + currentDamage + " damage to zombie");

                // Handle attack cooldown
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
