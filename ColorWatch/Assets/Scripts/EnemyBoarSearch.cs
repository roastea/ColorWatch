using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoarSearch : MonoBehaviour
{
    public GameObject enemyBoar;
    [SerializeField] Transform player;
    NavMeshAgent agent;
    private RaycastHit hit;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Position", 0f, 5.0f);
        agent = enemyBoar.GetComponent<NavMeshAgent>();
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
                    //target = player.transform.position;
                    agent.isStopped = false;
                    agent.destination = target;

                }
                else
                {
                    agent.isStopped = true;
                }
            }
        }
    }

    void Position()
    {
        target = player.transform.position;
    }
}
