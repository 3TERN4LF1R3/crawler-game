using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigi;
    public float lifeSpam;
    public float damage;
    public float speed;
    public bool useAmmo;
    public float spawnDist;

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