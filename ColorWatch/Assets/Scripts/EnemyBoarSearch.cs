/*using System.Collections;
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
    private Vector3 vector3;
    private Vector3 playerPos;
    private Vector3 targetPos;
    private bool getPos = true;
    private bool search = false;
    private bool lookFinish = false;

    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();

        GotoNextPoint();


        vector3.y = 0f;
    }

    private void Update()
    {
        playerPos = player.transform.position;

        //float distance;

        //distance = Vector3.Distance(transform.position, player.position);

        vector3 = playerPos - enemyBoar.transform.position;

        if (search &&!lookFinish)
        {
            AgentStan();
            //Vector3 vector3 = playerPos - enemyBoar.transform.position; //playerとboarの座標からベクトルを計算
            //vector3.y = 0f; //上下の回転しない
            Quaternion quaternion = Quaternion.LookRotation(vector3);
            //Quaternion quaternion = Quaternion.identity;
            enemyBoar.transform.rotation = quaternion;
        }
        if (!IsDetected && !search)
        {
            //Debug.Log("GoToNextPoint"); //ずっと呼ばれてる
            GotoNextPoint();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsDetected = true;

            var diff = playerPos - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (getPos == true)
                {
                    Debug.Log("aaaaaaaaaaaaaaaa");
                    Debug.Log("player");
                    getPos = false;
                    getPosition();
                    StartCoroutine("Attack");
                }
            }

            IsDetected = false;
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

    void Search()
    {
        AgentStan();
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        enemyBoar.transform.rotation = quaternion;
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

    IEnumerator Attack() //プレイヤーに突進する
    {
        //lookFinish = false;

        search = true;
        
        yield return new WaitForSeconds(1.5f);

        search = false;

        //lookFinish = true;

        if (hit.transform.gameObject == Player && !getPos && lookFinish)
        {
            Debug.Log("attack");
            AgentSpeedUp();
            agent.destination = targetPos;
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
*/

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


    private Vector3 detectedPosition; // 検知したプレイヤーの位置
    private bool isChasing = false; // プレイヤーの位置を追跡中かどうか
    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    /* private void Update()
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
             Rotate();
             agent.destination = playerPos;
         }
         else
         {
             if (!agent.pathPending && agent.remainingDistance < 0.5f)
             {
                 GotoNextPoint();
             }
         }
     }*/

    private void Update()
    {
        playerPos = player.transform.position;

        float distance = Vector3.Distance(transform.position, player.position);

        // 検知状態を更新
        if (distance <= detectDistance && !isChasing)
        {
            IsDetected = true;
            detectedPosition = playerPos; // プレイヤーを検知した瞬間の位置を記録
            isChasing = true; // 追跡状態に遷移
        }
        else if (!isChasing)
        {
            IsDetected = false;
        }

        if (IsDetected)
        {
            // 記録した位置に向かう
            RotateTowardsTarget(detectedPosition);
            agent.destination = detectedPosition;

            // 到着したら巡回に戻る
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                IsDetected = false;
                isChasing = false; // 追跡終了
                GotoNextPoint(); // 巡回再開
            }
        }
        else
        {
            // 巡回処理
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
            //targetPos = playerPos; //player�̌��ݒn�擾

            var diff = playerPos - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (getPos == true)
                {
                    getPos = false;
                    targetPos = playerPos;
                }

                //StartCoroutine("Rotate");

                agent.speed = 2;

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

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void RotateTowardsTarget(Vector3 targetPos)
    {
        // 目標の方向を計算（高さを無視）
        Vector3 direction = (targetPos - transform.position).normalized;
        direction.y = 0; // 上下方向の回転を無視

        // 現在の向きとターゲット方向の回転を補間
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        enemyBoar.transform.rotation = Quaternion.Slerp(enemyBoar.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}