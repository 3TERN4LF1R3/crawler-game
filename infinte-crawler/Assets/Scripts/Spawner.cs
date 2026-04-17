using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject newEnemy;
    private float spawnTimer;
    public float interval;
    public float range;
    public float healthAmount;
    public bool takeDamage;

    void SpawnNewEnemy()
    {
        float rad = Random.Range(0f, range);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = rad * Mathf.Cos(angle) + this.transform.position.x;
        float y = this.transform.position.y;
        Instantiate(newEnemy, new Vector2(x, y), Quaternion.identity);
    }
    public bool getTakeDamage() {
        return takeDamage;
    }
    public void damageSpawner(float damage) {
        healthAmount -= damage;
        if (healthAmount <= 0) {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = interval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0f)
        {
            SpawnNewEnemy();
            spawnTimer = interval;
        }
    }
}
