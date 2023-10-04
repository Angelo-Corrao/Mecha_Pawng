using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : PauseUI
{
    
    public void ResumeGame()
    {
        isPaused = false;
        if (!isPaused)
        {
            Time.timeScale = 1;
            uiPausePannel.SetActive(false);
        }
    }

}
