using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed;
    public Vector3 endPos;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if (transform.position.x > endPos.x)
        {
            Destroy(gameObject);
        }
    }
    
}
