using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
//using UnityEditor.SearchService;

public class EnemyNormalOther : MonoBehaviour
{
    //public GameObject enObj;
    //[SerializeField] EnemyNormal en;

    ////EnemyPatrol
    //[SerializeField] Transform player;
    //[SerializeField] float detectDistance;
    //public Transform[] points;
    //private int destPoint = 0;
    //NavMeshAgent agent;
    //bool IsDetected = false;

    //private void Start()
    //{
    //    en = enObj.GetComponent<EnemyNormal>();

    //    agent = GetComponent<NavMeshAgent>();

    //    GotoNextPoint();
    //}

    //private void Update()
    //{
    //    float distance;

    //    distance = Vector3.Distance(transform.position, player.position);

    //    if (distance <= detectDistance)
    //    {
    //        IsDetected = true;
    //    }
    //    else
    //    {
    //        IsDetected = false;
    //    }

    //    if (IsDetected)
    //    {
    //        agent.destination = player.position;
    //    }
    //    else
    //    {
    //        if (!agent.pathPending && agent.remainingDistance < 0.5f)
    //        {
    //            GotoNextPoint();
    //        }
    //    }
    //}

    //void GotoNextPoint()
    //{
    //    if (points.Length == 0)
    //    {
    //        return;
    //    }

    //    agent.destination = points[destPoint].position;

    //    destPoint = (destPoint + 1) % points.Length;
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "LightPillarNormal")
    //    {
    //        Destroy(this.gameObject);
    //        en.kill++;
    //        en.killCount.SetText("Enemy : {0} / 10", en.kill);
    //        en.normal++;
    //        if (en.normal == 2)
    //        {
    //            GameObject gObj = GameObject.Find("LightPillarNormal");
    //            Destroy(gObj);
    //        }
    //    }
    //}
}
