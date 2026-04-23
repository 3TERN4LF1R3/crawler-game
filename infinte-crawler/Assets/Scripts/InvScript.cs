using UnityEngine;

public class InvScript : MonoBehaviour
{
    public bool scrollOn; //just for debugging
    public GameObject scrollBox;
    void setScrollBox(bool value)
    {
        scrollBox.SetActive(value);
    }

    void Start()
    {
        setScrollBox(false);
    }

    public void changeActiveState()
    {
        if(scrollOn){
            scrollOn = false;
            setScrollBox(false);
        } else{
            scrollOn = true;
            setScrollBox(true);
        }
    }
}
