using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject pos;
    [SerializeField] 
    private float jumpDist = 5f;

	// Use this for initialization
	void Start () {
        pos.GetComponent<IslandBehavior>().setStatus(1);
	}
	
	// Update is called once per frame
	void Update () {
        handleInput();
	}

    void handleInput()
    {
        Vector2 ray = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        ray.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos.transform.position, ray);
        Debug.DrawRay(pos.transform.position, ray, Color.green, 0.1f);
        Debug.Log(ray); 

        //bool check = false;
        foreach (var hit in hits)
        {

            
            if (hit.collider.gameObject.tag == "Island")
            {
                Debug.Log(hit.collider.gameObject.tag);
                Debug.Log("hey");
                switchToIsland(hit.collider.gameObject);
            }
        }
    }

    void switchToIsland(GameObject island)
    {
        pos.GetComponent<IslandBehavior>().setStatus(0);
        pos = island;
        pos.GetComponent<IslandBehavior>().setStatus(1);
    }
}
