using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimDownSights : MonoBehaviour

{
    private PlayerControls controls;
    [SerializeField] private Vector3 adsCoords;
    [SerializeField] private bool aiming= false;
    [SerializeField] private float adsSpeed;
    private Vector3 coordsHolder;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.WorldActions.Aim.performed += context => Aim();
    }

    private void Aim()
    {
        if (!aiming)
        {
            coordsHolder = adsCoords;
            aiming = true;
        }
        else if (aiming)
        {
            coordsHolder = new Vector3(0, 0, 0);
            aiming = false;
        }

        
    }

    private void UpdateAim()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, coordsHolder, adsSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAim();
    }
}
