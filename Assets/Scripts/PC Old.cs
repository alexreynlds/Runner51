using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public float Horizontal;
    public float Speed = 8f;
    public float JumpPower = 16f;
    public bool IsFacingRight = true;

    private InputMaster Controls;


    public float MoveSpeed;

    public int Atoms;

    Vector2 MoveDirection = Vector2.zero;

    string highScoreKey = "HighScore";

    //====================================================================================
    // GAME MODE
    // The game mode will change throughout the level depending on the type of part it is.
    // Will be set by certain triggers in the level
    // 0 = Platforming - Side View - Move left, right and jump
    // 1 = Rush Mode - Back View - Move left, right and jump
    // 2 = Lane Mode - Back View - Snap to lanes shown on the level.
    //====================================================================================
    public int GameMode;

    public GameObject SideCam;
    public GameObject BackCam;

    void Awake()
    {
        //Setting the controls, using Unity's new input system
        Controls = new InputMaster();
        MoveSpeed = 3.5f;

        GameMode = 0;
        
        // Controls.Player.Jump.performed += _ => Jump();
        // Controls.Player.Test.performed += _ => Test();
    }
    // Start is called before the first frame update
    void Start()
    {
        Atoms = PlayerPrefs.GetInt(highScoreKey,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameMode == 0){
            // MoveDirection = Controls.Player.Movement.ReadValue<Vector2>();
            // rb.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
        }
        rb.velocity = new Vector2(Horizontal * Speed, rb.velocity.y);

        if (IsFacingRight && Horizontal > 0f){
            Flip();
        }
        else if (IsFacingRight && Horizontal < 0f){
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context){
        Debug.Log(IsGrounded());
        if (context.performed && IsGrounded()){
            rb.velocity = new Vector3(rb.velocity.x, JumpPower, rb.velocity.z);
        }

        if (context.canceled && rb.velocity.y > 0f){
            // rb.velocity = new Vector2(rb.velocity.x, 0.5f);
            rb.velocity = new Vector3(rb.velocity.x, 0.5f, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        //Move to gamemode 1 - Rush
        if(other.name == "1Trigger"){
            SideCam.SetActive(false);
            BackCam.SetActive(true);
            GameMode = 1;
        }  
        //Move to gamemode 2 - Lane
        if(other.name == "2Trigger"){
            SideCam.SetActive(false);
            BackCam.SetActive(true);
            GameMode = 2;
        }      
    }

    // void Jump(){
    //     Debug.Log("Jump");
    // }

    void Test(){
        Atoms++;
    }

    void onTest(){

    }

    public bool IsGrounded(){
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }

    private void Flip(){
        IsFacingRight = !IsFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context){
        Horizontal = context.ReadValue<Vector2>().x;
    }

    // private void OnEnable() 
    // {
    //     Controls.Enable();
    // }

    // private void OnDisable() 
    // {
    //     PlayerPrefs.SetInt(highScoreKey, Atoms);
    //     PlayerPrefs.Save();
    //     Controls.Disable();
    // }
}
