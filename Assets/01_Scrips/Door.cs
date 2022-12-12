using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player e = collision.gameObject.GetComponent<Player>();
            Destroy(gameObject);
            SceneManager.LoadScene("SamplEscene");
        }
    }
}
