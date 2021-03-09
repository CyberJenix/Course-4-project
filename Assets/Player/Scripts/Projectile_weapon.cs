using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile_weapon : MonoBehaviour
{

    protected PlayerControls controls;

    //bullet
    [SerializeField] private GameObject bullet;

    //bullet force
    [SerializeField] private float shootForce, upwardForce;

    //Weapon stats
    [SerializeField] private float fireRate, spread, reloadTime, timeBetweenShots;
    [SerializeField] public int magazineSize;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private bool allowButtonHold;

    private int bulletsLeft, bulletsShot;

    //bool checks
    [SerializeField] private bool shooting, readyToShoot, reloading;

    //References
    [SerializeField] private Camera camera;
    [SerializeField] private Transform attackPoint;

    //bug fixing
    public bool allowInvoke = true;


    

    private void Awake()
    {
        controls = new PlayerControls();
        controls.WorldActions.Enable();
        //full magazine

        bulletsLeft = magazineSize;

        readyToShoot = true;
    }


    private void Input()
    {

        

        //Check if allowed to hold fire button and take corresponding input
        if (allowButtonHold)
        {
            controls.WorldActions.Shoot.started +=
                  context =>
                  {
                      shooting = true;
                  };

            controls.WorldActions.Shoot.canceled +=
                 context =>
                 {
                     shooting = false;
                 };
        }

        else
        {
            controls.WorldActions.Shoot.performed +=
                context =>
                {
                    shooting = true;

                    Invoke("StopShooting", timeBetweenShots);
                };

        }

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }

        //reloading
        if (bulletsLeft < magazineSize && !reloading)
        {
           controls.WorldActions.Reload.performed += context => Reload();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find hit position using raycast
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //just a far away point

        //Calculate direction fromm attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate projectile to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(camera.transform.up.normalized * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }

        //if more then one projectiles per tap
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void StopShooting()
    {
        //Allow shooting and invoking again
        shooting = false;
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    // Update is called once per frame
    void Update()
    {
        Input();
    }
}
