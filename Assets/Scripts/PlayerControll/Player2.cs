using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Player2 : Paddle {

    private float x = 0f;
    private float y = 0f;
    private float currentSpeed = 0.0f;
    private float dis = 0f;
	public float snowCatTimer = 1f;
    public Color color;
	public Renderer wallRenderer;
	public Player1 p1;
	public Ball ball;
	public Text abilityTextP2;
	public Text malusText;
	private CatNip catnip;

    private Vector3 direction;

	private void Start() {
		wallRenderer = this.GetComponent<Renderer>();
		color = wallRenderer.material.color;
	}

	// Update is called once per frame
	void Update()
	{

        if (!isAnimationActive) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				puurfectShoot = true;
				isAnimationActive = true;
				dashStartTime = Time.time;
			}

			if (Input.GetKeyDown(KeyCode.RightControl)) {
				switch (activeAbility) {
					case Abilities.MAGNET_PAW:
						magnetPaw = true;

                        sound[0].Play();
                        hasAbility = false;
						color = Color.white;
						wallRenderer.material.color = color;
						abilityTextP2.text = string.Empty;
						break;

                    case Abilities.SNOW_CAT:
                        snowCat = true;
						ball.snowCat = true;

						sound[1].Play();
                        hasAbility = false;
                        color = Color.white;
                        wallRenderer.material.color = color;
                        abilityTextP2.text = string.Empty;
                        ball.startTime = Time.time;
                        break;
                }
			}
        }

		if (puurfectShoot)
		{
			float time = Time.time - dashStartTime;
			if (time < animationTime * dashPositiveTime)
			{
				transform.position += Vector3.left * dashSpeed * Time.deltaTime;

				if (ghostBar) {
                    GetComponent<Collider>().enabled = true;
                    color.a = 1f;
					wallRenderer.material.color = color;
				}
			}
			else if (transform.position.x < 8.4f)
			{
				transform.position += Vector3.right * ((dashSpeed * dashPositiveTime) / (1 - dashPositiveTime)) * Time.deltaTime;

				if (ghostBar) {
                    GetComponent<Collider>().enabled = false;
                    color.a = 0.4f;
					wallRenderer.material.color = color;
				}
			}
			else
			{
				puurfectShoot = false;
				isAnimationActive = false;
			}
		}
		else
		{
			direction = new Vector3(0, Input.GetAxisRaw("Vertical2"), 0);
			Vector3 newpos = transform.position + (direction * speed * Time.deltaTime);
			newpos.y = Mathf.Clamp(newpos.y, -3.7f, 3.3f);
			transform.position = newpos;

			if (magnetPaw) {
				ball.transform.position = rb.transform.position + new Vector3(-0.5f, 0, 0);
				activeAbility = 0;
				magnetPaw = false;
			}

			if (ghostBar) {
				float time = Time.time - ghostBarStartTime;
				if (time > 5f) {
                    GetComponent<Collider>().enabled = true;
                    color.a = 1f;
					wallRenderer.material.color = color;
					ghostBar = false;
                    malusText.text = string.Empty;
				}
			}
			if (snowCat)
			{
				snowCatTimer -= Time.deltaTime;
				ball.rb.velocity = Vector3.zero;

				if (snowCatTimer <= 0f)
				{
					ball.rb.velocity = new Vector3(Random.Range(-5f, -7f), Random.Range(-1f, 1f), 0);
					snowCat = false;
                    activeAbility = 0;
					ball.isBonusActive= false;
                    ball.strength = 1;
                    ball.totDashMultiplier = 0;
                    snowCatTimer = 1f;
					ball.snowCat = false;
				}
			}
			if (catnip)
			{
                catnip.CatNipAbility();
            }
            if (Time.time - slowingStartTime > totSlowingTime)
				speed = 10f;
		}
    }

	private void OnCollisionEnter(Collision collision) {
        CatnipBallHandler catnipball = collision.gameObject.GetComponent<CatnipBallHandler>();
        sound[2].Play();

        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.lastHit = 1;

            if (color.a == 1)
            {
                x = ball.lastVelocity.x;
                y = ball.lastVelocity.y;
                currentSpeed = ball.lastVelocity.magnitude;
                x *= -1;
                if (y == 0)
                    y = 1;
                else if (y > 5)
                    y--;
                else if (y < 5)
                    y++;


                dis = Vector3.Distance(transform.position, collision.transform.position);
                //bottom l'alto
                if (transform.position.y > collision.transform.position.y && y > 0)
                {
                    y *= -1;
                    y -= Mathf.Sin(dis);
                }
                //bottom verso il basso
                else if (transform.position.y > collision.transform.position.y && y < 0)
                {
                    y -= Mathf.Sin(dis);
                }
                //top verso l'alto
                else if (transform.position.y < collision.transform.position.y && y > 0)
                {

                    y += Mathf.Sin(dis);
                }
                //top verso il basso
                else if (transform.position.y < collision.transform.position.y && y < 0)
                {
                    y *= -1;
                    y += Mathf.Sin(dis);
                }

                ball.rb.velocity = new Vector3(x, y, 0).normalized * currentSpeed;
            }

            if (puurfectShoot && x < 0)
            {
                
                float time = Time.time - dashStartTime;
                bool moreStrength;
                // Parte positiva
                if (time < animationTime * dashPositiveTime)
                {
                    if (time > (animationTime * dashPositiveTime) / 2)
                    {
                        sound[3].Play(); //PURESHOTAUDIO COLPITO BENE
                        if (time > (animationTime * dashPositiveTime) * (3f / 4f))
                            moreStrength = true;
                        else
                            moreStrength = false;
                        ball.dashBonus(moreStrength);
                    }
                }
                // Parte negativa
                else
                {
                    sound[4].Play(); //PUREFECTSHOT COLPITO MALE
                        
                    speed = slowingSpeed;
                    slowingStartTime = Time.time;

                    float x = ball.rb.velocity.x / Mathf.Abs(ball.rb.velocity.x);
                    float y = ball.rb.velocity.y / Mathf.Abs(ball.rb.velocity.y);
                    ball.AddForce(new Vector3(-x, -y, 0) * ball.dashSpeedMultiplier * ball.totDashMultiplier);
                    ball.strength = 1;
                    ball.totDashMultiplier = 0;
                    ball.isBonusActive = false;
                }
            }
            else if (ball.isBonusActive)
            {
                ball.strength = 1;
            }
        }
	}
}
