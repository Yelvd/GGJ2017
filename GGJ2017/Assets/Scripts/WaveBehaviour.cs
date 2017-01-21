using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

	// Use this for initialization
    public float speed = 0.01f;
    public float maxScale = 1f;
    private float maxSize;
    private float startWavePower;
    private float endWavePower;
    private bool hitFirst;


    void Start () {
        this.transform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        Grow();
	}

    void Grow()
    {
        /*grows the wave*/
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(scale.x + speed, scale.y + speed, scale.z + speed);

        /* Destroys the wave if it reaches the max size */
        if (this.GetComponent<SpriteRenderer>().bounds.size.x >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }
    private Vector2 getWavePower(GameObject other)
    {
        Vector3 waveHitDirection = other.transform.position - this.transform.position;
        waveHitDirection.Normalize();

        float currentPercentTraffeled = (maxSize / 100) * this.GetComponent<SpriteRenderer>().bounds.size.x;
        float wavePower = startWavePower - (startWavePower - endWavePower) * currentPercentTraffeled;

        Debug.Log(wavePower);
    
        Vector2 endResult = waveHitDirection * wavePower;

    return endResult;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hitFirst)
        {
            hitFirst = false;
            return;
        }
        if (other.gameObject.tag == "Island")
        {
            other.gameObject.GetComponent<IslandBehavior>().MoveIsland(getWavePower(other.gameObject));
        }
    }

    public void setupWave(Vector3 pos, float startPower, float endPower, float scale, bool hitFirst = false, float sp = 0.01f)
    {
        this.transform.position = pos;
        startWavePower = startPower;
        endWavePower = endPower;
        maxScale = scale;
        speed = sp;
        this.hitFirst = hitFirst;
        maxSize = GameObject.Find("PlayingField").GetComponent<SpriteRenderer>().bounds.size.x * maxScale;
    }
    
}
