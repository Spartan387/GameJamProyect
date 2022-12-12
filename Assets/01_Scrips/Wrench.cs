using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wrench : MonoBehaviour
{
    public float timeToDestroy = 10;
    int wrech = 0;
    public Text wrenchText;

    public GameObject gameOverPanel;


    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wrech = int.Parse(wrenchText.text);
            wrech++;
            wrenchText.text = wrech.ToString();
            Destroy(gameObject);
            if (wrech == 3)
            {
                gameOverPanel.SetActive(false);
            }
        }

    }
}
