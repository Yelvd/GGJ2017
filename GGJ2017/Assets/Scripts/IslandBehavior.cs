using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehavior : MonoBehaviour {
    private Rigidbody2D rbd;
    private bool hit;
	// Use this for initialization
	void Start () {
        rbd = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MoveIsland(Vector2 directionSpeed)
    {
        rbd.AddForce(directionSpeed/2);
    }
    public void DestroyIsland()
    {
        Destroy(this.gameObject);

    }
}
