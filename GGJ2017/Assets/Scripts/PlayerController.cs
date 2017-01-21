using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject wave;
    public GameObject pos;
    public float cooldown;
    private float timeLeft = 0;
    [SerializeField]
    private float jumpDist = 1f;
    private GameObject line;
    private LineRenderer lr;
    public float maxWavePower;
    public float minWavePower;
    public float scale;
    private GameObject myWave = null;
    private bool penalty;
    [SerializeField]
    private float penaltyTimer;
    [SerializeField]
    private float curPenaltyTimer;
    private bool fireDown = false;
    public int playerID = 1;
    public Color clr = Color.red;


    // Use this for initialization
    void Start()
    {
        line = setupLine();
        pos = GameObject.Find("MiddleIsland");
    }

    // Update is called once per frame
    void Update() {
        if (penalty)
        {
            drawLine(Vector3.zero);
            curPenaltyTimer -= Time.deltaTime;
            if (curPenaltyTimer <= 0)
            {
                curPenaltyTimer = penaltyTimer;
                penalty = false;
            }
            return;
        } else if (pos == null)
        {
            ResetPlayer();
        }
        handleInput();
    }

    GameObject setupLine()
    {
        Vector3 start = new Vector3(0, 0, 1);
        Color color = clr;
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
        lr.SetPosition(0, pos.transform.position + new Vector3(0, 0, -1));
        lr.SetPosition(1, pos.transform.position + (ray * size) + new Vector3(0, 0, -1));
    }

    void handleInput()
    {
        Vector2 ray = new Vector2(Input.GetAxis(playerID.ToString() + ":Horizontal"), Input.GetAxis(playerID.ToString() + ":Vertical"));
        ray.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos.transform.position, ray);
        drawLine(ray);

        if (Input.GetAxis(playerID.ToString() + ":Fire2") == 1)
        {
            createWave();
        }

        if (Input.GetAxis(playerID.ToString() + ":Fire3") == -1)
            pullIsland();

        if (hits.Length <= 0) return;
        RaycastHit2D hit = hits[0];
        float maxDist = float.PositiveInfinity;
        foreach (var h in hits)
        {
            if (h.collider.gameObject.tag == "Island" && maxDist > h.distance)
            {
                Debug.Log(h.collider.gameObject.tag);
                hit = h;
                maxDist = h.distance;
            }
        }
        float len = maxDist;

        if (len <= jumpDist)
        {
            drawLine(ray, len);
            if (Input.GetButtonDown(playerID.ToString() + ":Fire1") && hit.collider.gameObject.GetComponent<IslandBehavior>().getStatus() == 1 && hit.collider.gameObject.GetComponent<IslandBehavior>().getStatus() == 2)
            {
                switchToIsland(hit.collider.gameObject);
            }
        }
    }

    void createWave()
    {
        timeLeft -= Time.deltaTime;
        if (pos.gameObject.tag != "Island") return;
        if (myWave != null) return;
        if (timeLeft > 0) return;

        myWave = Instantiate(wave);
        myWave.GetComponent<WaveBehaviour>().setupWave(pos.transform.position, maxWavePower, minWavePower, scale, true);
        timeLeft = cooldown;
    }

    void pullIsland()
    { }

    void switchToIsland(GameObject island)
    {
        if(pos.gameObject.tag == "Island")
            pos.GetComponent<IslandBehavior>().setStatus(0);
        if (island.GetComponent<IslandBehavior>().getStatus() == 5)
        {
            GameObject.Find("PlayingField").GetComponent<PlayingFieldBehavior>().pointIslandReset();
            GameObject.Find("Score").GetComponent<ScoreCounter>().AddPoint(playerID);
        }
        pos = island;
        pos.GetComponent<IslandBehavior>().setStatus(playerID);
    }

    public void ResetPlayer()
    {
        pos = GameObject.FindGameObjectWithTag("Respawn");
        drawLine(Vector3.zero);
        penalty = true;
    }
}
