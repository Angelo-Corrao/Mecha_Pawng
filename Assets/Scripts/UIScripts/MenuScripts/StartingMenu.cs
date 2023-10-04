using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartingMenu : MonoBehaviour
{
    public AudioSource sound;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        
        Application.Quit();
        
    }
}
