using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public UnityEvent GameEnded;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] savedSounds;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip rocketSound;
    [SerializeField] private ParticleSystem explosion;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject detector;

    [SerializeField] private GameObject model;
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform mainRotor;
    [SerializeField] private Image rescueBar;

    private Vector3 allyPosition;
    private float horizontalInput;
    private float verticalInput;
    private float strafeInput;
    public float force;
    public float turnSpeed;
    public float savingProgress = 0;
    public int savedAllies = 0;
    private bool canShoot = true;
    public bool gameOver;

    // Start is called before the first frame update
    private void Awake() {
        spawnManager.AllySpawned += SetupDetector;        
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && !gameOver)
        {
            canShoot = false;
            //move rocket up a little
            Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z); 
            Instantiate(rocket, pos, transform.rotation);
            audioSource.PlayOneShot(rocketSound, 0.3f);
            StartCoroutine(RocketCD());
        }

        detector.transform.position = transform.position;
        if (detector.activeInHierarchy)
        {
            detector.transform.LookAt(allyPosition);
        }

    }

    IEnumerator RocketCD()
    {
        yield return new WaitForSeconds(1.5f);
        canShoot = true;

    }
    private void FixedUpdate() 
    {
        //W,S for moving forward; A,D for rotating; Q,E for strafe
        horizontalInput = Input.GetAxis("Horizontal");
        strafeInput = Input.GetAxis("StrafeAxis");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(strafeInput, 0, verticalInput);
        playerRb.AddRelativeForce(direction * force);

        //apply rotation to model while moving
        model.transform.localEulerAngles = new Vector3(verticalInput * 15, 0, -strafeInput * 15);

        //compensate for parent rotation
        float prevY = transform.localEulerAngles.y;
        transform.Rotate(0f, turnSpeed * horizontalInput, 0f);
        float curY = transform.localEulerAngles.y;
        mainRotor.localEulerAngles = new Vector3(mainRotor.localEulerAngles.x, mainRotor.localEulerAngles.y - (curY - prevY), mainRotor.localEulerAngles.z);
    }

    private void OnTriggerStay(Collider other) 
    {
        //Need to stay for 5 sec over Ally to capture it
        if (other.CompareTag("Ally"))
        {
            savingProgress += Time.deltaTime;
            //rescueBar.fillAmount = savingProgress / 5;
            if (savingProgress >= 5) 
            {
                savedAllies++;
                Destroy(other.transform.gameObject);
                savingProgress = 0;
                rescueBar.fillAmount = 0;
                detector.SetActive(false);
                spawnManager.SpawnAlly();

                int index = Random.Range(0,2);
                audioSource.PlayOneShot(savedSounds[index]);
            }
            else 
            {
                rescueBar.fillAmount = savingProgress / 5;
            }
        }
    }

   private void OnTriggerExit(Collider other) 
   {
       if (other.CompareTag("Ally"))
       {
           savingProgress = 0;
           rescueBar.fillAmount = 0;
       }
   }

   private void OnTriggerEnter(Collider other) 
   {
       if (other.CompareTag("Bullet"))
       {
           //Destroy(gameObject);
           Destroy(other.gameObject);
           gameOver = true;
           explosion.Play();
           audioSource.PlayOneShot(explosionSound);
           StartCoroutine(DelayGameOver());
       }
   }

   private void LateUpdate() {
       rescueBar.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
   }

   private void SetupDetector(Vector3 position)
   {
       allyPosition = position;
       detector.SetActive(true);
   }

   IEnumerator DelayGameOver()
   { 
        yield return new WaitForSeconds(1.5f);
        GameEnded.Invoke();  
   }
}
