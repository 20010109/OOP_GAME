using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [Header("Purchase Feedback UI")]
    [SerializeField] private Text purchaseFeedbackText;
    [SerializeField] private Text gunCountText;

    private void Awake()
    {
        // Validate key dependencies immediately
        if (purchaseFeedbackText == null)
            Debug.LogError("WeaponShop: purchaseFeedbackText is not assigned in the Inspector!");
        
        if (gunCountText == null)
            Debug.LogError("WeaponShop: gunCountText is not assigned in the Inspector!");
    }

    private void Start()
    {
        // Ensure GunManager and MoneyManager are initialized
        if (GunManager.Instance == null || MoneyManager.Instance == null)
        {
            Debug.LogError("WeaponShop: Required managers are not initialized. Check scene setup.");
            DisplayFeedback("Error: Required systems not initialized!");
            return;
        }

        InitializeShopUI();
    }

    private void InitializeShopUI()
    {
        UpdateGunCount();
        DisplayFeedback("Welcome to the Shop!");
    }

    /// <summary>
    /// Handles weapon purchase logic.
    /// </summary>
    public void BuyWeapon(string weaponName)
    {
        if (GunManager.Instance == null || MoneyManager.Instance == null)
        {
            Debug.LogError("WeaponShop: Required managers are not initialized.");
            DisplayFeedback("Error: Required systems not initialized!");
            return;
        }

        int cost = 0;

        switch (weaponName)
        {
            case "Gun":
                cost = 30;
                break;
            case "Gun2":
                cost = 100;
                break;
            case "Gun3":
                cost = 200;
                break;
            default:
                DisplayFeedback("Invalid Weapon!");
                Debug.LogError($"WeaponShop: Invalid weapon name - {weaponName}");
                return;
        }

        // Check if the player can afford the weapon
        if (MoneyManager.Instance.SpendMoney(cost))
        {
            if (GunManager.Instance.AddGun())
            {
                DisplayFeedback($"Purchased {weaponName}! Remaining Money: ${MoneyManager.Instance.totalMoney}");
                UpdateGunCount();
            }
            else
            {
                DisplayFeedback("No available slots for new weapons! Refunding money...");
                MoneyManager.Instance.AddMoney(cost); // Refund the money
            }
        }
        else
        {
            DisplayFeedback("Not enough money!");
        }
    }

    /// <summary>
    /// Handles weapon removal logic.
    /// </summary>
    public void RemoveWeaponFromSlot(int slotIndex)
    {
        if (GunManager.Instance == null)
        {
            Debug.LogWarning("WeaponShop: GunManager is not initialized when trying to remove a weapon.");
            DisplayFeedback("Error: Inventory not initialized!");
            return;
        }

        GunManager.Instance.RemoveWeapon(slotIndex);
        DisplayFeedback($"Removed weapon from slot {slotIndex + 1}");
        UpdateGunCount();
    }

    /// <summary>
    /// Updates the UI to reflect current weapon count.
    /// </summary>
    private void UpdateGunCount()
    {
        if (gunCountText != null && GunManager.Instance != null)
        {
            gunCountText.text = $"Guns: {GunManager.Instance.GetGunCount()}/{GunManager.Instance.GetMaxSlots()}";
            Debug.Log($"Gun Count Updated: {GunManager.Instance.GetGunCount()} / {GunManager.Instance.GetMaxSlots()}");
        }
        else
        {
            Debug.LogWarning("WeaponShop: gunCountText or GunManager is not properly set up.");
        }

        // Refresh inventory grid
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshUI(WeaponInventory.Instance.GetInventorySlots());
        }
    }



    /// <summary>
    /// Displays a feedback message to the player.
    /// </summary>
    private void DisplayFeedback(string message)
    {
        if (purchaseFeedbackText != null)
        {
            purchaseFeedbackText.text = message;
        }
        else
        {
            Debug.LogWarning("WeaponShop: purchaseFeedbackText is not assigned in the Inspector!");
        }
    }
}
