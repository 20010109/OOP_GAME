using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.Instance.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
