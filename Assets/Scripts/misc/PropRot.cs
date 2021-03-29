using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRot : MonoBehaviour
{

    //[SerializeField] private float angularSpeed;
    //[SerializeField] private bool CCW;
    [SerializeField] private Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(rotation * Time.deltaTime, Space.Self);
    }
}
