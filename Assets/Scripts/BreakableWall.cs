using UnityEngine;

public class BreakableWall : MonoBehaviour
{
	public int totHitNum = 2;
	public float invulnerability = 0;
	private Renderer wallRenderer;
	private float invulStartTime = 0;
	private AudioSource sound;

	private void Start() {
		wallRenderer = this.GetComponent<Renderer>();
		sound= this.GetComponent<AudioSource>();
	}

	private void Update() {
		float time = Time.time - invulStartTime;
		if (time > 2)
			invulnerability = 0;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("CatNipBall"))
		{
			sound.Play();
			collision.gameObject.SetActive(false);
			totHitNum--;
            switch (totHitNum)
            {
                case 1:
                    wallRenderer.material.color = Color.cyan;
                    break;
                case <= 0:
                    this.gameObject.SetActive(false);
                    break;
            }
        }
		if (collision.gameObject.CompareTag("Ball")) {
            sound.Play();
            if (invulnerability == 0) {
				Ball ball = collision.gameObject.GetComponent<Ball>();
				totHitNum -= ball.strength;

				if (ball.isBonusActive) {
					float x = ball.rb.velocity.x / Mathf.Abs(ball.rb.velocity.x);
					float y = ball.rb.velocity.y / Mathf.Abs(ball.rb.velocity.y);
					ball.AddForce(new Vector3(-x, -y, 0) * ball.dashSpeedMultiplier * ball.totDashMultiplier);
					ball.strength = 1;
					ball.totDashMultiplier = 0;
					ball.isBonusActive = false;
				}

				switch (totHitNum) {
					
					case 1:
						wallRenderer.material.color = Color.cyan;
						break;
					case <=0:
						this.gameObject.SetActive(false);
						break;
				}

				invulnerability = 2;
				invulStartTime = Time.time;
			}
		}
	}
}
