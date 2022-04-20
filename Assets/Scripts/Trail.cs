using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private GameObject ground;
    [SerializeField] private GameObject planePrefab;
    private Vector3 startPos;
    private Vector3 endPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindWithTag("Ground");
        //getting 2 random points from the edge of the surface to fly through
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        float startPosZ = Random.Range(groundRenderer.bounds.min.z, groundRenderer.bounds.max.z);
        startPos = new Vector3 (groundRenderer.bounds.min.x - 20, 0, startPosZ);
        float endPosZ = Random.Range(groundRenderer.bounds.min.z, groundRenderer.bounds.max.z);
        endPos = new Vector3 (groundRenderer.bounds.max.x + 20, 0, endPosZ);

        transform.position = startPos;
        transform.LookAt(endPos);

        StartCoroutine(SpawnPlane());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if (transform.position.x > endPos.x + 50)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnPlane()
    {       
        yield return new WaitForSeconds(3.0f);
        var plane = Instantiate(planePrefab, startPos, transform.rotation);
        Plane planeScript = plane.GetComponent<Plane>();
        planeScript.speed = speed;
        planeScript.endPos = endPos; 
    }
}
