using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private int health;

    Player player;
    Animator animator;
    [SerializeField] private AudioClip explosionSound;
    private AudioSource audioSource;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject lasersPref;
    [SerializeField] private float fireRate = 3.0f;
    private float canFire = -1f;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if(player == null )
        {
            Debug.Log("Player is NULL!");
        }

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = explosionSound;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Movement();
        Shooting();
    }

    void Shooting()
    {
        if(Time.time > canFire )
        {
            fireRate = UnityEngine.Random.Range(5f, 7f);
            canFire = Time.time + fireRate;
            Instantiate(lasersPref, transform.position, Quaternion.identity);
        }
    }

    void Movement()
    {
        // per second go down
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        //if enemy go to bottom screen respawn of top

        float randXpos = UnityEngine.Random.Range(-9.0f, 9.0f); //random bir x pozisyonun doðmasýný istiyoruz. deðiþken 

        if (transform.position.y < -5.3f)
        {
            transform.position = new Vector3(randXpos, 6.35f, 0); // random x pozisyonunda yyi geçince respawn olacaktýr!

        }

    }

    private bool isDestroyed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed) return; // Düþman zaten yok edildiyse tetiklemeleri yok say

        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().TakeDamage();
            enemySpeed = 0;
            Destroy(this.gameObject, 0.7f);
            audioSource.Play();
            animator.SetTrigger("enemyDestroyed");
            isDestroyed = true; // Düþmanýn yok edildiðini belirtmek için bayrak deðerini ayarla
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // Mermiyi yok et

            if (!isDestroyed && player != null) // Düþman yok edilmediyse kontrol et
            {
                player.AddScore(10);
            }

            Destroy(this.gameObject, 0.7f); // Düþmaný yok et
            audioSource.Play();
            enemySpeed = 0;
            animator.SetTrigger("enemyDestroyed");
            isDestroyed = true; // Düþmanýn yok edildiðini belirtmek için bayrak deðerini ayarla
        }
    }
}
