using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("Inventory Grid UI")]
    [SerializeField] private Transform inventoryGrid; // Grid Layout Group
    [SerializeField] private GameObject slotPrefab;   // Slot prefab

    private List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeInventorySlots();
    }

    /// <summary>
    /// Create slots dynamically based on max slots in WeaponInventory.
    /// </summary>
    private void InitializeInventorySlots()
    {
        if (inventoryGrid == null)
        {
            Debug.LogError("InventoryUI: Inventory Grid is not assigned in the Inspector!");
            return;
        }

        slots.Clear(); // Clear any existing slots

        for (int i = 0; i < WeaponInventory.Instance.GetMaxSlots(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryGrid);
            slot.name = $"Slot_{i + 1}";
            slots.Add(slot);

            // Assign OnClick Listener Dynamically
            Button slotButton = slot.GetComponent<Button>();
            if (slotButton != null)
            {
                int slotIndex = i; // Capture the index for the listener
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => OnSlotClicked(slotIndex));
            }
        }
    }

    /// <summary>
    /// Refresh the UI to reflect the current inventory state.
    /// </summary>
    public void RefreshUI(List<GameObject> weapons)
    {
        if (slots.Count == 0)
        {
            Debug.LogError("InventoryUI: No slots available. Ensure InitializeInventorySlots is called.");
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            Text slotText = slots[i].GetComponentInChildren<Text>();
            Button slotButton = slots[i].GetComponent<Button>();

            if (weapons[i] != null)
            {
                slotText.text = "Occupied";
                slots[i].GetComponent<Image>().color = Color.green;

                // Update Click Listener
                int slotIndex = i;
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => OnSlotClicked(slotIndex));
            }
            else
            {
                slotText.text = "Empty";
                slots[i].GetComponent<Image>().color = Color.white;

                // Clear Click Listener for empty slots
                slotButton.onClick.RemoveAllListeners();
            }
        }

        Debug.Log("InventoryUI: Grid refreshed successfully.");
    }

    /// <summary>
    /// Handles clicking on a weapon slot.
    /// </summary>
    private void OnSlotClicked(int slotIndex)
    {
        Debug.Log($"Slot {slotIndex + 1} clicked");

        WeaponShop shop = FindObjectOfType<WeaponShop>();
        if (shop != null)
        {
            shop.RemoveWeaponFromSlot(slotIndex);
        }
    }
}
