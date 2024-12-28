using UnityEngine;

public class Projectile : MonoBehaviour
{
   float speed = 20f;
   [SerializeField] int damage = 25;

   private void FixedUpdate(){
    transform.Translate(Vector2.right * speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D collision){
    var enemy = collision.gameObject.GetComponent<Enemy>();
    if(enemy!=null){
        Destroy(gameObject);
        enemy.Hit(damage);
    }
   }
}
