using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletE : MonoBehaviour
{
    public float speed = 10;
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
        if (other.gameObject.CompareTag("Player"))
        {

            Player d = other.gameObject.GetComponent<Player>();
            d.TakeDamageBE();
            Destroy(gameObject);

        }
    }
}
