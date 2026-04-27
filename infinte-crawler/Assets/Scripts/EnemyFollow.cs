using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float range;
    private Transform target;
    private Rigidbody2D rigi;
    private RaycastHit2D collis;
    private float health;
    public float startingHealth;
    private bool canMove = true;
    public float damage;
    public float bounciness;
    public LayerMask playerLayer;
    public int minCoin;
    public int maxCoin;
    public GameObject coin;
    public GameObject item;
    public float chanceToDrop;
    public float rangeDrop;
    public bool flipSprite;
    private bool facingRight;
    public bool isBoss;
    public Sprite secondPhase; 
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polyCollider;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = startingHealth;
        Debug.Log(health);
    }
    void Update()
    {
        if(canMove){
        Vector2 moveVect = (target.position - transform.position).normalized;
        if (moveVect.x < 0 && facingRight){
            flip();
        }
        else if (moveVect.x > 0 && !facingRight){
            flip();
        }
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

        int randomCoinDrop = UnityEngine.Random.Range(minCoin, maxCoin + 1);
        Debug.Log(randomCoinDrop);
        for (int i = 1; i <= randomCoinDrop; i++) 
        {
            float rad = Random.Range(0f, rangeDrop);
            float angle = Random.Range(0f, 2 * Mathf.PI);
            float x = rad * Mathf.Cos(angle) + this.transform.position.x;
            float y = rad * Mathf.Sin(angle) + this.transform.position.y;
            GameObject newCoin = Instantiate(coin, new Vector2(x, y), transform.rotation);
        }
    }
    void spawnItem()
    {
        int randomItemDrop = UnityEngine.Random.Range(1, 101);
        if(randomItemDrop <= chanceToDrop){
            GameObject newItem = Instantiate(item, transform.position, transform.rotation);
        } else {

        }
    }

    public void damageEnemy(float damageInt)
    {

        health -= damageInt;
        Debug.Log(health);
        if (health <= 0)
        {
            spawnCoins();
            spawnItem();
            Destroy(gameObject);
        } else if(health <= (startingHealth/2) && isBoss){
            spriteRenderer.sprite = secondPhase;
            polyCollider.CreateFromSprite(spriteRenderer.sprite);
            damage = damage * 2;
            speed = speed * 2;
        }
        //Debug.Log(health);
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
    private void flip()
    {
        if(flipSprite){
            facingRight = !facingRight;
            Vector3 theScale = this.transform.localScale;
            theScale.x = -1 * theScale.x;
            this.transform.localScale = theScale;
        }
    }
}