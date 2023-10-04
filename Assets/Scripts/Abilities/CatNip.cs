using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

[DisallowMultipleComponent]
public class CatNip : MonoBehaviour
{
    private Player1 paddle1;
    private Player2 paddle2;
    public GameObject catNipBall;
    public GameObject cubeAbilities;
    private Ball ball;

    private void Awake()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        paddle1 = GameObject.Find("Player 1").GetComponent<Player1>();
        paddle2 = GameObject.Find("Player 2").GetComponent<Player2>();
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
            paddle1.catNip = true;
            paddle2.catNip = true;
            cubeAbilities.SetActive(false);
            CatNipAbility();
            AbilitiesSpawner.counter--;
        }
    }

    void Update()
    {
        
    }
    public void CatNipAbility()
    {
        if (paddle1.catNip||paddle2.catNip)
        {
            float ballPositionX = ball.transform.position.x;
            float ballPositionY = ball.transform.position.y;
            Vector3 ballPositon = new Vector3(ballPositionX - 3, ballPositionY, 0);
            GameObject catnipBall = Instantiate(catNipBall, ballPositon, Quaternion.identity);
            catnipBall.GetComponent<Rigidbody>().velocity = ball.rb.velocity / 2;
        }
    }
}
