using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{

    private PlayerControls controls;


    private CharacterController characterController;

    [SerializeField] private Camera camera;

    [SerializeField] [Range(0.0f, 10.0f)] private float cameraOffset = 0.514f;

    private Vector2 move; //to represent movementsame as look  


    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector3 verticalVelocity;

    private float height;
    private float originalHeight;
    [SerializeField] private float crouchHeight= 0.9f;
    [SerializeField] private float proneHeight = 0.9f;
    [SerializeField] private float targetHeight;
    [SerializeField] private float movementSpeed =1f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float proneSpeed = 1f;
    [SerializeField] private float timeToCrouch = 0.9f;
    [SerializeField] private float timeToProne = 0.9f;

    [SerializeField] private float gravity = -9.81f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    [SerializeField] private float jumpHeight = 3f;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] bool isGrounded;
    [SerializeField] bool isCrouching;
    [SerializeField] bool isProne;

    public float cameraHeight; //debug delete later

    [SerializeField] private float camTransformDivider=2f;

    private void Awake()
    {
        controls = new PlayerControls();

        characterController = GetComponent<CharacterController>();

        controls.WorldActions.Jump.performed += context => Jump();

        controls.WorldActions.Crouch.performed += context => Crouch();

        controls.WorldActions.Prone.performed += context => Prone();

        controls.WorldActions.Sprint.started += context => Sprint(true);

        controls.WorldActions.Sprint.canceled += context => Sprint(false);

       
    }

    // Start is called before the first frame update
    void Start()
    {
        height = characterController.height;
        targetHeight = height;
        originalHeight = height;
    }

    void Jump()
    {
        if (isGrounded && !isCrouching && !isProne)  //So player cannot jump from crouching/prone position, to allow it leave only if statement and remove !isCrouching and !isProne from if
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
        else if (isCrouching) // Call Crouch function which will make character stand up instead of jumping when pressing jump button while crouching
        {
            Crouch(); 
        }
        else if (isProne) // Call Prone function which will make character stand up instead of jumping when pressing jump button while crouching
        {
            Prone();
        }
    }

    void Sprint(bool check)
    {
        if (check && isGrounded && !isCrouching && !isProne) // doesn't allow to sprint in air and if crouching, to allow sprint in air remove isGrounded, to allow sprint from crouching position remove !isCrouching
        {
            movementSpeed = runSpeed;
        }
        else if (isCrouching) //remove if character will sprint while crouching
        {
            movementSpeed = crouchSpeed;
        }
        else if (isProne) //remove if character will sprint while prone
        {
            movementSpeed = proneSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

    }

    void Crouch()
    {
            if (!isCrouching)
            {
                isCrouching = true;
                isProne = false;
                targetHeight = crouchHeight;
                camTransformDivider = 5;
                movementSpeed = crouchSpeed;

            }
            else
            {
                if (!Physics.Raycast(transform.position, Vector3.up * originalHeight / 2, 2f)) //Raycast, cast ray of characters standing position height to check if there is obstacles and allow standing up if there is not
                {
                    isCrouching = false;
                    targetHeight = height;
                    camTransformDivider = 2;
                    movementSpeed = walkSpeed;
                }
             
            }
         
    }

    void Prone() //Character controller is restricted to world space Y axis so rotation on other axis is impossible
        {
        if (!isProne)
        {
            isProne = true;
            isCrouching = false;
            targetHeight = proneHeight;
            camTransformDivider = -2;
            movementSpeed = proneSpeed;

        }
        else
        {
            if (!Physics.Raycast(transform.position, Vector3.up * originalHeight / 2, 2f)) //Raycast, cast ray of characters standing position height to check if there is obstacles and allow standing up if there is not
            {
                isProne = false;
                targetHeight = height;
                camTransformDivider = 2;
                movementSpeed = walkSpeed;
            }

        }
    }

   /* void UpdateProne() // same as crocuh for now but kept separate for further customisation/development
    {
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, timeToProne * Time.deltaTime);
        characterController.center = Vector3.down * (originalHeight - characterController.height) / 2.0f;

        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, characterController.transform.position.y + targetHeight / camTransformDivider - cameraOffset, camera.transform.position.z), timeToProne * Time.deltaTime);
    }*/


    void UpdateCrouch() //Works for crouch and prone alike, to make scripts different uncomment prone function above
    {
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, timeToCrouch * Time.deltaTime);
        characterController.center = Vector3.down * (originalHeight - characterController.height) / 2.0f;

        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, characterController.transform.position.y + targetHeight /camTransformDivider -cameraOffset , camera.transform.position.z), timeToCrouch * Time.deltaTime);

        //You need to adjust CharacterController.center as well when you adjust height (set it to half height + any foot offset you might be using)
        //https://gamedev.stackexchange.com/questions/159781/changing-charactercontrollers-height-causes-jittering-issue-in-unity
        //https://www.youtube.com/watch?v=Wepj2-K5l_g&ab_channel=DitzelGames
         
    //Prone variant just scale character on x or y axis while changing height
    }

    void UpdateMovement()
    {

        Debug.DrawRay(transform.position, Vector3.up * originalHeight/2, Color.red, 2f);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // create sphere based on groundcheck position, use grounddistance as radius ground mask as layer mask 

        if (isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f; // because checkSphere registers earlier than player is grounded 

        }

        move = controls.WorldActions.Move.ReadValue<Vector2>();
        move.Normalize(); //So diagonal movement has the same speed as other directions

        currentDir = Vector2.SmoothDamp(currentDir, move, ref currentDirVelocity, moveSmoothTime); //smoothing from currentDir to *move* value 

        Vector3 velocity = (currentDir.y * transform.forward + currentDir.x * transform.right)* movementSpeed;

        characterController.Move(velocity  * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;

        characterController.Move(verticalVelocity * Time.deltaTime);

        if (!isGrounded && characterController.collisionFlags == CollisionFlags.Above)
        {
            verticalVelocity.y = -verticalVelocity.y;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateCrouch();
    }


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
