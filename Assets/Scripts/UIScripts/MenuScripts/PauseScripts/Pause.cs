using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : PauseUI
{
    void Start()
    {
        uiPausePannel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            uiPausePannel.SetActive(true);
        }
    }
}
