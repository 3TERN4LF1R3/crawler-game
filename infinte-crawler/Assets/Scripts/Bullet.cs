using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigi;
    public float lifeSpam;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        // set velocity directly for consistent 2D movement
        Destroy(gameObject, lifeSpam);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }else if (other.transform.CompareTag("Player")){
            //clear
        }else{
            Destroy(gameObject);
        }
        
    }
}