using UnityEngine;
using UnityEngine.UI;

public class GhostBar : MonoBehaviour {
	public Player1 paddle1;
	public Player2 paddle2;
	public GameObject cubeAbilities;
	public Ball ball;
	public Text malusText;

	private void Awake() {
		paddle1 = GameObject.Find("Player 1").GetComponent<Player1>();
		paddle2 = GameObject.Find("Player 2").GetComponent<Player2>();
		ball = GameObject.Find("Ball").GetComponent<Ball>();
		malusText = GameObject.Find("malusText").GetComponent<Text>();
        
    }

	private void OnTriggerEnter(Collider other) {
        if (other.tag == "GhostBar" || other.tag == "CatNip" || other.tag == "MagentPaw" || other.tag == "SnowCat")
        {
            cubeAbilities.SetActive(false);
            AbilitiesSpawner.isOverlapped = true;
            AbilitiesSpawner.counter--;
        }
        if (other.tag == "Ball") {
			
			paddle1.ghostBar = true;
			paddle1.color.a = 0.4f;
			paddle1.wallRenderer.material.color = paddle1.color;
			paddle1.ghostBar = true;
			paddle1.ghostBarStartTime = Time.time;
			paddle1.GetComponent<Collider>().enabled = false;


            paddle2.GetComponent<Collider>().enabled = false;
            paddle2.ghostBar = true;
			paddle2.color.a = 0.4f;
			paddle2.wallRenderer.material.color = paddle2.color;
			paddle2.ghostBar = true;
			paddle2.ghostBarStartTime = Time.time;
            malusText.text = "Ghost Bar 'Now you see me, now you don't!'";

			cubeAbilities.SetActive(false);
			AbilitiesSpawner.counter--;
		}
	}
}
