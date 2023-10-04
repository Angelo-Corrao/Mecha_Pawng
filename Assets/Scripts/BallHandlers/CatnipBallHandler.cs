using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading;
using UnityEngine;

[DisallowMultipleComponent]
public class CatnipBallHandler : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject catNipBall;
    private Vector3 direction;
    public float speed = 10f;
    public float speedMultiplier = 15.0f;
    public float dashSpeedMultiplier = 200.0f;
    public Ball ball;
    public bool hasAlreadyHit=false;
    public float timer = 0.2f;

    void Awake()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        rb = catNipBall.GetComponent<Rigidbody>();

    }
    private void Start()
    {
        AddInitialSpeed();  
    }

    private void AddInitialSpeed()
    {
       
        float x = ball.rb.velocity.x;
        float y = ball.rb.velocity.y;
        direction = new Vector3(x, y, 0);
        
        if(isActiveAndEnabled)
        {
            rb.AddForce((direction * speed)/300);
        }
    }
    private void Update()
    {
        
        if(hasAlreadyHit)
        {
            timer -=Time.deltaTime;
            if(timer<=0)
                hasAlreadyHit= false;
        }

        VelocityControl();
	}

    private void VelocityControl() {
        if (rb.velocity.magnitude > 6f) {
            Mathf.Min(6f, rb.velocity.magnitude);
        }
    }
}
