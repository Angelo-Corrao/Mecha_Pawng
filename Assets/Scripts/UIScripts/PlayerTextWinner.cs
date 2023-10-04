using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextWinner : MonoBehaviour
{
    public Text playerTextWins;
    void Start()
    {
        playerTextWins = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
       EndGameText();
    }
    private void EndGameText()
    {
        playerTextWins.text = GameManager.playerWinner;
    }
}
