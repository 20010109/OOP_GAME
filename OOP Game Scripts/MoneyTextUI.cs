using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;

    private void Start()
    {
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= UpdateMoneyText;
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + MoneyManager.Instance.totalMoney;
        }
    }
}
