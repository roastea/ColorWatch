using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoarSearch : MonoBehaviour
{
    //EnemyPatrol
    public Transform[] points;
    private int destPoint = 0;

    //EnemySearch
    public GameObject enemyBoar;
    [SerializeField] Transform player;
    NavMeshAgent agent;
    private RaycastHit hit;
    private Vector3 target;

    void Start()
    {
        InvokeRepeating("Position", 0f, 5.0f);
        agent = enemyBoar.GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Player = GameObject.Find("Player");

            var diff = target - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (hit.transform.gameObject == Player)
                {
                    //agent.isStopped = false;
                    agent.destination = target;

                }
                else
                {
                    //agent.isStopped = true;
                    GotoNextPoint();
                }
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

    void Position()
    {
        target = player.transform.position;
    }
}
