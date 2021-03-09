using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdsDisablerForDropping : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private PickUpAndDrop equipScript;
    [SerializeField] private AimDownSights aimingScript;

    private void OnEnable()
    {
        
        controls = new PlayerControls();
        controls.Enable();
        controls.WorldActions.Interact.performed += context => Pickup();
        controls.WorldActions.Drop.performed += context => DisableAds();
    }

    void Start()
    {

    }

    private void Pickup()
    {
        Invoke("EnableAds", 0.1f);
    }

    private void EnableAds()
    {
        if (equipScript.equipped == true)
        {
            Debug.Log("i'm pressed");
            aimingScript.enabled = true;
        }
    }

    private void DisableAds()
    {
        if (equipScript.equipped == true)
        {
            aimingScript.enabled = false;
        }
    }
}
