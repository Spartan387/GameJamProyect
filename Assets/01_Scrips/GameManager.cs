using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    //public AudioClip explosionSound;
    public static GameManager instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySFX(AudioClip sfx)
    {
        sfxAudioSource.PlayOneShot(sfx);
    }
}
