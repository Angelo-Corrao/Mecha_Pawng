using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Paddle : MonoBehaviour {

	protected Rigidbody rb;
    [SerializeField]
	protected float speed = 10.0f;
	protected float slowingSpeed = 4.5f;
	protected float slowingStartTime = 0f;
	protected bool puurfectShoot = false;
	public bool ghostBar = false;
	public bool magnetPaw = false;
	public bool hasAbility = false;
	public bool snowCat=false;
	public bool catNip=false;
    public AudioSource[] sound;

    protected float dashStartTime = 0.0f;
	public float ghostBarStartTime = 0.0f;
	public float totSlowingTime = 3f;
	[SerializeField]
	protected float dashSpeed = 5f;
	[SerializeField]
	protected float animationTime = 1f;
	protected bool isAnimationActive = false;
	[Tooltip("This property indicates for how much time the paddle goes forward in percentage compared to the total " +
		"dash's duration")]
	[SerializeField]
	[Range(0.1f, 0.8f)]
	protected float dashPositiveTime = 0.4f;
	public Abilities activeAbility = 0;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Start()
	{
        sound[0] = GetComponent<AudioSource>();
        sound[1] = GetComponent<AudioSource>();
        sound[2] = GetComponent<AudioSource>();
        sound[3] = GetComponent<AudioSource>();

    }
}
