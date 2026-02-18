using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerControl2 : MonoBehaviour
{
    private InputSystem_Actions ctrl;
    public float speed;  //typical 480
    private Rigidbody2D rigi;
    private string direction;
    public GameObject projectile;
    public float spawnDist;

    //method called when jump button depressed
    //Awake is called when object first instantiates in game
    void fire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        GameObject newBullet = Instantiate(projectile, this.transform.position, this.transform.rotation);
        Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
    }

    void Awake()
    {
        
        rigi = GetComponent<Rigidbody2D>();
        ctrl = new InputSystem_Actions();
        ctrl.Enable();
        ctrl.Player.Jump.performed += fire;
    }

   
    private void OnDisable()
    {
        ctrl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVect = ctrl.Player.Move.ReadValue<Vector2>();
        //check if facing right and moving left
        if (moveVect.x < 0 && Mathf.Abs(moveVect.x) > Mathf.Abs(moveVect.y)){
            direction = "Left";
        }else if (moveVect.x > 0 && Mathf.Abs(moveVect.x) > Mathf.Abs(moveVect.y)){
            direction = "Right";
        }else if (moveVect.y > 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x)){
            direction = "Up";
        }else if (moveVect.y < 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x)){
            direction = "Down";
        }
            
        
        //scale horizontal movement
        moveVect.y = moveVect.y * speed * Time.deltaTime;
        moveVect.x = moveVect.x * speed * Time.deltaTime;
        //apply movement vector
        rigi.AddForce(moveVect);
    }

    private void flip()
    {
        Vector3 theScale = this.transform.localScale;
        theScale.x = -1 * theScale.x;
        this.transform.localScale = theScale;
    }

}
