using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public bool canDamage = false; // Set by animation events
    private float nextAttackTime = 0f;

    public PlayerStats playerStats;

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponentInParent<PlayerStats>();
            if (playerStats == null)
                Debug.LogError("PlayerStats reference is missing!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && canDamage && Time.time >= nextAttackTime)
        {
            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(playerStats.attackDamage);
                Debug.Log("🗡️ Dealt " + playerStats.attackDamage + " damage to zombie");

                nextAttackTime = Time.time + playerStats.attackCooldown;
            }
        }
    }

    // These should be triggered by animation events
    public void EnableDamage()
    {
        canDamage = true;
        Debug.Log("🗡️ Damage Enabled");
    }

    public void DisableDamage()
    {
        canDamage = false;
        Debug.Log("❌ Damage Disabled");
    }
}
