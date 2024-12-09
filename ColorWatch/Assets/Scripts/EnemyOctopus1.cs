using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
//using UnityEditor.SearchService;

public class EnemyOctopus1 : MonoBehaviour
{
    //EnemyPatrol
    [SerializeField] Transform player;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;

    private void Start()
    {
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
