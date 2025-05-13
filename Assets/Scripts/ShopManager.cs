using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerCurrency playerCurrency;
    public PlayerHealth playerHealth;
    public PlayerStats playerStats;

    [Header("Upgrade Costs")]
    public int maxHealthCost = 75;
    public int speedCost = 60;
    public int attackSpeedCost = 80;
    public int rangeCost = 70;
    public int damageCost = 90;

    [Header("Upgrade Amounts")]
    public int healthIncreaseAmount = 20;
    public float speedIncrease = 0.5f;
    public float attackSpeedIncrease = 0.1f;
    public float rangeIncrease = 1f;
    public int damageIncrease = 5;

    [Header("Shop UI")]
    public GameObject shopUI; // Assign in inspector
    public MonoBehaviour cameraLookScript; // Drag your camera look script here (e.g., BasicFPCC's look script)

    private bool isShopOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Replace with your shop key
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        shopUI.SetActive(isShopOpen);

        if (cameraLookScript != null)
            cameraLookScript.enabled = !isShopOpen;

        Cursor.lockState = isShopOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isShopOpen;
    }

    // Button click handlers

    public void BuyMaxHealth()
    {
        if (playerCurrency.SpendMoney(maxHealthCost))
        {
            playerHealth.maxHealth += healthIncreaseAmount;
            playerHealth.currentHealth += healthIncreaseAmount;
            Debug.Log("Max health increased!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade max health.");
        }
    }

    public void BuySpeed()
    {
        if (playerCurrency.SpendMoney(speedCost))
        {
            playerStats.moveSpeed += speedIncrease;
            Debug.Log("Speed increased!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade speed.");
        }
    }

    public void BuyAttackSpeed()
    {
        if (playerCurrency.SpendMoney(attackSpeedCost))
        {
            playerStats.attackCooldown -= attackSpeedIncrease;
            playerStats.attackCooldown = Mathf.Max(0.1f, playerStats.attackCooldown);
            Debug.Log("Attack speed increased!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade attack speed.");
        }
    }

    public void BuyRange()
    {
        if (playerCurrency.SpendMoney(rangeCost))
        {
            playerStats.attackRange += rangeIncrease;
            Debug.Log("Attack range increased!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade range.");
        }
    }

    public void BuyDamage()
    {
        if (playerCurrency.SpendMoney(damageCost))
        {
            playerStats.attackDamage += damageIncrease;
            Debug.Log("Attack damage increased!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade damage.");
        }
    }
}
