using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpAndDrop : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private Projectile_weapon gun_script;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private Transform player, gunContainer, camera;
    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce, dropUpwardForce;
    [SerializeField] public bool equipped;
    [SerializeField] private static bool slotFull; //static - changind this in one script will change in all otherscripts
    [SerializeField] Vector3 distanceToPlayer, drop_velocity;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.WorldActions.Enable();
        controls.WorldActions.Interact.performed += context => PickUp();
        controls.WorldActions.Drop.performed += context => Drop();

        if (!equipped)
        {
            gun_script.enabled = false;
            rigidBody.isKinematic = false;
            collider.isTrigger = false;
        }
        if (equipped)
        {
            gun_script.enabled = true;
            rigidBody.isKinematic = true;
            collider.isTrigger = true;
            slotFull = true;
        }
    }

    private void PickUp()
    {
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && !slotFull)
        {
            equipped = true;
            slotFull = true; //So u cant pick up any other gun

            //Make weapon a child of the camera and move it to default position
            transform.SetParent(gunContainer);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;

            //Make Rigidbody kinematic and BoxCollider a trigger
            rigidBody.isKinematic = true;
            collider.isTrigger = true;

            //Enable script
            gun_script.enabled = true;
        }
    }

    private void Drop()
    {
        if (equipped )
        {

            equipped = false;
            slotFull = false;

           
            transform.SetParent(null); //Set parent to null

            
            rigidBody.isKinematic = false;//Make Rigidbody not kinematic and BoxCollider normal
            collider.isTrigger = false;

            //AddForce
            rigidBody.AddForce(camera.forward * dropForwardForce, ForceMode.Impulse);
            rigidBody.AddForce(camera.up * dropUpwardForce, ForceMode.Impulse);
            //Add random rotation
             float random = Random.Range(-1f, 1f);
             rigidBody.AddTorque(new Vector3(random, random, random) * 10);

            //Disable script
            gun_script.enabled = false;
        }
    }



    // Update is called once per frame
    void LateUpdate()
    {
        distanceToPlayer = player.position - transform.position; // calculate distance to player
    }
}
