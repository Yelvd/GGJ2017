using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public int ScorePoints, ScorePoints2 = 0;
    public Text scorep1, scorep2;
    public int winScore = 10;
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
            ScorePoints += 2;
        else
            ScorePoints2 += 2;

        if (ScorePoints >= winScore || ScorePoints2 >= winScore)
            GameObject.Find("PlayingField").GetComponent<PlayingFieldBehavior>().victory(player);
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
