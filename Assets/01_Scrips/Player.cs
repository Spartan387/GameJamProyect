using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 4;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask groundLayer;
    public AudioClip deadplayer;
    public AudioClip shoot;
    float x;
    public float jumpForce = 7;
    public bool canJump = false;
    public Transform feet;
    public Vector2 size = new Vector2(1, 0.5f);
    public Transform firePoint;
    public float timer = 0;
    public float timeBtwAttack = 2;
    public bool canShoot = true;
    public GameObject bulletPrefab;
    public float timerS = 5;
    public float timeOfS= 0;
    private bool activo;
    public int hp = 3;
    public Text hpText;
    public GameObject PULifePanel;
    public Text timePULifeText;
    public float timerTA = 5;
    public float timeOfTA = 0;
    public GameObject PUTimeAttackPanel;
    public Text PUTimeAttackPanelText;
    

    void Start()
    {
        hpText.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfCanShoot();
        Shoot();
        Bend();
        CheckTimePowerUps();
    }
    void Move()
    {
        x = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(x));
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        if (x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void Jump()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(feet.position, size, 0, groundLayer);
        if (colliders.Length > 0)
        {
            //anim.SetBool("Jumping", false);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        else
        {
           
            //anim.SetBool("Jumping", true);
            //canJump = false;
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //GameManager.instance.PlaySFX(shoot);
                Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                canShoot = false;
                //anim.SetTrigger("Shoot");
            }
        }
    }
    void CheckIfCanShoot()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBtwAttack)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();

        }
    }
    public void TakeDamage()
    {
        Debug.Log("Daño Recibido");
        hp--;
        hpText.text = hp.ToString();
        if (hp <= 0)
        {
            GameManager.instance.PlaySFX(deadplayer);
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");

        }
    }
    public void TakeDamageBE()
    {
        Debug.Log("Daño Recibido");
        hp--;
        hpText.text = hp.ToString();
        if (hp <= 0)
        {
            GameManager.instance.PlaySFX(deadplayer);
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");

        }
    }
    void Bend()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            activo = !activo;

        }
        if (activo == true)
        {
            anim.SetBool("CanW", true);
        }
        if (activo == false)   
        {
            anim.SetBool("CanW", false); 
        }
    }
    public void ActivatePower(PowerUpType power)
    {
        Debug.Log("poder " + power.ToString());
        switch (power)
        {
            case PowerUpType.Bullets:
                //bulletsCount += 5;
                //bulletsTxt.text = bulletsCount.ToString();
                break;
            case PowerUpType.DoubleDamage:
                break;
            case PowerUpType.BulletSpeed:
                BulletSpeed();
                break;
            case PowerUpType.Shield:
                ShootSpeed();
                break;
            case PowerUpType.Life:
                PowerUpLife();
                break;
        }
       
    }
    void PowerUpLife()
    {
        hp++;
        hpText.text = hp.ToString();
    }
    void BulletSpeed()
    {
        speed = 20;
    }
    void ShootSpeed() 
    {
        timeBtwAttack = 1;
    }

    void CheckTimePowerUps()
    {
        if (speed == 20)
        {
            PULifePanel.SetActive(true);
            timerS -= Time.deltaTime;
            timePULifeText.text = timerS.ToString() + "/3";

            if (timerS <= timeOfS)
            {
                timerS = 5;
                speed = 4;
                PULifePanel.SetActive(false);
            }
        }

        if (timeBtwAttack == 1)
        {
            PUTimeAttackPanel.SetActive(true);
            timerTA -= Time.deltaTime;
            PUTimeAttackPanelText.text = timerTA.ToString();

            if (timerTA <= timeOfS)
            {
                timerS = 5;
                timeBtwAttack = 2;
                PUTimeAttackPanel.SetActive(false);
            }
        }


    }
}
