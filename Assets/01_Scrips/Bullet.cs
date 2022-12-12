using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float timeToDestroy = 3;
    public AudioClip explosionSound;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            Enemy d = other.gameObject.GetComponent<Enemy>();
            d.TakeDamageB();
            Destroy(gameObject);

        }
    }

    /*void Destroyed()
    {
        //explosionAS.PlayOneShot(explosionSound);
        
        //GameManager.instance.PlaySFX(explosionSound);
        Destroy(gameObject);
    }*/
   
}
