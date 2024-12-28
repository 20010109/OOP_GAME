using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] Button restartButton;
    [SerializeField] Button nextWaveButton;
    [SerializeField] Text moneyText;
    [SerializeField] private GameObject inventoryPanel; 

    private bool gameRunning;

    public static GameManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    void Start(){
        restartButton.onClick.AddListener(RestartGame);
        nextWaveButton.onClick.AddListener(StartNextWave);
        UpdateMoneyText();
    }

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameRunning(){
        return gameRunning;
    }

    public void GameOver(){
        gameRunning = false;
        gameOverPanel.SetActive(true);
    }

    public void StartNextWave()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        gameRunning = true;
        WaveManager.Instance.Invoke("StartNewWave", 0.5f);
        Debug.Log("Next Wave Started");
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + MoneyManager.Instance.totalMoney;
        }
    }

    public void StartShopPhase()
{
    Time.timeScale = 0f;
    shopPanel.SetActive(true);
    inventoryPanel.SetActive(true); // Show InventoryPanel in shop
    gameRunning = false;
    UpdateMoneyText();

    if (InventoryUI.Instance != null && WeaponInventory.Instance != null)
    {
        InventoryUI.Instance.RefreshUI(WeaponInventory.Instance.GetInventorySlots());
    }
    else
    {
        Debug.LogWarning("GameManager: InventoryUI or WeaponInventory is not properly set up!");
    }
}
}
