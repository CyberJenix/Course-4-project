using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Look : MonoBehaviour
{

    private PlayerControls controls;

    [SerializeField] [Range(0.0f, 10.0f)] private float mouseSensitivity = 3.5f;

    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private Vector2 look; //control direction we are looking 

    private float xRotation;

    private Transform playerBody;



    private void Awake()
    {
        controls = new PlayerControls();

        Cursor.lockState = CursorLockMode.Locked;

        playerBody = transform.parent; 
    }



    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMovement();
    }

    void UpdateMouseMovement()
    {
        look = controls.WorldActions.Look.ReadValue<Vector2>();

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, look, ref currentMouseDeltaVelocity, mouseSmoothTime);  //smoothing from currentMouseDelta to *look* value 

        float MouseX = currentMouseDelta.x * mouseSensitivity * Time.deltaTime; // * deltatime so speed does not increase with high fps 
        float MouseY = currentMouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); //so you dont rotate your head backwards

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * MouseX);
    }
}
