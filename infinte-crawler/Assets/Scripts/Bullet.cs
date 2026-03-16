using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigi;
    public float lifeSpam;
    public float damage;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeSpam);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            EnemyFollow enemyMethods = other.gameObject.GetComponent<EnemyFollow>();
            enemyMethods.damageEnemy(damage);
            Destroy(gameObject);
            return;
        }else if (other.transform.CompareTag("Player")){
            //clear
        }else{
            Destroy(gameObject);
        }
        
    }
}