using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public int ScorePoints, ScorePoints2 = 0;
    public Text scorep1, scorep2;
	// Use this for initialization
	void Start () {
        OnHud();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddPoint(int player)
    {
        if (player == 1)
            ScorePoints++;
        else
            ScorePoints2++;
        OnHud();
    }
    public void TakePoint(int player)
    {
        if (player == 1)
            ScorePoints--;
        else
            ScorePoints2--;
        OnHud();
    }
    void OnHud()
    {
        scorep1.text = "Points = " + ScorePoints.ToString();
        scorep2.text = "Points = " + ScorePoints2.ToString();
    }
}
