using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject uiPausePannel;
    public bool isPaused = false;
    public void PauseGame()
    {
       isPaused = true;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
    }
}
