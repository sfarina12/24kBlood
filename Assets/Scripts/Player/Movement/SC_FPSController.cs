using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float maxJumpHeight = 8.0f;
    public float jumpStrength = 8.0f;
    public float jumpVelocity = 1f;
    public AnimationCurve jumpCurve;
    [Space]
    public float gravityStrength = 20f;
    public AnimationCurve gravityCurve;
    public float gravityVelocity = 1f;
    [Space]
    public float groundDistance = 1f;
    public LayerMask ignoreLayers;

    [Space,Header("Look settings")]
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Space,Header("Audio settings")]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;

    [Space, Header("Gun Settings for player")]
    public Gun gun;

    Coroutine fireCoroutine;
    float rotation_x = 0;
    float jump_factor = 1;
    float gravity_factor = 0;
    float jumpforce_y = 0;
    float gravityforce_y = 0;    
    float start_jump_height = -1.4f;
    bool canJumping = false;
    PlayerInputActionSet playerInput;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public float movementDirectionY;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isCrouch = false;
    [HideInInspector] public bool isMoving = false;

    

    

    void Start() {
        playerInput = new PlayerInputActionSet();
        playerInput.Enable();

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Subscription to button actions...
        
        //playerInput.Combat.Shoot.performed += ctx => gun.Shoot();
        playerInput.Combat.StartFiring.performed += ctx => StartFiring();
        playerInput.Combat.StopFiring.performed += ctx => stopFiring();
        playerInput.Combat.Reload.performed += ctx => gun.ReloadFunction();

        //
    }

    void Update() {
        Vector2 inputDirectionValues = playerInput.Movement.Move.ReadValue<Vector2>();
        Vector2 inputRotationValues = playerInput.Movement.Look.ReadValue<Vector2>();
        bool isJumping = playerInput.Movement.Jump.ReadValue<float>() == 1 ? true : false;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 up = transform.TransformDirection(Vector3.up);

        float velocity_x = inputDirectionValues.y * walkingSpeed;
        float velocity_y = inputDirectionValues.x * walkingSpeed;
        float velocity_z = 0;

        //--------------------[CALCULATE JUMP & GRAVITY FORCE]--------------------  
        float max_factor_j,max_factor_g;
        
        if(isJumping && canJumping) {
            max_factor_j = 0;
            max_factor_g = 0;
            if(start_jump_height == -1.4f) { start_jump_height = transform.position.y; }
            if((transform.position.y - start_jump_height) >= maxJumpHeight) { canJumping = false; }
        } else if(characterController.isGrounded) {
            max_factor_j = 1;
            max_factor_g = 0; 
            canJumping = true;
            start_jump_height = -1.4f;
        } else {
            max_factor_j = 1;
            max_factor_g = 1; 
            canJumping = false;
        }

        jump_factor = Mathf.Lerp(jump_factor,max_factor_j,Time.deltaTime * jumpVelocity);
        jumpforce_y = jumpCurve.Evaluate(jump_factor); 

        gravity_factor = Mathf.Lerp(gravity_factor,max_factor_g,Time.deltaTime * gravityVelocity);
        gravityforce_y = gravityCurve.Evaluate(gravity_factor); 

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