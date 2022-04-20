using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] private AudioClip[] spawnSounds;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySpawnSound());
    }

    IEnumerator DelaySpawnSound()
    {
        yield return new WaitForSeconds(2.0f);
        int index = Random.Range(0,2);
        audioSource.PlayOneShot(spawnSounds[index]);
    }

}
