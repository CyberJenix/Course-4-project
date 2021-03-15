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
        textHunger = GameObject.Find("FirstPersonPlayer/Canvas/timer_bg/Hunger").GetComponent<Text>();
        textThirst = GameObject.Find("FirstPersonPlayer/Canvas/timer_bg/Thirst").GetComponent<Text>();
        textAmmo = GameObject.Find("FirstPersonPlayer/Canvas/Ammo_bg/Ammo_count").GetComponent<Text>();
        ammoPanel = GameObject.Find("FirstPersonPlayer/Canvas/Ammo_bg");
        playerStats = GameObject.Find("FirstPersonPlayer/Canvas/status_bar");
        gunContainer = GameObject.Find("FirstPersonPlayer/Main Camera/GunContainer");
        //Debug.Log(gunContainer+" | "+gunContainer.transform.childCount);
    }

    // stat - текущее состояние хп/стамины | Для изменения статусбара
    private void updateBars(string valType, float stat) {
        for (int i = 5; i <= 100; i+=5) {
            //Debug.Log(i);
            if (i > stat) { playerStats.transform.Find(valType + "_bg/" + valType + "_container/" + valType + i.ToString()).gameObject.active=false; }
            else { playerStats.transform.Find(valType + "_bg/" + valType + "_container/" + valType + i.ToString()).gameObject.active = true; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        textHunger.text = Mathf.Round(ps.hunger).ToString();
        textThirst.text = Mathf.Round(ps.thirst).ToString();

        updateBars("hp", ps.health);
        //updateBars("stamina", ps.stamina);

        //Debug.Log(gunContainer+" | "+gunContainer.transform.childCount);
        if (gunContainer.transform.childCount > 0)
        {
            if (!ammoPanel.active) { ammoPanel.active = true; }
            textAmmo.text = gunContainer.transform.GetChild(0).gameObject.GetComponent<Projectile_weapon>().bulletsLeft.ToString();
        }
        else {
            ammoPanel.active = false;
        }
    }
}
