using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float timeToDestroy = 10;
    public AudioClip explosionSound;

    void Start()
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.ActivatePower(type);
            Destroy(gameObject);
        }
    }
}

public enum PowerUpType
{
    Bullets,
    Shield,
    DoubleDamage,
    BulletSpeed,
    Life

}
