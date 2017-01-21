using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject pos;
    [SerializeField] 
    private float jumpDist = 1f;
    private GameObject line;
    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        pos.GetComponent<IslandBehavior>().setStatus(1);
        line = setupLine();
	}

 
	
	// Update is called once per frame
	void Update () {
        handleInput();
	}

    GameObject setupLine()
    {
        Vector3 start = new Vector3(0,0,1);
        Color color = Color.red;
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.05f);
        return myLine;
    }

    void drawLine(Vector3 ray, float size = 0)
    {
        if (size == 0) { size = jumpDist; }
        lr.SetPosition(0, pos.transform.position  + new Vector3(0,0,-1));
        lr.SetPosition(1, pos.transform.position + (ray * size) + new Vector3(0, 0, -1));
    }

    void handleInput()
    {
        Vector2 ray = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        ray.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos.transform.position, ray);
        drawLine(ray);
        Debug.Log(ray);

        //bool check = false;
        foreach (var hit in hits)
        {         
            if (hit.collider.gameObject.tag == "Island")
            {
                drawLine(ray, (hit.collider.transform.position - pos.transform.position).magnitude);
                switchToIsland(hit.collider.gameObject);
            }
        }
    }

    void switchToIsland(GameObject island)
    {
        if (Input.GetButton("Fire1"))
        {
            pos.GetComponent<IslandBehavior>().setStatus(0);
            pos = island;
            pos.GetComponent<IslandBehavior>().setStatus(1);
        }
    }
}
