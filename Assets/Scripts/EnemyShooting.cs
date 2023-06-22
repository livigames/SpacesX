using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.Log("Player is NULL!");
        }
    }

    void Update()
    {
        MoveDown();
    }


    void MoveDown()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 2.5f);
        //triple shoot power ups parent destroy
        Destroy(transform.parent.gameObject, 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
