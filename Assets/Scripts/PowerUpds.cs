using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpds : MonoBehaviour
{
    [SerializeField] private float downSpeed = 3f;  // ÜSTTEN DÜÞME HIZI

    [SerializeField] private int powerUpsID;  // 0-TripleShot 1-SpeedUP 2-Shield
    [SerializeField] private AudioClip powerUpSound;
    void Start()
    {

    }

    void Update()
    {
        //move down speed 
        //when vwe leave the screen destroy this obj
        transform.Translate(Vector3.down * downSpeed *Time.deltaTime);

        //we cant catch up okey destroyyy
        if(transform.position.y < -4.7f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTrigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //only be collectiable by Player
        if(other.tag == "Player")
        {
            //communicate player sc
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(powerUpSound, transform.position);  // ses efektini obje yok olsa bile çal
            // bunun için null check yapmalýyýz no Crash :D
            if(player != null)
            {
               //check power up
                switch(powerUpsID)
                {
                    case 0:
                        player.TripleShootActive();
                        break;
                    case 1:
                        player.SpeedUpActive(); 
                        break;
                    case 2:
                        player.ShiledActive();
                        break;
                }
            }
            Destroy(this.gameObject);

        }
    }

    //on collected destroy
}
