using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        /*** Collision Checker ***/
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("ah ha ah");
        }
        
    }
}
