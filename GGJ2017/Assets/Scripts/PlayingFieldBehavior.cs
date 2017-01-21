using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldBehavior : MonoBehaviour {
    [SerializeField]
    private float curPointTimer, pointTimerSet;
    private bool pointTimerActive;
    private List<GameObject> islands;
    private GameObject finishIsland;
	// Use this for initialization
	void Start () {
        pointTimerActive = true;

	}
	
	// Update is called once per frame
	void Update () {
        if (pointTimerActive)
            timer();		
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
        if (finishIsland.GetComponent<IslandBehavior>().getStatus() == 1)
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
    public void pointIslandReset()
    {
        pointTimerActive = true;
    }
}
