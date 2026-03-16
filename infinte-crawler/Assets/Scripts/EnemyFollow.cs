using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float range;
    private Transform target;
    private Rigidbody2D rigi;
    private RaycastHit2D collis;
    public float health;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        collis = Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, range);
        if (collis.collider != null && collis.collider.CompareTag("Player"))
        {
            Vector2 direction = (target.position - transform.position).normalized;
            Vector2 velo = direction * speed * Time.deltaTime;
            rigi.MovePosition(rigi.position + velo);
        }
    }
    public void damageEnemy(float damageInt)
    {
        health -= damageInt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(health);
    }
}