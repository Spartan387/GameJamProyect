using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyPatroll : MonoBehaviour
{
    
    public float minX;
    public float maxX;
    public float TiempoEspera = 2f;
    public float Velocidad = 1f;
    public float dropChance = 30;
    private Animator animR;
    public float hp = 5;
    public AudioClip deadEnemy;
    public List<PowerUp> powerUpList = new List<PowerUp>();
    public bool setDeath;
    public GameObject _LugarObjetivo;

    // Start is called before the first frame update
    void Start()
    {
        UpdateObjetivo();
        StartCoroutine("Patrullar");
        animR = GetComponent<Animator>();
    }

    void Update()
    {
        //SetAnimD();
    }
    public void TakeDamageB()
    {
        hp--;
        if (hp <= 0)
        {
            setDeath = true;
            this.enabled = false;
            Destroyer();
        }
    }
    /*void SetAnimD()
    {
        animR.SetBool("DeadRobot", setDeath);
    }*/
    void Destroyer()
    {
        int chance = Random.Range(0, 1); // 0 -> 100
        if (chance >= dropChance)
        {
            Instantiate(powerUpList[Random.Range(0, powerUpList.Count)],
                transform.position, transform.rotation);
        }
        GameManager.instance.PlaySFX(deadEnemy);

        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
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
