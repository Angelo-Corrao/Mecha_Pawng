using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour {
	internal Rigidbody rb;
	internal int totDashMultiplier = 0;
	private Vector3 direction;
	public Vector3 lastVelocity;
	public int lastHit = -1;
	public bool isBonusActive = false;
	public float speed = 300.0f;
	public float speedMultiplier = 15.0f;
	public float dashSpeedMultiplier = 200.0f;
	public int strength = 1;
	public AudioSource[] sound;
	public float timerStart = 1;
    private int totCollOneFrame = 0; 
	private string previousCollision;
	private bool isReset = false;
	public bool snowCat = false;
	public float time = 0;
	public float startTime = 0;
	private bool isForceAlreadyAdded = false;
	public float addForceStartTime = 0;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
	{
		startTime=Time.time;
        StartCoroutine(AddStartingForce());
		sound[0] = GetComponent<AudioSource>();
		sound[1] = GetComponent<AudioSource>();
    }

	private void Update() {
        //VelocityControll();

        
        
        time = Time.time - startTime;
        
		totCollOneFrame = 0;
        

        if (time > 1.2f){
			if (!isForceAlreadyAdded){
				if (!isReset && !snowCat) {
					float randomValueX = UnityEngine.Random.value < 0.5f ? -1.0f : 1.0f;
					float randomValueY = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1.0f, 0.0f)
															 : UnityEngine.Random.Range(0.0f, 1.0f);

					if (Math.Abs(rb.velocity.y) < 0.05f) {
						if (Math.Abs(rb.velocity.x) < 0.5f) {
							randomValueX = Math.Abs(randomValueX) * (Math.Abs(lastVelocity.x) / lastVelocity.x) * -1f;

							
							rb.velocity = Vector3.zero;
							direction = new Vector3(randomValueX, randomValueY, 0);
							rb.AddForce(direction * speed);
							
							isForceAlreadyAdded = true;
							addForceStartTime = Time.time;
						}
						else {
							
							randomValueX = Math.Abs(randomValueX) * (Math.Abs(lastVelocity.x) / lastVelocity.x) * -1f;
							rb.velocity = Vector3.zero;
							direction = new Vector3(randomValueX, randomValueY, 0);
							
							rb.AddForce(direction * speed);
							isForceAlreadyAdded = true;
							addForceStartTime = Time.time;
						}
					}
					else if (Math.Abs(rb.velocity.x) < 0.5f) {
                        randomValueX = Math.Abs(randomValueX) * (Math.Abs(lastVelocity.x) / lastVelocity.x) * -1f;
                        
						rb.velocity = Vector3.zero;
						direction = new Vector3(randomValueX, randomValueY, 0);
						rb.AddForce(direction * speed);

						isForceAlreadyAdded = true;
						addForceStartTime = Time.time;
					}
				}
			}
			else if (Time.time - addForceStartTime > 0.4f) {
				isForceAlreadyAdded = false;
			}
		}
		if (rb.velocity.x > 0.05f || rb.velocity.x < -0.05f) {
			lastVelocity = rb.velocity;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        totCollOneFrame++;

        if (!collision.gameObject.CompareTag("Paddle1") && !collision.gameObject.CompareTag("Paddle2"))
        {
            if ((collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("BottomWall")) && totCollOneFrame >= 2)
            {
                lastVelocity.x = -lastVelocity.x;

                float speed = lastVelocity.magnitude;
                direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                rb.velocity = direction * speed;
            }
            else
            {
                if ((previousCollision == "TopWall" || previousCollision == "BottomWall") && totCollOneFrame >= 2)
                {
                    lastVelocity.y = -lastVelocity.y;

                    float speed = lastVelocity.magnitude;
                    direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                    rb.velocity = direction * speed;
                }
                else
                {
                    float speed = lastVelocity.magnitude;
                    direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                    rb.velocity = direction * speed;
                }
            }
        }

		previousCollision = collision.gameObject.tag;
	}
    private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GhostBar")
			sound[0].Play();
		else if (other.tag == "CatNipBall")
			sound[1].Play();
    }
	public IEnumerator AddStartingForce() {
		yield return new WaitForSeconds(1);
			float randomValueX = UnityEngine.Random.value < 0.5f ? -1.0f : 1.0f;
			float randomValueY = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1.0f, 0.0f)
													 : UnityEngine.Random.Range(0.0f, 1.0f);

			direction = new Vector3(randomValueX, randomValueY, 0);


			rb.AddForce(direction * speed);
			yield return null;

		isReset = false;
	}

	public void ResetPosition() {
		rb.position= Vector3.zero;
		rb.velocity = Vector3.zero;
		strength = 1;
		totDashMultiplier = 0;
		isBonusActive = false;
		timerStart = 1;
		isReset = true;
		startTime = Time.time;
		StartCoroutine(AddStartingForce());
	}

	public void AddForce(Vector3 force) {
		rb.AddForce(force);
	}

	public void dashBonus(bool moreStrength) {
		isBonusActive = true;

		if (totDashMultiplier < 2) {
			totDashMultiplier++;
			float x = rb.velocity.x / Mathf.Abs(rb.velocity.x);
			float y = rb.velocity.y / Mathf.Abs(rb.velocity.y);
			rb.AddForce(new Vector3(x, y, 0) * dashSpeedMultiplier);
		}

		if (moreStrength)
			strength = 2;
		else
			strength = 1;
	}
}
