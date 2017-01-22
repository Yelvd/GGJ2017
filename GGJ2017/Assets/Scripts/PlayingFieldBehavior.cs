using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayingFieldBehavior : MonoBehaviour {
    [SerializeField]
    private float curPointTimer, pointTimerSet;
    public bool pointTimerActive;
    private List<GameObject> islands;
    private GameObject finishIsland;
    public bool gameStarted = false;
    public Text text1, text2;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (pointTimerActive)
            timer();
        if (Input.GetButton("1:select") && Input.GetButton("2:select"))
            options();
	}
    private void timer()
    {
        curPointTimer -= Time.deltaTime;
        if (curPointTimer <= 0)
        {
            findFinishIslands();
            pointTimerActive = false;
            curPointTimer = pointTimerSet;
        }
    }

    private void findFinishIslands()
    {
        islands = new List<GameObject>(GameObject.FindGameObjectsWithTag("Island"));
        int randomIndexNumber = Random.Range(0, islands.Count);
        finishIsland = islands[randomIndexNumber];
        if (finishIsland.GetComponent<IslandBehavior>().getStatus() == 1 || finishIsland.GetComponent<IslandBehavior>().getStatus() == 2)
        {
            int newRandomIndexNumber = Random.Range(0, islands.Count);
            while (newRandomIndexNumber == randomIndexNumber)
                newRandomIndexNumber = Random.Range(0, islands.Count);
            finishIsland = islands[newRandomIndexNumber];
        }
        finishIsland.GetComponent<IslandBehavior>().setStatus(5);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Island")
        {
            other.gameObject.GetComponent<IslandBehavior>().DestroyIsland();
            GameObject.Find("MiddleIsland").GetComponent<SpawningScript>().deleteIsland();
        }
    }

    public void victory(int i)
    {
        if (i == 1) {
            text2.text = "Red Wins";
            text2.color = Color.red;
        }
        else
        {
            text2.text = "Green Wins";
            text2.color = Color.green;
        }


        foreach (GameObject island in GameObject.FindGameObjectsWithTag("Island"))
        {
            island.GetComponent<IslandBehavior>().setStatus(i);
        }
        GameObject.Find("MiddleIsland").GetComponent<SpawningScript>().maxIslands = -1;
        pointTimerActive = false;
        text1.enabled = true;
        text2.enabled = true;
       
        
       
    }

    public void pointIslandReset()
    {
        pointTimerActive = true;
    }

    public void options()
    {
        if (GameObject.Find("Score").GetComponent<ScoreCounter>().ScorePoints == 0 &&
            GameObject.Find("Score").GetComponent<ScoreCounter>().ScorePoints2 == 0)
            Application.Quit();
        else
        {
            GameObject.Find("Player").GetComponent<PlayerController>().reset();
            SceneManager.LoadScene("MainScene");
        }
           
    }
}
