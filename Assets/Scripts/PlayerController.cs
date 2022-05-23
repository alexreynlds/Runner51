using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
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
    private float speed = 8f;
    private float jumpPower = 14f;

    //==============================
    // Setting the player variables
    //==============================
    public int Atoms;
    string highScoreKey = "HighScore";

    public int gameMode;
    public GameObject SideCam;
    public GameObject BackCam;
    public GameObject player;

    void Awake(){
        //Set the controller
        Controls = new InputMaster();
        gameMode = 0;
    }

    void Start(){
        Atoms = PlayerPrefs.GetInt(highScoreKey,0);
        Debug.Log("epic");
        player = GameObject.Find("Player");
    }

    void Update(){
        Debug.Log("epic");        
    }

    void FixedUpdate()
    {   

        if(gameMode == 0){
        //Update the left or right depnding on the horizontal level - Detemined by the inputs
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);          
        }

        // if(player.transform.position.y <= -10){
        //     GameObject startPoint = GameObject.Find("startPoint");
        //     player.transform.position = startPoint.transform.position;
        // }
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
        horizontal = context.ReadValue<Vector2>().x;
    }

    // public void OnCollisionEnter(Collision col) {
    //     Debug.Log("Epic");
    // }

    void Test(){
        Atoms++;
    }

    void OnEnable(){
        Controls.Player.Enable();
    }

    void OnDisable() 
    {
        Controls.Player.Disable();
    }
}
