using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

	// Use this for initialization
    public float speed = 0.01f;
    public float maxScale = 1f;
    private float maxSize;

	void Start () {
        this.transform.localScale = Vector3.zero;
        maxSize = GameObject.Find("PlayingField").GetComponent<SpriteRenderer>().bounds.size.x * maxScale;
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

    
}
