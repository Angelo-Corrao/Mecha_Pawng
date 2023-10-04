using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowCat : MonoBehaviour
{
    public Player1 paddle1;
    public Player2 paddle2;
    public GameObject cubeAbilities;
    public Ball ball;
    public Text abilityTextP1;
    public Text abilityTextP2;
    public AudioSource source;

    private void Awake()
    {
        paddle1 = GameObject.Find("Player 1").GetComponent<Player1>();
        paddle2 = GameObject.Find("Player 2").GetComponent<Player2>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        abilityTextP1 = GameObject.Find("P1Ability").GetComponent<Text>();
        abilityTextP2 = GameObject.Find("P2Ability").GetComponent<Text>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GhostBar" || other.tag == "CatNip" || other.tag == "MagentPaw" || other.tag == "SnowCat")
        {
            cubeAbilities.SetActive(false);
            AbilitiesSpawner.isOverlapped = true;
            AbilitiesSpawner.counter--;
        }
        if (other.tag == "Ball")
        {
            if (ball.lastHit == 0)
            {
                if (!paddle1.hasAbility)
                {
                    paddle1.activeAbility = Abilities.SNOW_CAT;
                    paddle1.hasAbility = true;
                    paddle1.color = Color.cyan;
                    paddle1.wallRenderer.material.color = paddle1.color;
                    cubeAbilities.SetActive(false);
                    AbilitiesSpawner.counter--;
                    abilityTextP1.text = "S";
                }
            }
            else if (ball.lastHit == 1)
            {
                if (!paddle2.hasAbility)
                {
                    paddle2.activeAbility = Abilities.SNOW_CAT;
                    paddle2.hasAbility = true;
                    paddle2.color = Color.cyan;
                    paddle2.wallRenderer.material.color = paddle2.color;
                    cubeAbilities.SetActive(false);
                    AbilitiesSpawner.counter--;
                    abilityTextP2.text = "S";
                }
            }
        }
    }
}
