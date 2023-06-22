using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    void Update()
    {
       MoveUp();
    }


    void MoveUp()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 2f);
        //triple shoot power ups parent destroy
        Destroy(transform.parent.gameObject, 2f);
    }
}
