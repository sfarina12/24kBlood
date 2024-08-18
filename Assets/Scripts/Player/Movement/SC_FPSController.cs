using System.Collections;
using System.Collections.Generic;
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
    public float jumpStrength = 8.0f;
    public AnimationCurve jumpDecrementFactor;
    [Space]
    public float gravity = 20f;
    public AnimationCurve gravityDecrementFactor;

    [Space,Header("Look settings")]
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Space,Header("Audio settings")]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;

    float rotationX = 0;
    PlayerInputActionSet playerInput;
    float jump_factor = 0;
    float gravity_factor = 0;
    bool canJumping = false;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public float movementDirectionY;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isCrouch = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isGrounded = false;

    void Start() {
        playerInput = new PlayerInputActionSet();
        playerInput.Enable();

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        Vector2 inputValues = playerInput.Movement.Move.ReadValue<Vector2>();

        //trasformare la direzione Forward e Right dal locale a global
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 up = transform.TransformDirection(Vector3.up);

        float velocity_x = inputValues.y * walkingSpeed;
        float velocity_y = inputValues.x * walkingSpeed;
        float velocity_z = 0;

        bool isJumping = playerInput.Movement.Jump.ReadValue<float>() == 1 ? true : false;

        float tmp_gravity = 0;
        if(!characterController.isGrounded) { 
            gravity_factor = Mathf.Lerp(gravity_factor,1,Time.deltaTime);
            tmp_gravity = gravityDecrementFactor.Evaluate(gravity_factor); 
        } else { gravity_factor = 0; canJumping = true; jump_factor = 0;}
        
        float tmp_jump = 0;
        if(isJumping && canJumping) { 
            jump_factor = Mathf.Lerp(jump_factor,1,Time.deltaTime);
            tmp_jump = jumpDecrementFactor.Evaluate(jump_factor); 
        } else { jump_factor = 0; canJumping = false; }

        velocity_z += tmp_jump * jumpStrength;
        velocity_z -= tmp_gravity * gravity;

        Vector3 direction = (forward * velocity_x) + (right * velocity_y) + (up * velocity_z);

        //Time.deltaTime necessario per legare l'update dei valori di movimento al framerate. Se lo togli schizza a velocita' supersonica
        characterController.Move(direction * Time.deltaTime);






        // isRunning = Input.GetKey(KeyCode.LeftShift);
        // isCrouch = Input.GetKey(KeyCode.LeftControl);

        // //isGrounded = groundChecker();
        // isGrounded = characterController.isGrounded;

        // if (canMove) {
        //     if (isRunning && !isCrouch) {
        //         curSpeedX = runningSpeed * Input.GetAxis("Vertical");
        //         curSpeedY = runningSpeed * Input.GetAxis("Horizontal");
        //     } else if (isCrouch) {
        //         curSpeedX = crouchingSpeed * Input.GetAxis("Vertical");
        //         curSpeedY = crouchingSpeed * Input.GetAxis("Horizontal");
        //     } else {
        //         curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
        //         curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
        //     }
        

        //     float movementDirectionY = moveDirection.y;
        //  moveDirection = (forward * cursorSpeedX) + (right * cursorSpeedY);

        //     if (moveDirection == Vector3.zero) { audioWalking.stopAudio(); audioRunning.stopAudio(); }
        //     else if (isRunning) { audioRunning.playAudio(); audioWalking.stopAudio(); }
        //     else { audioWalking.playAudio(); audioRunning.stopAudio(); }


        //     if (Input.GetKeyDown("space") && canMove && isGrounded) { moveDirection.y = jumpSpeed; }
        //     else { moveDirection.y = movementDirectionY; }

        //     if (!isGrounded) { moveDirection.y -= gravity * Time.deltaTime; }    
            
        //  characterController.Move(moveDirection * Time.deltaTime);

        //     if(moveDirection == Vector3.zero) { isMoving = false; }
        //     else { isMoving = true; }

        //     // Player and Camera rotation
        //     if (canMove) {
        //         rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        //         rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        //         playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //         transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        //     }
        // } else { isMoving = false; }
    }

}