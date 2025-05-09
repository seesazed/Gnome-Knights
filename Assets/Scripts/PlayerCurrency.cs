using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public int currentMoney = 100;

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
    }
}

