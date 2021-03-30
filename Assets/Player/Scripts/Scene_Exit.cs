using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Scene_Exit : MonoBehaviour

{

    [SerializeField] ScoreTable score_table;
    [SerializeField] PlayerStats playerstats;
    [SerializeField] Volume red;
    [SerializeField] Volume green;
    Volume volume;

    // Start is called before the first frame update
    void Start()
    {
        DisableScoreTable();
    }

    private void DisableScoreTable()
    {
        if (score_table.enabled == true)
        {
            score_table.enabled = false;
        }
    }

    private void EnableScoreTable()
    {
        if (score_table.enabled == false)
        {
            score_table.enabled = true;

        }
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (playerstats.score >= 100)
        {
       
            EnableScoreTable();

        }
        else
        {


        }
            
    }
        // Update is called once per frame
        void Update()
        {
        if (playerstats.score >= 100)
        {

           green.enabled = true;
           red.enabled = false;

        }
        else if (playerstats.score < 100)
        {

              red.enabled = true;
              green.enabled = false;
        }

    }

}
