using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAbilities : MonoBehaviour
{
    public GameObject cubeAbilities;
	public Player1 paddle1;
	public Player2 paddle2;

	private void Awake() {
		paddle1 = GameObject.Find("Player 1").GetComponent<Player1>();
		paddle2 = GameObject.Find("Player 2").GetComponent<Player2>();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (paddle1.hasAbility)
            cubeAbilities.SetActive(false);
            AbilitiesSpawner.counter--;

        }
    }
}
