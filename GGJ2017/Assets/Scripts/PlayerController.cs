using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float pullPower;
    public Material mat;
    public GameObject wave;
    public GameObject pos;
    public GameObject linePreFab;
    public float cooldownWave = 3;
    private float timeLeftWave = 0;
    public float cooldownPull = 2;
    private float timeLeftPull = 0;
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
    public int playerID = 0;
    public Color clr = Color.red;
    public static bool notJumped = true;
    public Text text1, text2;


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
                GameObject.Find("MiddleIsland").GetComponent<MiddleIslandBehaviour>().playerRespawnFlash(playerID);
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
        GameObject myLine = Instantiate(linePreFab);
        myLine.transform.position = start;
        lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.material = mat;
        //lr.startColor = clr;
        //lr.endColor = clr;
        //lr.startWidth = 0.05f;
        //lr.endWidth = 0.05f;
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

        timeLeftWave -= Time.deltaTime;
        timeLeftPull -= Time.deltaTime;

        if (Input.GetButtonDown(playerID.ToString() + ":Fire2"))
            createWave();

        if (Input.GetButtonDown(playerID.ToString() + ":Fire3"))
            pullIsland();

        if (hits.Length <= 0) return;
        RaycastHit2D hit = hits[0];
        float maxDist = float.PositiveInfinity;
        foreach (var h in hits)
        {
            if (h.collider.gameObject.tag == "Island" && maxDist > h.distance)
            {
                hit = h;
                maxDist = h.distance;
            }
        }
        float len = maxDist;

        if (len <= jumpDist)
        {
            drawLine(ray, len);
            if (Input.GetButtonDown(playerID.ToString() + ":Fire1") && hit.collider.gameObject.GetComponent<IslandBehavior>().getStatus() != 1 && hit.collider.gameObject.GetComponent<IslandBehavior>().getStatus() != 2)
            {
                switchToIsland(hit.collider.gameObject);
            }
        }
    }

    void createWave()
    {
        if (pos.gameObject.tag != "Island") return;
        if (myWave != null) return;
        if (timeLeftWave > 0) return;

        myWave = Instantiate(wave);
        myWave.GetComponent<WaveBehaviour>().setupWave(pos.transform.position, maxWavePower, minWavePower, scale, true);
        timeLeftWave = cooldownWave;
    }

    void pullIsland()
    {
        if (timeLeftPull > 0) return;
        if (pos.gameObject.tag != "Island") return;
        GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
        float dist = float.PositiveInfinity;
        GameObject island = null;
        foreach(GameObject land in islands)
        {
            float d = Mathf.Abs((land.transform.position - pos.transform.position).magnitude);
            if(d < dist & d != 0)
            {
                dist = d;
                island = land;
            }
        }

        if (island == null) return;

        Vector2 vec = (island.transform.position - pos.transform.position);
        vec.Normalize();
        pos.GetComponent<Rigidbody2D>().AddForce(vec * pullPower);
        island.GetComponent<Rigidbody2D>().AddForce(-vec * pullPower);
        timeLeftPull = cooldownWave;
    }

    void switchToIsland(GameObject island)
    {
        if(pos.gameObject.tag == "Island")
            pos.GetComponent<IslandBehavior>().setStatus(0);
        else if (notJumped)
        {
            GameObject.Find("PlayingField").GetComponent<PlayingFieldBehavior>().pointTimerActive = true;
            GameObject.Find("PlayingField").GetComponent<PlayingFieldBehavior>().gameStarted = true;
            text1.enabled = false;
            text2.text = "Both press Y to restart";
            text2.enabled = false;
            notJumped = false;
        }
        
            
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
        GameObject.Find("Score").GetComponent<ScoreCounter>().TakePoint(playerID);
        pos = GameObject.FindGameObjectWithTag("Respawn");
        drawLine(Vector3.zero);
        penalty = true;
    }

    public void reset()
    {
        notJumped = true;
    }
}
