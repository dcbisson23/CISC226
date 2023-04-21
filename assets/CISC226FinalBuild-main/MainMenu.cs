using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{   
    public AudioSource click;

    public void PlayGame()
    {
        click.Play();
        SceneManager.LoadScene(1);
    }
    public void QuitGame ()
    {
        click.Play();
        Debug.Log("quit");
        Application.Quit();   
    }
    public void openOptions ()
    {
        click.Play();
    }
}
