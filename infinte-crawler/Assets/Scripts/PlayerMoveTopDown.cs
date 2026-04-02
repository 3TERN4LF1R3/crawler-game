using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMoveTopDown : MonoBehaviour
{
    private InputSystem_Actions ctrl;
    public float speed;  //typical 480
    private Rigidbody2D rigi;
    private string direction = "right";
    public GameObject projectile;
    //public Image HealthBar;
    public float healthAmount = 200f;
    public GameObject facingRight;
    public GameObject facingLeft;
    public GameObject facingDown;
    public GameObject facingUp;
    private SpriteRenderer rightRenderer;
    private SpriteRenderer leftRenderer;
    private SpriteRenderer downRenderer;
    private SpriteRenderer upRenderer;
    public bool canShoot = true;
    public bool useAmmo;
    public float startingAmmo;
    public float ammo;
    private float coins = 0;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI ammoText;
    
    
    
    void updateAmmo()
    {
        /*** Updates Ammo ***/
        ammoText.text = "Ammo: " + ammo.ToString();
        Debug.Log("ammo " + ammo);
    }

    void resetCanShoot()
    {
        /*** Resets canShoot ***/
        canShoot = true;
    }
    
    void fire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        /*** Projectile firing ***/
        Bullet bulletVars = projectile.gameObject.GetComponent<Bullet>();
        if(canShoot == true){
            if(bulletVars.useAmmo == true && ammo < 1)
            {
                return;
            }
            else
            {
                Vector2 offset = Vector2.zero;
                Quaternion bulletRotation = Quaternion.identity;

                if(direction == "up")
                {
                    offset = Vector2.up * bulletVars.spawnDist;
                    bulletRotation = Quaternion.Euler(0, 0, 90);
                } 
                else if(direction == "down")
                {
                    offset = Vector2.down * bulletVars.spawnDist;
                    bulletRotation = Quaternion.Euler(0, 0, 270);
                } 
                else if(direction == "right")
                {
                    offset = Vector2.right * bulletVars.spawnDist;
                    bulletRotation = Quaternion.Euler(0, 0, 0);
                }
                else if(direction == "left")
                {
                    offset = Vector2.left * bulletVars.spawnDist;
                    bulletRotation = Quaternion.Euler(0, 0, 180);
                }

                Vector3 spawnPos = (Vector2)transform.position + offset;
                GameObject newBullet = Instantiate(projectile, spawnPos, bulletRotation);
                Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
                Vector3 theScale = rbBullet.transform.localScale;
                
                if(direction == "up")
                {
                    rbBullet.AddForce(Vector2.up * bulletVars.speed, ForceMode2D.Impulse);
                } 
                else if(direction == "down")
                {
                    rbBullet.AddForce(Vector2.down * bulletVars.speed, ForceMode2D.Impulse);
                } 
                else if(direction == "right")
                {
                    rbBullet.AddForce(Vector2.right * bulletVars.speed, ForceMode2D.Impulse);
                } 
                else if(direction == "left")
                {
                    rbBullet.AddForce(Vector2.left * bulletVars.speed, ForceMode2D.Impulse);
                }


                ammo --;
                updateAmmo();
                canShoot = false;
                Invoke(nameof(resetCanShoot), 0.5f);
            }
        }
        else
        {
            return;
        }
    }



    void UpdateHealth()
    {
        /*** Updates Health ***/
        Debug.Log(healthAmount);
        if(healthAmount < 1)
        {

        }
        //HealthBar.fillAmount = healthAmount / 200f;
    }
    public void DamagePlayer(float damage)
    {
        /*** Damages player: Enemy follow calls this ***/
        healthAmount -= damage;
        UpdateHealth();
    }



    void Awake()
    {
        /*** Set up for player ***/
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

        ammo = startingAmmo;
        updateAmmo();

        coinsText.text = "0";

        
    }

   

    private void OnDisable()
    {
        /*** Disable ctrl: DO NOT REMOVE ***/
        ctrl.Disable();
    }



    void changeDirection(string direc)
    {
        /*** Changes direction and player's sprite ***/
        if(direc == "Left")
        {
            rightRenderer.enabled = false;
            leftRenderer.enabled = true;
            downRenderer.enabled = false;
            upRenderer.enabled = false;
            direction = "left";
        } 
        else if(direc == "Right")
        {
            rightRenderer.enabled = true;
            leftRenderer.enabled = false;
            downRenderer.enabled = false;
            upRenderer.enabled = false;
            direction = "right";
        } 
        else if(direc == "Up")
        {
            rightRenderer.enabled = false;
            leftRenderer.enabled = false;
            downRenderer.enabled = false;
            upRenderer.enabled = true;
            direction = "up";
        } 
        else if(direc == "Down")
        {
            rightRenderer.enabled = false;
            leftRenderer.enabled = false;
            downRenderer.enabled = true;
            upRenderer.enabled = false;
            direction = "down";
        }
    }
    


    void addCoins(int add)
    {
        /*** Adds coins to coin total ***/
        coins += add;
        coinsText.text = coins.ToString();
        //Debug.Log("Coins: " + coins);
    }
    public void collect(string var, float add)
    {
        /*** Public function for collectable to call: DONT CALL FROM HERE ***/
        if(var == "a")
        {
            ammo += add;
            updateAmmo();
        }
    }


    
    void Update()
    {
        /*** Movement for player ***/
        Vector2 moveVect = ctrl.Player.Move.ReadValue<Vector2>();



        if (moveVect.x < 0 && Mathf.Abs(moveVect.x) > Mathf.Abs(moveVect.y))
        {
            changeDirection("Left");
        }
        else if (moveVect.x > 0 && Mathf.Abs(moveVect.x) > Mathf.Abs(moveVect.y))
        {
            changeDirection("Right");
        }
        else if (moveVect.y > 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x))
        {
            changeDirection("Up");
        }
        else if (moveVect.y < 0 && Mathf.Abs(moveVect.y) > Mathf.Abs(moveVect.x))
        {
            changeDirection("Down");
        }
            
        
        moveVect.y = moveVect.y * speed * Time.deltaTime;
        moveVect.x = moveVect.x * speed * Time.deltaTime;
        rigi.AddForce(moveVect);
    }
    
    

    void OnCollisionEnter2D(Collision2D other)
    {
        /*** Collision Checker ***/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*** Trigger Checker ***/
        if (other.transform.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            addCoins(1);
        } else if (other.transform.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            Collectable collectMethods = other.gameObject.GetComponent<Collectable>();
            collectMethods.collected();
        }
    }
    

}
