using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    [Header("Sword Settings")]
    public bool canDamage = true;

    private float nextAttackTime = 0f;
    public float attackCooldown = 1f;

    // Reference to PlayerStats to pull updated damage
    public PlayerStats playerStats;

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponentInParent<PlayerStats>();
            if (playerStats == null)
                Debug.LogError("⚠️ PlayerStats not assigned in SwordDamage script!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && canDamage && Time.time >= nextAttackTime)
        {
            if (playerStats == null)
            {
                Debug.LogWarning("⚠️ playerStats is missing, cannot apply damage.");
                return;
            }

            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                int damage = playerStats.attackDamage;
                zombieHealth.TakeDamage(damage);
                Debug.Log("🗡️ Dealt " + damage + " damage to zombie");

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
