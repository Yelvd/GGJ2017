using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayingFieldBehavior : MonoBehaviour {
    [SerializeField]
    private float curPointTimer, pointTimerSet;
    public bool pointTimerActive;
    private List<GameObject> islands;
    private GameObject finishIsland;
    public Material p1;
    public Material p2;
    public bool gameStarted = false;

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
        Material win;
        if (i == 1)
            win = p1;
        else
            win = p2;

        foreach (GameObject island in GameObject.FindGameObjectsWithTag("Island"))
        {
            island.GetComponent<IslandBehavior>().setStatus(i);
        }
        GameObject.Find("MiddleIsland").GetComponent<SpawningScript>().maxIslands = -1;
        pointTimerActive = false;
    }

    public void pointIslandReset()
    {
        pointTimerActive = true;
    }

    public void options()
    {
        if (GameObject.Find("Score").GetComponent<ScoreCounter>().ScorePoints == 0 &&
            GameObject.Find("Score").GetComponent<ScoreCounter>().ScorePoints2 == 0)
            return;
        else
            SceneManager.LoadScene("MainScene");
    }
    public void OnGUI()
    {
        if (true)
        {
            Rect r = new Rect(new Vector2(Screen.width / 2 + Screen.height / 2 + Screen.width / 8, Screen.height / 4), new Vector2(Screen.height / 2, Screen.width / 4));
            GUI.Label(new Rect(), "Both press Y to restart \n Left bumber to create wave \n Right bumber pull to closest island \n Left stick and A to move");
        }
    }
}
