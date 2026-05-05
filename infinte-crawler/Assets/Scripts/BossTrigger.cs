using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall;
    
    public void startBossFight()
    {
        boss.SetActive(true);
        wall.SetActive(true);
    }
}
