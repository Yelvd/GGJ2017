using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleIslandBehaviour : MonoBehaviour {

    public GameObject wave;
    public float waveSpeed = 5;
    public float timeLeft = 0;
    public float maxWavePower = 50;
    public float minWavePower = 50;
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
            GameObject w = Instantiate(wave);
            w.GetComponent<WaveBehaviour>().setupWave(this.transform.position, maxWavePower, minWavePower, 1);
        }
    }
}
