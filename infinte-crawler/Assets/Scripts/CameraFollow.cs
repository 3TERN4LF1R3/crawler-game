using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;
    // Update is called once per frame
    void Update()
    {
        Vector3 target = toFollow.position;
        transform.position = new Vector3(target.x, target.y, transform.position.z);
    }
}
