using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehavior : MonoBehaviour {

    private Rigidbody2D rbd;
    private bool hit;

    public Material neutral;
    public Material p1;

    [SerializeField]
    private int status = 0;

	// Use this for initialization
	void Start () {
        rbd = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        ColorPicker();
	}

    public void MoveIsland(Vector2 directionSpeed)
    {
        rbd.AddForce(directionSpeed);
    }

    public void DestroyIsland()
    {
        if (getStatus() == 1)
            GameObject.Find("Player").GetComponent<PlayerController>().ResetPlayer();
        Destroy(this.gameObject);

    }


    void ColorPicker()
    {
        switch(status)
        {
            case 0:
                this.gameObject.GetComponent<Renderer>().material = neutral;
                break;
            case 1:
                this.gameObject.GetComponent<Renderer>().material = p1;
                break;
        }
    }

    public void setStatus(int i) { status = i; }
    public int getStatus() { return status; }
}
