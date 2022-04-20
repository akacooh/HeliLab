using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private float zBound = 8;
    private float xBound = 20;

    // Update is called once per frame
    void LateUpdate()
    {
        //keep center of camera at max of x/zBound from heli
        if (transform.position.x - playerTransform.position.x > xBound)
        {
            transform.position = new Vector3(xBound + playerTransform.position.x,transform.position.y, transform.position.z);
        }
        if (transform.position.x - playerTransform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound + playerTransform.position.x,transform.position.y, transform.position.z);
        }  

        if (transform.position.z - playerTransform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, playerTransform.position.z + zBound);
        }
        if (transform.position.z - playerTransform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, playerTransform.position.z - zBound);
        }         
    }
    
}
