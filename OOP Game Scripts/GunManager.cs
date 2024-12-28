using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{   
    [SerializeField] private GameObject gunPrefab;

    Transform player;
    List<Vector2> gunPositions = new List<Vector2>();

    int spawnedGuns = 0;

    public static GunManager Instance; // Singleton for global access

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        // Predefined gun positions
        gunPositions.Add(new Vector2(-0.7f, -0.15f));
        gunPositions.Add(new Vector2(0.7f, -0.15f));
        gunPositions.Add(new Vector2(-1.4f, 0.2f));
        gunPositions.Add(new Vector2(1.4f, 0.2f));
        gunPositions.Add(new Vector2(-1f, -0.5f));
        gunPositions.Add(new Vector2(1f, -0.5f));
        //Starting Gun
        AddGun();

    }

    public bool AddGun()
    {
        if (spawnedGuns >= gunPositions.Count)
        {
            Debug.LogWarning("GunManager: No more available gun slots!");
            return false;
        }

        Vector2 pos = gunPositions[spawnedGuns];
        GameObject newGun = Instantiate(gunPrefab, player.position, Quaternion.identity, player);

        newGun.transform.localPosition = pos; 
        newGun.transform.localRotation = Quaternion.identity;

        Gun gunComponent = newGun.GetComponent<Gun>();
        if (gunComponent != null)
        {
            gunComponent.SetOffset(pos);
        }

        spawnedGuns++;
        Debug.Log($"Gun added successfully at position {pos}");
        return true;
    }

    public void RemoveWeapon(int slotIndex)
    {
        if (slotIndex < spawnedGuns && slotIndex >= 0)
        {
            Debug.Log($"Removing weapon from slot {slotIndex + 1}");
            // Logic to remove weapon
            spawnedGuns--;
        }
    }

    public int GetGunCount()
    {
        return spawnedGuns;
    }

    public int GetMaxSlots()
    {
        return gunPositions.Count;
    }
}
