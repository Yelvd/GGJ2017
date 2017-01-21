using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public int ScorePoints = 0;
    public Text scoretext;
	// Use this for initialization
	void Start () {
        OnHud();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddPoint()
    {
        ScorePoints++;
        OnHud();
    }
    void OnHud()
    {
        scoretext.text = "Points = " + ScorePoints.ToString();
        
    }
}
