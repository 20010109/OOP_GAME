using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    
    [Header("Stats")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 2f;
    //[SerializeField] int moneyDropAmount  = 10;
    [Range(0f, 1f)] public float dropChance = 0.5f;

    [Header("Charger")]
    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;
    bool isCharging = false;
    bool isPreparingCharge = false;

    private int currentHealth;

    Animator anim;
    Transform target;

    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isPreparingCharge) return;

        if(target!=null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);

            if(isCharger && !isCharging && Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime);
            }
        }
    }

    void StartCharging(){
        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;
    }

    public GameObject coinPrefab;

    public void Hit(int damage){
        currentHealth -= damage;
        anim.SetTrigger("hit");

        if(currentHealth <=0){
            if (Random.value <= dropChance)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
