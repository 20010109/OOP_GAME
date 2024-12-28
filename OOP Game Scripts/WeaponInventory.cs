using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory Instance;

    private int maxSlots = 6; // 3x2 grid
    private List<GameObject> weapons = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Initialize inventory with empty slots
        for (int i = 0; i < maxSlots; i++)
        {
            weapons.Add(null); 
        }
    }

    public bool AddWeapon(GameObject weapon)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = weapon;
                Debug.Log($"Weapon added to slot {i + 1}");
                return true;
            }
        }
        Debug.LogWarning("No available slots in inventory!");
        return false;
    }

    public void RemoveWeapon(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < weapons.Count && weapons[slotIndex] != null)
        {
            Debug.Log($"Weapon removed from slot {slotIndex + 1}");
            weapons[slotIndex] = null;
        }
        else
        {
            Debug.LogWarning($"Invalid slot index {slotIndex}");
        }
    }

    public List<GameObject> GetInventorySlots()
    {
        return weapons;
    }

    public int GetWeaponCount()
    {
        return weapons.FindAll(weapon => weapon != null).Count;
    }

    public int GetMaxSlots()
    {
        return maxSlots;
    }
}
