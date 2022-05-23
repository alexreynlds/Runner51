using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster Controls;
    public Rigidbody rb;
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

    void Awake()
    {
        //Setting the controls, using Unity's new input system
        Controls = new InputMaster();
        MoveSpeed = 3.5f;

        GameMode = 0;
        
        Controls.Player.Jump.performed += _ => Jump();
        Controls.Player.Jump.performed += _ => Test();
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
            MoveDirection = Controls.Player.Movement.ReadValue<Vector2>();
            rb.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
        }
    }

    void Jump(){
        Debug.Log("Jump");
    }

    void Test(){
        Atoms++;
    }

    private void OnEnable() 
    {
        Controls.Enable();
    }

    private void OnDisable() 
    {
        PlayerPrefs.SetInt(highScoreKey, Atoms);
        PlayerPrefs.Save();
        Controls.Disable();
    }
}
