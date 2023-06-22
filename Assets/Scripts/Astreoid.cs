using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astreoid : MonoBehaviour
{
    [SerializeField] private float astRotateSpeed;
    [SerializeField] private GameObject astExplosionVFX;

    SpawnManager spawnManager;


    void Start()
    {
        float randomPosX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomPosX, 5, 0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(0, 0, astRotateSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            Instantiate(astExplosionVFX, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager.StartSpawn();
            Destroy(this.gameObject, 0.2f);
            
        }
    }
}
