using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleIslandBehaviour : MonoBehaviour {

    public GameObject wave;
    public float waveSpeed = 5;
    public float timeLeft = 0;
    public float maxWavePower = 50;
    public float minWavePower = 50;
    [SerializeField]
    private List<Material> materials;
    [SerializeField]
    float curTimer, setTimer = 0.5f;
    private int playerID;
    private bool respawnSwitch;
	// Use this for initialization
	void Start () {     
    }
	
	// Update is called once per frame
	void Update () {
        Wave();
        if (respawnSwitch)
        {
            this.gameObject.GetComponent<Renderer>().material = materials[playerID];
            curTimer -= Time.deltaTime;
            if (curTimer <= 0)
            {
                this.gameObject.GetComponent<Renderer>().material = materials[0];
                curTimer = setTimer;
                respawnSwitch = false;
            }
        }
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
    public void playerRespawnFlash(int playerid)
    {
        this.playerID = playerid;
        respawnSwitch = true;
    }

}
