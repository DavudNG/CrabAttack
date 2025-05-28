using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public AudioSource ButtonSound;


    public void PlayGame()
    {
        ButtonSound.pitch = Random.Range(0.9f, 1.1f)
        ButtonSound.Play();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        ButtonSound.Play();
        Application.Quit();
    }
}
