using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinmapCamera : MonoBehaviour
{
    Quaternion quaternion;
    private void Start()
    {
         quaternion = transform.rotation;
         
    }
    void Update()
    {

        transform.rotation =quaternion;
       
    }

 
}
