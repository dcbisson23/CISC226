using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseUI;
    public GameObject OptionUI;

    public AudioSource click;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        click.Play();
        pauseUI.SetActive(false);
        OptionUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }
    void Pause ()
    {
        click.Play();
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadOptions()
    {
        click.Play();
        pauseUI.SetActive(false);
        OptionUI.SetActive(true);
    }
    public void UnloadOptions()
    {
        click.Play();
        pauseUI.SetActive(true);
        OptionUI.SetActive(false);
    }
    
    
    public void QuitGame()
    {   
        click.Play();   
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}
