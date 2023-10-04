using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilitiesSpawner : MonoBehaviour
{

    public GameObject[] abilities;
    public static int counter = 3;
    public float timer = 3;
    public static bool isOverlapped=false;

    // Update is called once per frame
    void Update()
    {       
        if (counter < 3)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0 || isOverlapped) 
                {

                    RandomSpawner();
                    
                    counter++; 
                }
            }
        }
    }
	private void Start() {
		for(int i =0; i<3; i++) {
            RandomSpawner();
        }
	}
	public void RandomSpawner()
    {
        float randomValueX = 0;
        float randomValueY = UnityEngine.Random.Range(-3, 3);
        if(randomValueY < 0.6f || randomValueY > -0.6f)
        {
              randomValueX = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-2.0f, 0.7f)
                                                 : UnityEngine.Random.Range(1.3f, 4f);
        }
        else {
             randomValueX = UnityEngine.Random.Range(-3, 3);
        }

        GameManager.abilityIndex = UnityEngine.Random.Range(0, abilities.Length);
        Vector3 randomPos = new Vector3(randomValueX,randomValueY,0);
        GameObject abilitiesCloned = Instantiate(abilities[GameManager.abilityIndex],randomPos,Quaternion.identity);
        timer = 3;
        isOverlapped = false;
    }
}
