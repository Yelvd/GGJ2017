using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Island")
        {
            other.gameObject.GetComponent<IslandBehavior>().DestroyIsland();
            GameObject.Find("MiddleIsland").GetComponent<SpawningScript>().deleteIsland();
        }
    }
}
