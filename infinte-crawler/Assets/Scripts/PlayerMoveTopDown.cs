using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerControl2 : MonoBehaviour
{
    private InputSystem_Actions ctrl;
    public float speed;  //typical 480
    private Rigidbody2D rigi;
    private string direction = "right";
    public GameObject projectile;
    public float spawnDist;
    //public Image HealthBar;
    public float healthAmount = 200f;
    public float bulletSpeed;
    public GameObject facingRight;
    public GameObject facingLeft;
    public GameObject facingDown;
    public GameObject facingUp;
    private SpriteRenderer rightRenderer;
    private SpriteRenderer leftRenderer;
    private SpriteRenderer downRenderer;
    private SpriteRenderer upRenderer;

    //method called when jump button depressed
    //Awake is called when object first instantiates in game

    void fire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 offset = Vector2.zero;
        if(direction == "up"){
            offset = Vector2.up * spawnDist;
        } else if(direction == "down"){
            offset = Vector2.down * spawnDist;
        } else if(direction == "right"){
            offset = Vector2.right * spawnDist;
        } else if(direction == "left"){
            offset = Vector2.left * spawnDist;
        }
        Vector3 spawnPos = (Vector2)transform.position + offset;
        GameObject newBullet = Instantiate(projectile, spawnPos, this.transform.rotation);
        Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
        Vector3 theScale = rbBullet.transform.localScale;
        if(direction == "up"){
            rbBullet.rotation = 90;
            rbBullet.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        } else if(direction == "down"){
            rbBullet.rotation = 270;
            rbBullet.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
        } else if(direction == "right"){
            rbBullet.rotation = 0;
            rbBullet.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
        } else if(direction == "left"){
            rbBullet.rotation = 180;
            rbBullet.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    void UpdateHealth()
    {
        if(healthAmount < 1){
        }
        //HealthBar.fillAmount = healthAmount / 200f;
    }

    void Awake()
    {
        rightRenderer = facingRight.GetComponent<SpriteRenderer>();
        leftRenderer = facingLeft.GetComponent<SpriteRenderer>();
        downRenderer = facingDown.GetComponent<SpriteRenderer>();
        upRenderer = facingUp.GetComponent<SpriteRenderer>();
        rightRenderer.enabled = true;
        leftRenderer.enabled = false;
        downRenderer.enabled = false;
        upRenderer.enabled = false;
        rigi = GetComponent<Rigidbody2D>();
        ctrl = new InputSystem_Actions();
        ctrl.Enable();
        ctrl.Player.Jump.performed += fire;
        UpdateHealth();
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
            rightRenderer.enabled = false;
            leftRenderer.enabled = true;
            downRenderer.enabled = false;
            upRenderer.enabled = false;
            direction = "left";
        }else if (moveVect.x > 0 && Mathf.Abs(moveVect.x) > Mathf.Abs(moveVect.y)){
            rightRenderer.enabled = true;
            leftRenderer.enabled = false;
            downRenderer.enabled = false;
            upRenderer.enabled = false;
            direction = "right";
        }else if (moveVect.y > 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x)){
            rightRenderer.enabled = false;
            leftRenderer.enabled = false;
            downRenderer.enabled = false;
            upRenderer.enabled = true;
            direction = "up";
        }else if (moveVect.y < 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x)){
            rightRenderer.enabled = false;
            leftRenderer.enabled = false;
            downRenderer.enabled = true;
            upRenderer.enabled = false;
            direction = "down";
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
