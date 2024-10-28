using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEditor.SearchService;

public class EnemyNormal : MonoBehaviour
{
    //EnemyPatrol
    [SerializeField] Transform player;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool IsDetected = false;

    //Count(kill)
    public TextMeshProUGUI killCount;
    public int kill;

    //Color
    [SerializeField] GameObject obj;
    [SerializeField] Material m;
    int normal = 0;

    private void Start()
    {
        kill = 0;

        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    private void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectDistance)
        {
            IsDetected = true;
        }
        else
        {
            IsDetected = false;
        }

        if (IsDetected)
        {
            agent.destination = player.position;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPillar"))
        {
            Destroy(this.gameObject);
            kill++;
            killCount.SetText("Enemy : {0} / 15", kill);
            if (other.gameObject.name == "LightPillarNormal")
            {
                normal++;
                if (normal == 4)
                {
                    GameObject gObj = GameObject.Find("LightPillarNormal");
                    Destroy(gObj);
                    obj.GetComponent<Renderer>().material = m; //オブジェクト複数個になるからタグで判別の方がいいかも
                }
            }
        }
    }
}
