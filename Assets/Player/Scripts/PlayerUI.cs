using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    //from PlayerStats
    private Text textHunger, textThirst, textAmmo;
    private PlayerStats ps;
    private GameObject gunContainer, ammoPanel, playerStats;

    void Start()
    {
        ps = gameObject.GetComponent<PlayerStats>();
        textAmmo = GameObject.Find("FirstPersonPlayer/Canvas/Ammo_bg/Ammo_count").GetComponent<Text>();
        ammoPanel = GameObject.Find("FirstPersonPlayer/Canvas/Ammo_bg");
        playerStats = GameObject.Find("FirstPersonPlayer/Canvas/status_bar");
        gunContainer = GameObject.Find("FirstPersonPlayer/Main Camera/GunContainer");
        //Debug.Log(gunContainer+" | "+gunContainer.transform.childCount);
    }

    // stat - текущее состояние хп/стамины | Для изменения статусбара
    private void updateBars(string contType, string valType, float stat) {
        for (int i = 5; i <= 100; i+=5) {
            if (i > stat) { playerStats.transform.Find(contType + "/" + valType + "_bg/" + valType + "_container/" + valType + i.ToString()).gameObject.active = false; }
            else { playerStats.transform.Find(contType + "/" + valType + "_bg/" + valType + "_container/" + valType + i.ToString()).gameObject.active = true; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateBars("hpAndStamina", "hp", ps.health);
        //updateBars("hpAndStamina", "stamina", ps.stamina);
        updateBars("hungerAndThirst", "hunger", ps.hunger);
        updateBars("hungerAndThirst", "thirst", ps.thirst);

        //Debug.Log(gunContainer+" | "+gunContainer.transform.childCount);
        if (gunContainer.transform.childCount > 0)
        {
            if (!ammoPanel.active) { ammoPanel.active = true; }
            textAmmo.text = gunContainer.transform.GetChild(0).gameObject.GetComponent<Projectile_weapon>().bulletsLeft.ToString() + "/" + gunContainer.transform.GetChild(0).gameObject.GetComponent<Projectile_weapon>().magazineSize.ToString();
        }
        else {
            ammoPanel.active = false;
        }
    }
}
