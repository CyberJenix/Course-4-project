using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverItem : Projectile_weapon
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PickUpAndDrop equipScript;
    [SerializeField] private float heal,food,water,score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    new public void Input()
    {
        if (magazineSize > 0)
        {
         playerStats.Heal(heal);
         playerStats.Eat(food);
         playerStats.Drink(water);
         playerStats.AddScore(score);
         magazineSize--;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (equipScript.equipped = true)
        {
            controls.WorldActions.Shoot.performed += context => Input();
        }
    }
}
