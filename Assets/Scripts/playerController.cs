using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    //========================================
    // Creating an instance of the controller
    //========================================
    private InputMaster Controls;

    //===========================
    // Movement System Variables
    //===========================

    public Rigidbody rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    public float speed = 12f;
    public float jumpPower = 14f;

    //==============================
    // Setting the player variables
    //==============================
    public int lives;
    public int startLives;
    public int atoms;
    public int shield;
    string highScoreKey = "HighScore";

    public int gameMode;
    public GameObject SideCam;
    public GameObject BackCam;
    private GameObject player;
    public GameObject playerModel;

    private int lane = 0;

    void Awake(){
        //Set the controller
        Controls = new InputMaster();
        gameMode = 0;

        Controls.Player.Test.performed += _ => Test();
        Controls.Player.Pause.performed += _ => GameObject.Find("UICanvas").GetComponent<UIScript>().pauseGame();
    }

    void Start(){
        lives = PlayerPrefs.GetInt("playerLives");
        startLives = PlayerPrefs.GetInt("playerLives");
        atoms = PlayerPrefs.GetInt(highScoreKey,0);
        shield = 0;
        player = GameObject.Find("Player");

        GameObject startPoint = GameObject.Find("startPoint");
        player.transform.position = startPoint.transform.position;
    }

    void FixedUpdate()
    {   
        if(gameMode == 0){
        //Update the left or right depnding on the horizontal level - Detemined by the inputs
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);          
        }

        if(gameMode == 1){  
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, lane);
            rb.velocity = new Vector2(2 * speed, rb.velocity.y);
        }

        if(player.transform.position.y <= -100){
            doDeath();
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if (context.performed && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (context.canceled && rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    //==============================================
    // Checks to see if the player is on the ground
    //==============================================
    private bool IsGrounded(){
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }
    //===================================================
    // Sets the horizontal according to input left/right
    //===================================================
    public void Move(InputAction.CallbackContext context){
        //Platforming Movement
        if(gameMode == 0){
            horizontal = context.ReadValue<Vector2>().x;
            var rotationVector = playerModel.transform.rotation.eulerAngles;
            
            if(horizontal == 1){
                rotationVector.y = 0;
            }
            if(horizontal == -1){
                rotationVector.y = 180;
            }
            playerModel.transform.rotation = Quaternion.Euler(rotationVector);
        }
        //Lane Movement
        if(gameMode == 1){
            float laneMove = context.ReadValue<Vector2>().x;
            if(context.performed){
                changeLane(laneMove);
            }
        }
    }

    void changeLane(float laneMove){
        if(laneMove == 1 && lane !=-2){
            Debug.Log("moved left");
            lane = lane -1;
        }
        if(laneMove == -1 && lane !=2){
            lane = lane + 1;
        }
    }

    void Test(){
        doDeath();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "1Trigger"){
            GameObject.Find("UICanvas").GetComponent<UIScript>().showRunnerScreen();
            SideCam.SetActive(false);
            BackCam.SetActive(true);
            gameMode = 1;
        }
        if(other.gameObject.name == "endPoint"){
            GameObject.Find("UICanvas").GetComponent<UIScript>().showEndScreen();
        }
        if(other.gameObject.tag == "autoSpawnTrigger"){
            if(gameMode==1){
                Debug.Log("Triggered");
                GameObject.Find("levelManager").GetComponent<roadSpawner>().moveRoad();
            }            
        }
        if(other.gameObject.tag == "Cactus"){
            doDeath();
        }
        if (other.gameObject.tag == "enemyBody")
        {
            if (shield > 0)
            {
                shield--;//prevents death
            }
            else
            {
                doDeath();
            }
        }
        if (other.gameObject.tag == "enemyHead"){
            Destroy(other.transform.parent.gameObject);
            atoms++;
        }
    }

    public void doDeath(){
        if(lives > 0){
            lives--;
            GameObject.Find("levelManager").GetComponent<levelManager>().timesDied += 1;
            PlayerPrefs.SetInt("playerLives", lives);    
            GameObject startPoint = GameObject.Find("startPoint");
            player.transform.position = startPoint.transform.position;      
        } 
        if (lives == 0){
            GameObject.Find("UICanvas").GetComponent<UIScript>().showDeathScreen();
        }
    }

    void OnEnable(){
        Controls.Player.Enable();
    }

    void OnDisable() 
    {
        Controls.Player.Disable();
    }
}
