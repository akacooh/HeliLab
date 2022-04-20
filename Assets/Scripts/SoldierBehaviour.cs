using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem dirt;
    [SerializeField] private ParticleSystem shot;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shotSound;

    private GameObject player;
    public GameObject bulletPrefab;
    public float shootDistance;
    public float speed;
    private bool shootCD = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceV = player.transform.position - transform.position;
        if (distanceV.magnitude > shootDistance)
        {
            transform.Translate(distanceV.normalized * speed * Time.deltaTime, Space.World);
            if (!dirt.isPlaying)
            {
                dirt.Play();
            }
        }
        else 
        {
            if (dirt.isPlaying)
            {
                dirt.Stop();
            }
            if (!shootCD)
            {
                Shoot();
                shootCD = true;
                StartCoroutine(ShootCooldown());
            }
        }
        
        transform.LookAt(player.transform);
    }

    void Shoot()
    {
        shot.Play();
        audioSource.PlayOneShot(shotSound);
        Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        shootCD = false;
    }
}
