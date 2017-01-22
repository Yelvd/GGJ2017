using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehavior : MonoBehaviour {

    private Rigidbody2D rbd;
    private bool hit;

    public Material neutral;
    public Material p1, p2, p3, p4;
    public Material finish;


    [SerializeField]
    private int status = 0;

	// Use this for initialization
	void Start () {
        rbd = GetComponent<Rigidbody2D>();
        ColorPicker();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MoveIsland(Vector2 directionSpeed)
    {
        rbd.AddForce(directionSpeed);
    }

    public void DestroyIsland()
    {
        if (getStatus() == 5)
            GameObject.Find("PlayingField").GetComponent<PlayingFieldBehavior>().pointIslandReset();
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
            case 2:
                this.gameObject.GetComponent<Renderer>().material = p2;
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                this.gameObject.GetComponent<Renderer>().material = finish;
                break;
        }
    }

    public void setStatus(int i) { status = i; ColorPicker(); }
    public int getStatus() { return status; }
}
