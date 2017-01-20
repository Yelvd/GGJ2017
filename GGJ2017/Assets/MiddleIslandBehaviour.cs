using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleIslandBehaviour : MonoBehaviour {

    public GameObject wave;
    public float waveSpeed = 5;
    public float timeLeft = 0;
	// Use this for initialization
	void Start () {     
    }
	
	// Update is called once per frame
	void Update () {
        Wave();
    }

    void Wave()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = waveSpeed;
            Instantiate(wave);
        }
    }
}
