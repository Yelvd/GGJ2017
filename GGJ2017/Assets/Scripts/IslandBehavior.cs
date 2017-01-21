using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehavior : MonoBehaviour {

    public Material neutral;
    public Material p1;

    [SerializeField]
    private int status = 0;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        ColorPicker();
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
    public int getSatus() { return status; }
}
