using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject player;
    public string varC;
    public float addC;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void collected()
    {
        PlayerMoveTopDown playerMethods = player.gameObject.GetComponent<PlayerMoveTopDown>();
        playerMethods.collect(varC,addC);
    }
}
