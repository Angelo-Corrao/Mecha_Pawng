using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    public void ButtonToMainMenu()
    {
        
        SceneManager.LoadScene(0);
        Time.timeScale= 1.0f;
    }
}
