using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    public enum textUpdate {gold,arrow}
    public textUpdate textState;
    public GameObject player;
    public TextMeshProUGUI updateText;
    private PlayerMoveTopDown playerMethods;
    
    void Awake(){
        playerMethods = player.gameObject.GetComponent<PlayerMoveTopDown>();
    }
    void Update()
    {
        
        switch (textState)
        {
            case textUpdate.gold:
                updateText.text = playerMethods.getCoins().ToString();
                break;
            case textUpdate.arrow:
                updateText.text = playerMethods.getAmmo().ToString();
                break;
        }
    }
}
