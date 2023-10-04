using UnityEngine;
using UnityEngine.Events;

public class ScoringZone : MonoBehaviour
{
	public UnityEvent onCollisionEnter;
    public GameObject catNipBall;

    private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Ball"))
			onCollisionEnter.Invoke();
		else if (collision.gameObject.CompareTag("CatNipBall"))
		{
			collision.gameObject.SetActive(false);
            onCollisionEnter.Invoke();
        }
	}
}
