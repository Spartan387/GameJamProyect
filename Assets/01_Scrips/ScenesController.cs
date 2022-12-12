using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Game()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
