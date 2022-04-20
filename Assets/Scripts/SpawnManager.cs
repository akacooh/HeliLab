using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefub;
    public GameObject allyPrefub;
    public GameObject planeTrail;
    public UnityAction<Vector3> AllySpawned;
    private float spawnBoundary = 90.0f;

    // Start is called before the first frame update
    void Start()
    {

        SpawnAlly();

        InvokeRepeating("SpawnEnemy", 5.0f, 6.0f);
        InvokeRepeating("SpawnPlane", 10.0f, 10.0f);
    }

    private Vector3 GetRandomPosition()
    {
        float rndX = Random.Range(-spawnBoundary, spawnBoundary);
        float rndZ = Random.Range(-spawnBoundary, spawnBoundary);
        return new Vector3(rndX, 0, rndZ);
    }

    public void SpawnAlly()
    {
        Vector3 position = GetRandomPosition();
        Instantiate(allyPrefub, position, allyPrefub.transform.rotation);
        AllySpawned.Invoke(position);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefub, GetRandomPosition(), enemyPrefub.transform.rotation); 
    }

    private void SpawnPlane()
    {
        Instantiate(planeTrail); 
    }
}
