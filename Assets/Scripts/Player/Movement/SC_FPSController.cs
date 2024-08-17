using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    private PlayerInputActionSet playerInput;

    [Space,Header("Movement settings")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchingSpeed = 3.75f;
    public float jumpSpeed = 8.0f;
    [Space]
    public float gravity = 20.0f;

    [Space,Header("Look settings")]
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Space,Header("Audio settings")]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;

    private float rotationX = 0;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
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
        Vector2 debugMovement = playerInput.Movement.Move.ReadValue<Vector2>();
        Debug.Log(debugMovement);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Aggiungere i comandi di corsa e accovacciamento + salto con relative velocità

        // isRunning = Input.GetKey(KeyCode.LeftShift);
        // isCrouch = Input.GetKey(KeyCode.LeftControl);

        // //isGrounded = groundChecker();
        // isGrounded = characterController.isGrounded;

        float cursorSpeedX = debugMovement.x*walkingSpeed;
        float cursorSpeedY = debugMovement.y*walkingSpeed;
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
             moveDirection = (forward * cursorSpeedX) + (right * cursorSpeedY);

        //     if (moveDirection == Vector3.zero) { audioWalking.stopAudio(); audioRunning.stopAudio(); }
        //     else if (isRunning) { audioRunning.playAudio(); audioWalking.stopAudio(); }
        //     else { audioWalking.playAudio(); audioRunning.stopAudio(); }


        //     if (Input.GetKeyDown("space") && canMove && isGrounded) { moveDirection.y = jumpSpeed; }
        //     else { moveDirection.y = movementDirectionY; }

        //     if (!isGrounded) { moveDirection.y -= gravity * Time.deltaTime; }    
            
             characterController.Move(moveDirection * Time.deltaTime);

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
    
    bool groundChecker() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down,Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) { Debug.Log(hit.transform.name); return true; }
        return false;
    }
}