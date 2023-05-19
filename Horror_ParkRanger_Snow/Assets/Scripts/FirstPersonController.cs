using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Camera mainCamera;

    bool isSprinting => canSprint && Input.GetKey(sprintKey);
    bool shouldJump => controller.isGrounded && Input.GetKey(jumpKey);

    [Header("Controls")]
    KeyCode interactKey = KeyCode.E;
    KeyCode sprintKey = KeyCode.LeftShift;
    KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField, Range(3, 5)] float walkSpeed = 5f;
    [SerializeField, Range(6, 10)] float sprintSpeed = 10f;
    float runMultiplier;
    float gravityValue = 9.8f;
    float verticalSpeed;

    Vector2 currentInput;
    Vector3 currentMovement;

    [Header("Interactions")]
    [SerializeField] Vector3 interactionRaypoint = default;
    [SerializeField] float interactionRange = 50f;
    [SerializeField] LayerMask interactionLayer = default;
    Interactable currentInteractable;

    [Header("Jump Parameters")]
    [SerializeField] float jumpForce = 10f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.1f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField, Range(1, 5)] private float crouchSpeed = 2.5f;
    private bool isCrouching;
    private bool duringCrouchAnimation;
    private bool canCrouch = true;

    bool canMove = true;
    bool canSprint = true;
    bool canJump = true;
    bool canInteract = true;

    void Awake()
    {
        //footstepAudioSource = gameObject.GetComponent<AudioSource>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Start(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        if(canMove){
            HandleMovement();
        }
        if(canJump){
            HandleJump();
        }
        if(canInteract){
            HandleInteractionCheck();
            HandleInteractionInput();
        }
        ApplyFinalMovement();
    }
    void HandleMovement() {
        currentInput.x = Input.GetAxis("Vertical") * (isSprinting ? sprintSpeed : walkSpeed);
        currentInput.y = Input.GetAxis("Horizontal") * (isSprinting ? sprintSpeed : walkSpeed);

        float currentMovementY = currentMovement.y;
        currentMovement = (transform.forward * currentInput.x) + (transform.right * currentInput.y);
        currentMovement.y = currentMovementY;
    }

    void HandleJump() {
        if(controller.isGrounded && Input.GetKey(jumpKey)){
            Debug.Log("jump");
            currentMovement.y = jumpForce;
        }
    }

    void HandleInteractionCheck() {
        // Ray from the center of the viewport.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out RaycastHit hit, interactionRange)){
            //If interactable exists
            // Also, check new object is different from previous (In case they are close together)
            if(hit.collider.gameObject.layer == 9){
                if(currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()){
                    hit.collider.TryGetComponent<Interactable>(out currentInteractable);

                    if(currentInteractable){ //If currentInteractable exists
                        //currentInteractable.OnFocus();
                    }
                }
            }
        }
        else if(currentInteractable){
            //currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
    void HandleInteractionInput() {
        if(Input.GetKeyDown(interactKey) && (currentInteractable != null)) {
            if(Physics.Raycast(mainCamera.ViewportPointToRay(interactionRaypoint), out RaycastHit hit, interactionRange, interactionLayer)){
                //currentInteractable.OnInteract();
            }
        }
    }

    void ApplyFinalMovement(){
        if(!controller.isGrounded){
            //Apply gravity
            currentMovement.y -= gravityValue * Time.deltaTime;
            //Landing frame; reset y value to 0
            if(controller.velocity.y < -1 && controller.isGrounded){
                currentMovement.y = 0;
            }
        }
        controller.Move(currentMovement * Time.deltaTime);
    }

    //Not used in this game
    /*
    public void AttemptToCrouch()
    {
        if(!duringCrouchAnimation && controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(CrouchOrStand());
            }
        }
    }
    private IEnumerator CrouchOrStand()
    {
        duringCrouchAnimation = true;

        float timeElapsed = 0f;
        float currentHeight = controller.height;
        float targetHeight;
        if(isCrouching)
        {
            targetHeight = standingHeight;
            isCrouching = false;
        }
        else
        {
            targetHeight = crouchHeight;
            isCrouching = true;
        }

        while(timeElapsed > timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.height = targetHeight;

        duringCrouchAnimation = false;
    }
    */
}
