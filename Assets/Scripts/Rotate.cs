using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 rotationMask;

    // Update is called once per frame
    void Update()
    {
        float x = rotationSpeed * Time.deltaTime * rotationMask.x;
        float y = rotationSpeed * Time.deltaTime * rotationMask.y;
        float z = rotationSpeed * Time.deltaTime * rotationMask.z;
        transform.Rotate(x, y, z);
    }

}
