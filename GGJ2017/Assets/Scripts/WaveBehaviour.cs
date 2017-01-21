using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour {

	// Use this for initialization
    public float speed = 0.01f;
    public float maxScale = 1f;
    private float maxSize;
    [SerializeField]
    private float wavePower;
    [SerializeField]
    private List<GameObject> islands;

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
    private Vector2 getWavePower(GameObject other)
    {
        Vector3 waveHitDirection = other.transform.position - this.transform.position;
        waveHitDirection.Normalize();
        if (wavePower > 20)
        {
            float currentPercentTraffeled = (maxSize / 100) * this.GetComponent<SpriteRenderer>().bounds.size.x;
            wavePower -= currentPercentTraffeled;            
        }
        else
        {
            wavePower = 20;
        }
        Vector2 endResult = new Vector2(waveHitDirection.x *= wavePower, waveHitDirection.y *= wavePower);
    return endResult;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Island")
        {
            other.gameObject.GetComponent<IslandBehavior>().MoveIsland(getWavePower(other.gameObject));
        }
    }
    
}
