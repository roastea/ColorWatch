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
    //bool IsDetected = false;

    //EnemySearch
    public Transform player;
    public GameObject Player;
    public GameObject enemyBoar;
    private RaycastHit hit;
    private Vector3 playerPos;
    private Vector3 targetPos;
    private bool getPos = true;
    private bool doAttack = false;
    private bool looking = true;


    private Vector3 detectedPosition; // 検知したプレイヤーの位置
    //private bool isChasing = false; // プレイヤーの位置を追跡中かどうか
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

        //if(!agent.pathPending && !looking && doAttack)
        //{
        //    doAttack = false;
        //}

        //float distance = Vector3.Distance(transform.position, player.position);

        //// 検知状態を更新
        //if (distance <= detectDistance && !isChasing)
        //{
        //    IsDetected = true;
        //    detectedPosition = targetPos; // プレイヤーを検知した瞬間の位置を記録
        //    isChasing = true; // 追跡状態に遷移
        //}
        //else if (!isChasing)
        //{
        //    IsDetected = false;
        //}

        //if (IsDetected)
        //{
        //    // 記録した位置に向かう
        //    RotateTowardsTarget(detectedPosition);
        //    agent.destination = detectedPosition;

        //    // 到着したら巡回に戻る
        //    if (!agent.pathPending && agent.remainingDistance < 0.5f)
        //    {
        //        //targetPos = playerPos;
        //        IsDetected = false;
        //        isChasing = false; // 追跡終了
        //        GotoNextPoint(); // 巡回再開
        //    }
        //}
        //else
        //{
        //    // 巡回処理
        //    if (!agent.pathPending && agent.remainingDistance < 0.5f)
        //    {
        //        GotoNextPoint();
        //    }
        //}
    }

    private void OnTriggerStay(Collider other) //索敵範囲内に入ってる間
    {
        if (other.CompareTag("Player")) //それがplayerだったら
        {
            var diff = playerPos - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance)) //playerがrayの範囲内だったら
            {
                //if (getPos == true) //到着したらgetPos=trueにしたい
                //{
                //    Debug.Log("1");

                //    getPos = false;
                //    targetPos = playerPos;
                //}

                if (hit.transform.gameObject == Player) //それがplayerだったら
                {
                    Debug.Log("2");

                    //speed=0
                    //1.5秒間見つめる

                    if(looking)
                    {
                        Debug.Log("3");

                        AgentStan();

                        // 目標の方向を計算（高さを無視）
                        Vector3 vector3 = (playerPos - transform.position).normalized;
                        vector3.y = 0; // 上下方向の回転を無視

                        // 現在の向きとターゲット方向の回転を補間
                        Quaternion targetRotation = Quaternion.LookRotation(vector3);
                        enemyBoar.transform.rotation = Quaternion.Slerp(enemyBoar.transform.rotation, targetRotation, Time.deltaTime * 5f);
                        
                        StartCoroutine("Attack"); //中身は1.5秒後に実行だけど、以下のコードがすぐに実行されちゃう
                    }

                    if (doAttack) //からboolで判定
                    {
                        if (getPos == true) //到着したらgetPos=trueにしたい
                        {
                            Debug.Log("1");

                            getPos = false;
                            targetPos = playerPos;
                        }

                        //speed=10
                        //最終playerがいた位置に向かう


                        AgentSpeedUp();
                        agent.destination = targetPos;

                        if (!agent.pathPending && agent.remainingDistance < 0.5f) //到着したら1秒スタンして巡回に戻る
                        {
                            //speed=0
                            //1秒間スタンする(視点＆speed)
                            AgentStan();
                            Invoke("AgentSpeed", 1.0f);
                            getPos = true;
                        }

                        //search = true;
                        //AgentSpeedUp();
                        //agent.destination = targetPos;
                        //Invoke("AgentSpeedDown", 2.0f);S
                        //getPos = true;

                    }
                    else
                    {

                    }
                }
                else
                {
                    //speed=2
                    //GotoNextPoint();

                    //search = false;
                    //AgentSpeedDown();
                    //GotoNextPoint();
                }
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
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
                    //targetPos = playerPos;
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
    }*/

    void AgentSpeedUp()
    {
        agent.speed = 10;
    }

    void AgentSpeed()
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

    IEnumerator Attack() //突進
    {
        Debug.Log("4");

        doAttack = false;

        yield return new WaitForSeconds(1.5f);

        Debug.Log("5");

        looking = false;
        doAttack = true;
    }
}