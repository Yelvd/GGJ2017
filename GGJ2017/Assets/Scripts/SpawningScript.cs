using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour {

    public GameObject islandPrefab;

    private float setTimer;
    private bool timerSet;
    float currentTime;
    [SerializeField]
    private int islandCounter;
    
    [SerializeField]
    private int maxIslands;
	// Use this for initialization
	void Start () {
        setTimer = 2;        

	}
	
	// Update is called once per frame
	void Update () {
		if (islandCounter < maxIslands)
        {
            Timer();
        }
	}
    private void Timer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            Instantiate(islandPrefab, IslandPositionSetter(new Vector3(0,0,0),0, new Vector3(0,0,0)), Quaternion.identity);
            addIsland();
            currentTime = SetCurrentTime();
        }
    }
    private float SetCurrentTime()
    {
        float currentTime = this.setTimer;
        return currentTime;
    }
    private Vector2 IslandPositionSetter(Vector3 mainIslandPosition, float mainIslandRadius, Vector3 newIslandPosition)
    {
        mainIslandPosition = this.transform.position;
        Vector2 angle = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        angle.Normalize();
        Debug.Log(angle);
        newIslandPosition = new Vector3(this.GetComponent<SpriteRenderer>().bounds.size.x * angle.x, this.GetComponent<SpriteRenderer>().bounds.size.y * angle.y, 0);
        return newIslandPosition;
    }
    public void addIsland()
    {
        islandCounter++;
    }
    public void deleteIsland()
    {
        islandCounter--;
    }
}
