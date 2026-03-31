using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject player;
    public string var;
    public int add;
    public void collected()
    {
        PlayerMoveTopDown playerMethods = player.gameObject.GetComponent<PlayerMoveTopDown>();
        playerMethods.collect(var,add);
    }
}
