using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    [Space,Header("Movement settings")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchingSpeed = 3.75f;
    [Space]
    [Tooltip("max jump haight relative to the place the start jumping")]
    public float maxJumpHeight = 8.0f;
    [Tooltip("The max strenght applied when performing jump")]
    public float jumpStrength = 8.0f;
    [Tooltip("How quickly move across the curve")]
    public float jumpVelocity = 1f;
    [Tooltip("How the force of the jump will be mdified over time")]
    public AnimationCurve jumpCurve;
    [Space]
    public float gravityStrength = 20f;
    public AnimationCurve gravityCurve;
    public float gravityVelocity = 1f;
    [Space]
    public float groundDistance = 1f;

    [Space,Header("Look settings")]
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Space,Header("Audio settings")]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;

    [Space,Header("Input System"),Tooltip("Can be null. If null will get input system from player")]
    public PlayerInputManager input;

    public Gun gun;

    float rotation_x = 0;
    float jump_factor = 0;
    float gravity_factor = 0;
    float jumpforce_y = 0;
    float gravityforce_y = 0;    
    float start_jump_height = 0;
    bool canJumping = false;
    bool performingJump = false;
    Coroutine fireCoroutine;   
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public float movementDirectionY;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isCrouch = false;
    [HideInInspector] public bool isMoving = false;

    

    

    void Start() {
        if(input == null) { 
            GameObject A = GameObject.Find("Player");
            if(A == null) { Debug.Log("No [Player] gameobject found for script <SC_FPSController>"); }
            input = A.GetComponent<PlayerInputManager>(); 
            if(input == null) { Debug.Log("No <PlayerInputManager> component found for script <SC_FPSController>"); }
        }

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        Vector2 inputDirectionValues = input.inputSet.Movement.Move.ReadValue<Vector2>();
        Vector2 inputRotationValues = input.inputSet.Movement.Look.ReadValue<Vector2>();
        bool isJumping = input.inputSet.Movement.Jump.ReadValue<float>() == 1 ? true : false;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 up = transform.TransformDirection(Vector3.up);

        float velocity_x = inputDirectionValues.y * walkingSpeed;
        float velocity_y = inputDirectionValues.x * walkingSpeed;
        float velocity_z = 0;

        //--------------------[CALCULATE JUMP & GRAVITY FORCE]--------------------  
        float max_factor_j,max_factor_g;
        
        max_factor_j = 0;
        if((isJumping && canJumping) || performingJump) {
            //performing jump
            max_factor_j = 1;
            max_factor_g = 0;
            
            performingJump = true;
            if(start_jump_height == 0) { start_jump_height = transform.position.y; }
            if((transform.position.y - start_jump_height) >= maxJumpHeight) { canJumping = false; performingJump = false; }

        } else if(characterController.isGrounded) {
            //is on ground
            max_factor_g = 0; 
            jump_factor = 0;
            gravity_factor = 0;

            performingJump = false;
            canJumping = true;
            start_jump_height = 0;
        } else {
            //is in air
            max_factor_g = 1; 
            
            canJumping = false;
            performingJump = false;
        }

        jump_factor = Mathf.Lerp(jump_factor,max_factor_j,(start_jump_height + maxJumpHeight) - transform.position.y);
        jumpforce_y = jumpCurve.Evaluate(jump_factor); 

        gravity_factor = Mathf.Lerp(gravity_factor,max_factor_g,Time.deltaTime * gravityVelocity);
        gravityforce_y = gravityCurve.Evaluate(gravity_factor); 
        //Debug.Log(jump_factor);
        velocity_z = (jumpforce_y * jumpStrength) - (gravityforce_y * gravityStrength);

        //END ----------------[CALCULATE JUMP & GRAVITY FORCE]--------------------  


        //Camera movement logic here

        Vector3 direction = (forward * velocity_x) + (right * velocity_y) + (up * velocity_z);

        characterController.Move(direction * Time.deltaTime);

        rotation_x += -inputRotationValues.y * (lookSpeed /100);
        float rotation_y = inputRotationValues.x * (lookSpeed /100);

        rotation_x = Mathf.Clamp(rotation_x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_x, 0, 0);
        transform.rotation *= Quaternion.Euler(0, rotation_y, 0);

        //End camera movement logic
        
        
    }

    void StartFiring(){
        fireCoroutine = StartCoroutine(gun.FastFire());
    }
    void stopFiring(){
        if(fireCoroutine != null){
        StopCoroutine(fireCoroutine);
        }
    }


}