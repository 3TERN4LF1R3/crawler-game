using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigi;
    public float speed;
    public float lifeSpam;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        // set velocity directly for consistent 2D movement
        rigi.AddForce(this.transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeSpam);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        if (other.transform.CompareTag("Maze"))
        {
            // don't destroy the maze itself; only destroy the bullet
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}