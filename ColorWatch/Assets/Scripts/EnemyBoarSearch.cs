using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoarSearch : MonoBehaviour
{
    //EnemyPatrol
    NavMeshAgent agent;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    bool IsDetected = false;

    //EnemySearch
    public Transform player;
    public GameObject Player;
    public GameObject enemyBoar;
    private RaycastHit hit;
    private Vector3 playerPos;
    private Vector3 targetPos;
    private bool getPos = true;

    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void Update()
    {
        //Debug.Log(agent.speed);

        playerPos = player.transform.position;

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
            //agent.destination = targetPos;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //targetPos = playerPos; //playerの現在地取得

            var diff = playerPos - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (getPos == true)
                {
                    getPos = false;
                    targetPos = playerPos;
                    Invoke("getPosition", 3.0f);
                }


                StartCoroutine("Rotate");

                agent.speed = 2;

                //if (hit.transform.gameObject == Player && getPos == false)
                //{
                //    AgentSpeedUp();
                //    agent.destination = targetPos;
                //    //AgentStan();
                //    Invoke("AgentSpeedDown", 2.0f);
                //    getPos = true;
                //}
                //else
                //{
                //    AgentSpeedDown();
                //    GotoNextPoint();
                //}
            }
        }
    }

    void AgentSpeedUp()
    {
        agent.speed = 10;
    }

    void AgentSpeedDown()
    {
        agent.speed = 2;
    }

    void AgentStan()
    {
        agent.speed = 0;
    }

    void getPosition()
    {
        targetPos = playerPos;
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

    IEnumerator Rotate() //プレイヤーの方向を見る
    {
        agent.speed = 0;
        Vector3 vector3 = targetPos - enemyBoar.transform.position; //playerとboarの座標からベクトルを計算
        vector3.y = 0f; //上下の回転しない
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        //Quaternion quaternion = Quaternion.identity;
        enemyBoar.transform.rotation = quaternion;
        yield return new WaitForSeconds(2.0f);


        if (hit.transform.gameObject == Player && getPos == false)
        {
            AgentSpeedUp();
            agent.destination = targetPos;
            //AgentStan();
            Invoke("AgentSpeedDown", 2.0f);
            getPos = true;
        }
        else
        {
            AgentSpeedDown();
            GotoNextPoint();
        }
    }
}
