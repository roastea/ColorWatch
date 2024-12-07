using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
//using UnityEditor.SearchService;

public class EnemyOctopus : MonoBehaviour
{
    public GameObject enObj;
    [SerializeField] EnemyNormal en;

    //EnemyPatrol
    [SerializeField] Transform player;
    //[SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;

    //Count(kill)
    //public TextMeshProUGUI killCount;
    //public int kill;

    public int octopus;

    private void Start()
    {
        en = enObj.GetComponent<EnemyNormal>();

        agent = GetComponent<NavMeshAgent>();

        agent.speed = 5.0f;

        GotoNextPoint();
    }

    private void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
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

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "LightPillarNormal")
    //    {
    //        Destroy(this.gameObject);
    //        en.kill++;
    //        en.killCount.SetText("Enemy : {0} / 10", en.kill);
    //        octopus++;
    //        if (octopus == 2)
    //        {
    //            GameObject gObj = GameObject.Find("LightPillarNormal");
    //            Destroy(gObj);
    //        }
    //    }
    //}
}
