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
    private bool facingRight;

    //method called when jump button depressed
    //Awake is called when object first instantiates in game
    void Awake()
    {
        facingRight = true;
        rigi = GetComponent<Rigidbody2D>();
        ctrl = new InputSystem_Actions();
        ctrl.Enable();
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
        if (moveVect.x < 0 && facingRight)
            flip();
        else if (moveVect.x > 0 && !facingRight)
            flip();
        
        //scale horizontal movement
        moveVect.y = moveVect.y * speed * Time.deltaTime;
        moveVect.x = moveVect.x * speed * Time.deltaTime;
        //apply movement vector
        rigi.AddForce(moveVect);
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x = -1 * theScale.x;
        this.transform.localScale = theScale;
    }

}
