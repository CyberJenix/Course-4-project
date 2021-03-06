﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathTintScript : MonoBehaviour
{
    private PlayerControls controls;
    public PlayerStats pst;
    [SerializeField] Volume red;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.WorldActions.ReloadScene.performed += context => ReloadScene();
        
    }

    private void ReloadScene()
    {
          //Scene scene = SceneManager.GetActiveScene();
          //SceneManager.LoadScene(scene.buildIndex+0);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (pst.isDead == true)
        {
            red.enabled = true;
        }
    }
}
