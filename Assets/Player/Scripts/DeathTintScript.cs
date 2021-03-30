﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class DeathTintScript : MonoBehaviour
{
    private PlayerControls controls;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.WorldActions.ReloadScene.performed += context => ReloadScene();
    }

    private void ReloadScene()
    {
        // Scene scene = SceneManager.GetActiveScene();
        //  SceneManager.LoadScene(scene.buildIndex+0);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}