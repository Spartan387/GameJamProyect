using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 5;
    private Transform playerPos;
    public GameObject dieEffect;
    public float distance;
    public bool patrol = true;
    public float dropChance = 30;
    public AudioClip deadEnemy;
    public List<PowerUp> powerUpList = new List<PowerUp>();
    public float minX;
    public float maxX;
    public float TiempoEspera = 2f;
    public float Velocidad = 1f;

    public GameObject _LugarObjetivo;

    public int hp = 1;
    void Start()
    {


    }
    void Awake()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //OnPlayerDetected?.Invoke(collider.gameObject);

    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (Vector2.Distance(transform.position, playerPos.position) > 0.3f)
        {
            speed = 5;
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }

    }

    public void TakeDamageB()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player e = collision.gameObject.GetComponent<Player>();
            e.TakeDamage();
            GameManager.instance.PlaySFX(deadEnemy);
            Destroy(gameObject);
            Destroy(GameObject.Find("Bar").transform.GetChild(0).gameObject);
            int chance = Random.Range(0, 101); // 0 -> 100
            if (chance >= dropChance)
            {
                Instantiate(powerUpList[Random.Range(0, powerUpList.Count)], transform.position, transform.rotation);
            }
        }
    }
    private void UpdateObjetivo()
    {
        if (_LugarObjetivo == null)
        {

            _LugarObjetivo.transform.position = new Vector3(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }


        if (_LugarObjetivo.transform.position.x == minX)
        {
            _LugarObjetivo.transform.position = new Vector3(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }


        else if (_LugarObjetivo.transform.position.x == maxX)
        {
            _LugarObjetivo.transform.position = new Vector3(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator Patrullar()
    {

        while (Vector2.Distance(transform.position, _LugarObjetivo.transform.position) > 0.05f)
        {

            Vector2 direction = _LugarObjetivo.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * Velocidad * Time.deltaTime);

            yield return null;
        }


        Debug.Log("Se alcanzo el Obejitvo");
        transform.position = new Vector3(_LugarObjetivo.transform.position.x, transform.position.y);


        Debug.Log("Esperando " + TiempoEspera + " segundos");
        yield return new WaitForSeconds(TiempoEspera);

        Debug.Log("Se espera lo necesario para que termine y vuelva a empezar movimiento");
        UpdateObjetivo();
        StartCoroutine("Patrullar");
    }
}
