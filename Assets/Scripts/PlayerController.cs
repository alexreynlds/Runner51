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
    private float speed = 12f;
    private float jumpPower = 14f;

    //==============================
    // Setting the player variables
    //==============================
    public int Atoms;
    string highScoreKey = "HighScore";

    public int gameMode;
    public GameObject SideCam;
    public GameObject BackCam;
    private GameObject player;

    private int lane = 0;

    void Awake(){
        //Set the controller
        Controls = new InputMaster();
        gameMode = 0;

        Controls.Player.Test.performed += _ => Test();

    }

    void Start(){
        Atoms = PlayerPrefs.GetInt(highScoreKey,0);
        player = GameObject.Find("Player");

        GameObject startPoint = GameObject.Find("startPoint");
        player.transform.position = startPoint.transform.position;
    }

    void FixedUpdate()
    {   

        if(gameMode == 0){
        //Update the left or right depnding on the horizontal level - Detemined by the inputs
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);          
        }

        if(gameMode == 1){
            if(lane == 0){
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            }
            rb.velocity = new Vector2(2 * speed, rb.velocity.y);
        }

        if(player.transform.position.y <= -10){
            GameObject startPoint = GameObject.Find("startPoint");
            player.transform.position = startPoint.transform.position;
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
        }
        //Lane Movement
    }

    void Test(){
        Debug.Log("Test");
        Atoms++;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.name);
        // if(horizontal == 1){
        //     rb.velocity = new Vector2(-100, rb.velocity.y); 
            // rb.AddExplosionForce(10000, player.transform.position, 50);
        //}
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "1Trigger"){
            SideCam.SetActive(false);
            BackCam.SetActive(true);
            gameMode = 1;
        }
        if(other.gameObject.name == "Atom"){
            Atoms++;
            Destroy(other.gameObject);
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
