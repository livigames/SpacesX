using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float speedMultipler = 1.5f;
    [SerializeField] private int score;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject tripleShootPref;
    [SerializeField] private float fireRate = 0.5f; // cooldown beetween shooting
    [SerializeField] private int health = 3;

    private float canFire = 0f;
    private bool isTripleShootActive = false;
    private bool isSpeedUpActive = false;
    private bool isShiledActive = false;

    [SerializeField] private GameObject thrusterVizualizer;
    [SerializeField] private GameObject shieldVizualizer;
    [SerializeField] private GameObject hurtRight;
    [SerializeField] private GameObject hurtLeft;

    private SpawnManager spawnManager;
    private UIManager uiManager;
    private Animator anim;

    [SerializeField] private AudioClip laserSound;
    private AudioSource audioSource;

    Rigidbody rb;

    void Start()
    {
        // her baþlancýçta player pos resetlensin
        transform.position = new Vector3(0, -2.6f, 0);
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();  // Gameofbecj.Find object tagle bulmaya yarar    
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();

        if(spawnManager == null )
        {
            Debug.Log("spawnmanager is null");
        }

        if(uiManager == null )
        {
            Debug.Log("UImanager is null");
        }

        audioSource.clip = laserSound;
    }

    void Companents()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckInputs();
    }

    private void FixedUpdate()
    {
        Move();
        PlayerBounds();
        Shooting();
    }

    void CheckInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        //thruster effect
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            thrusterVizualizer.SetActive(true);
        }else
        {
            thrusterVizualizer.SetActive(false);
        }

    }

    // oyuncu sýnýrlarý
    void PlayerBounds()
    {
       
        //if oyucu pozisyonu -3.8 den küçükse pos.y -3.8fe eþitle ki aþþaðýsýna gidemesin, if oyuncu pozisyonu y büyüktür 0 dan oyuncu hareket edemez. 0dan yukarý gitmesinbi istemiyoz
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0); // Mathf.Clamp ile 

       
        //if oyuncu posizyonu x büyük veya eþittir 9 ise oyuncu pozisyonunu eþitle ki daha fazla gidemesin
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9f, 9f), transform.position.y, 0);
    }

    void Shooting()
    {
        if(Input.GetMouseButton(0) && Time.time > canFire)  //Time.time oyundaki zamandýr, oyundaki geçen zaman her fire rateden büyük oldugu zaman ateþ etmeye izin ver.
        {
            canFire = Time.time + fireRate;  // next fire'ý her geçen süre boyunca fireRate kadar arttýr.

            //tripleShoot active ise onun prefabýný kullan deðilse tek ateþ et
            if(isTripleShootActive == true)
            {
                Instantiate(tripleShootPref, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet, shootPoint.position, Quaternion.identity);
            }
            audioSource.Play();
        }
    }

    public void TakeDamage()
    {
        //shiledactive? okey return
        if(isShiledActive == true)
        {
            isShiledActive = false;
            shieldVizualizer.SetActive(false);
            return;
        }

        
        health -= 1;
        
        if(health == 2)
        {
            hurtLeft.SetActive(true);
        }else if(health == 1)
        {
            hurtRight.SetActive(true);
        }

        uiManager.UpdateLives(health);

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        spawnManager.StopSpawning();
        Destroy(this.gameObject);
       
    }
    //tiprle shoot skills
    public void TripleShootActive()
    {
        isTripleShootActive = true;
        //start coroutine
        StartCoroutine(TripleShootRoutine());
    }
    //triple shoot skilss using cooldown
    IEnumerator TripleShootRoutine()
    {
        //wait 5 sec
        //set the tripleshot false
        yield return new WaitForSeconds(5.0f);
        isTripleShootActive = false; 
    }

    //speed up skills
    public void SpeedUpActive()
    {
        isSpeedUpActive = true;
        //speedi arttýr
        speed *= speedMultipler;
        StartCoroutine(SpeedUpRoutine());
    }
    //5sec for speed up skill
    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedUpActive = false;
        speed /= speedMultipler;
    }

    public void ShiledActive()
    {
        isShiledActive = true;
        shieldVizualizer.SetActive(true);
        StartCoroutine(ShiledRoutine());
    }

    IEnumerator ShiledRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isShiledActive = false;
        shieldVizualizer.SetActive(false);
    }

    //method for score
    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
        uiManager.CheckForBestScore();
    }
    //communicate for update score
}
