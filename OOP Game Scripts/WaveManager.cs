using TMPro;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] int waveTime = 30;

    public static WaveManager Instance;

    bool waveRunning = true;
    int currentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartNewWave();
        timeText.text = waveTime.ToString();
        waveText.text = "Wave: 1";
    }

    public bool WaveRunning() => waveRunning;

    private void StartNewWave()
    {
        StopAllCoroutines();
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = waveTime;
        waveText.text = "Wave: " + currentWave;

        // Reset Player Health at the start of each wave
        ResetPlayerHealth();

        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;

            timeText.text = currentWaveTime.ToString();

            if (currentWaveTime <= 0)
            {
                WaveComplete();
            }
        }
        yield return null;
    }

    private void WaveComplete()
    {
        StopAllCoroutines();
        EnemyManager.Instance.DestroyAllEnemies();
        waveRunning = false;
        currentWaveTime = waveTime;
        timeText.text = currentWaveTime.ToString();
        timeText.color = Color.red;

        GameManager.instance.StartShopPhase();
    }

    /// <summary>
    /// Resets the player's health to maximum at the start of every wave.
    /// </summary>
    private void ResetPlayerHealth()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ResetHealth();
            Debug.Log("Player health reset to maximum for the new wave.");
        }
        else
        {
            Debug.LogWarning("Player object not found. Cannot reset health.");
        }
    }
}
