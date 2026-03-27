using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float range;
    private Transform target;
    private Rigidbody2D rigi;
    private RaycastHit2D collis;
    public float health;
    private bool canMove = true;
    public float damage;
    public float bounciness;
    public LayerMask playerLayer;
    public int minCoin;
    public int maxCoin;
    public GameObject coin;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(canMove){
        //Debug.Log("Trying to find Player");
        collis = Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, range, playerLayer);
        if (collis.collider != null && collis.collider.CompareTag("Player"))
        {
            //Debug.Log("Found Player");
            Vector2 direction = (target.position - transform.position).normalized;
            Vector2 newPos = rigi.position + direction * (speed / 5) * Time.fixedDeltaTime;
            rigi.MovePosition(newPos);
        }
        }
    }
    void spawnCoins()
    {
        int randomCoinDrop = UnityEngine.Random.Range(minCoin, maxCoin);
        Debug.Log(randomCoinDrop);
        for (int i = 1; i <= randomCoinDrop; i++) 
        {
            Debug.Log("Dropped Coin");
            GameObject newCoin = Instantiate(coin, transform.position, transform.rotation);
        }
    }

    public void damageEnemy(float damageInt)
    {
        health -= damageInt;
        if (health <= 0)
        {
            spawnCoins();
            Destroy(gameObject);
        }
        Debug.Log(health);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Touching");
            PlayerMoveTopDown playerMethods = other.gameObject.GetComponent<PlayerMoveTopDown>();
            playerMethods.DamagePlayer(damage);
            canMove = false;
            Vector2 directionBounce = (transform.position - other.transform.position).normalized;
            rigi.AddForce(directionBounce * bounciness, ForceMode2D.Impulse);
            Invoke(nameof(canMoveSetTrue), 1f);
        }
    }
    void canMoveSetTrue()
    {
        canMove = true;
    }
}