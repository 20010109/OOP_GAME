using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance; // Singleton for global access
    public int totalMoney = 0;

    public static event Action OnMoneyChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        Debug.Log("Money Collected! Total: " + totalMoney);
        OnMoneyChanged?.Invoke();
    }

    public bool SpendMoney(int amount)
    {
        if (totalMoney >= amount)
        {
            totalMoney -= amount;
            Debug.Log("Money Spent! Remaining: " + totalMoney);
            OnMoneyChanged?.Invoke();
            return true;
        }
        Debug.Log("Not enough money!");
        return false;
    }
}
