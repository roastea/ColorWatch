using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class EnemyShySearch : MonoBehaviour
{
    //EnemyPatrol
    NavMeshAgent agent;

    //[SerializeField] float detectDistance;
    //public Transform[] points;
    //private int destPoint = 0;

    //EnemySearch
    //float speed = 5;
    public Transform player;
    //public Transform ebPos;
    public GameObject enemyShy;
    private RaycastHit hit;
    private Vector3 playerPos;
    private Vector3 target;

    //public GameObject blacklightobject;
    //[SerializeField] BlackLightScript blacklightscript;

    private bool playerLook = false; //プレイヤーが見てるかどうか
    private bool shyLook = false; //Shyの視界の範囲か


    void Start()
    {
        agent = enemyShy.GetComponent<NavMeshAgent>();

        //blacklightscript = blacklightobject.GetComponent<BlackLightScript>();
    }

    private void Update()
    {
        playerPos = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlackLight")) // && shyLook)
        {
            playerLook = true;
            //Debug.Log("入った");
            agent.speed = 0;
            //agent.angularSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BlackLight"))
        {
            playerLook = false;
            //Debug.Log("出た");
            agent.speed = 3;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(playerLook && shyLook)
        {
            //agent.speed = 0;
        }
        else
        {
            //agent.speed = 3;

            if (other.CompareTag("Player")) //ok
            {
                target = playerPos;

                var diff = target - transform.position;
                var distance = diff.magnitude;
                var direction = diff.normalized;

                if (Physics.Raycast(transform.position, direction, out hit, distance)) //ok
                {
                    //ebPos.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y), speed * Time.deltaTime);

                    if (hit.transform.gameObject == player) //反応してない
                    {
                        Debug.Log("touch!");
                        shyLook = true;

                        agent.destination = target;
                        agent.speed = 3;
                    }
                    else
                    {
                        shyLook = false;
                        //GotoNextPoint();
                    }
                }
            }
        }
    }

    //void GotoNextPoint()
    //{
    //    if (points.Length == 0)
    //    {
    //        return;
    //    }

    //    agent.destination = points[destPoint].position;

    //    destPoint = (destPoint + 1) % points.Length;
    //}
}
