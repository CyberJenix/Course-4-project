using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverItem : Projectile_weapon
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PickUpAndDrop equipScript;
    [SerializeField] private float heal,food,water;

    // Start is called before the first frame update
    void Start()
    {
        controls.WorldActions.Shoot.performed += context => Input();
    }

    new private void Input()
    {
        if (magazineSize > 0)
        {
         playerStats.Heal(heal);
         playerStats.Eat(food);
         playerStats.Drink(water);
         magazineSize--;

        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
