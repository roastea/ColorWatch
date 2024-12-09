using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class EnemyBoarSearch : MonoBehaviour
{
    //EnemyPatrol
    NavMeshAgent agent;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;

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
    private bool isChasing = false; // プレイヤーの位置を追跡中かどうか

    //FrameEffect
    [SerializeField] GameObject FrameEffect;
    bool EffectFlag = false;

    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void Update()
    {
        playerPos = player.transform.position;

        if(!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //FreameEffectのスクリプトに飛んでエフェクトを終了
            if (EffectFlag)
            {
                FrameEffect.GetComponent<FrameEffect>().StopEffect();
                EffectFlag = false;
            }

            GotoNextPoint();
        }
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
                if (hit.transform.gameObject == Player) //それがplayerだったら
                {
                    //FreameEffectのスクリプトに飛んでエフェクトを再生
                    if (!EffectFlag)
                    {
                        FrameEffect.GetComponent<FrameEffect>().PlayEffect();
                        EffectFlag = true;
                    }

                    isChasing = true;

                    //speed=0
                    //1.5秒間見つめる

                    if(looking)
                    {
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
                        if (getPos == true) //到着したらgetPos=true
                        {
                            getPos = false;
                            targetPos = playerPos;
                        }

                        //speed=10
                        //最終playerがいた位置に向かう

                        AgentSpeedUp();
                        agent.destination = targetPos;
                        Invoke("Arrived", 2.0f);

                        //ここがプレイヤーが範囲内に入った時に呼ばれてる
                        //if (!agent.pathPending && agent.remainingDistance < 0.1f) //到着したら1秒スタンして巡回に戻る
                        //{
                        //    //speed=0
                        //    //1秒間スタンする(視点＆speed)
                        //    AgentStan();
                        //    Invoke("AgentSpeed", 1.0f);
                        //    getPos = true;
                        //    looking = true;
                        //    isChasing = false;
                        //    Debug.Log("false");
                        //}
                    }
                }
            }
        }
    }

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

    void Arrived()
    {
        //speed=0
        //1秒間スタンする
        AgentStan();
        Invoke("AgentSpeed", 1.0f);
        getPos = true;
        looking = true;
        isChasing = false;
    }

    IEnumerator Attack() //突進
    {
        doAttack = false;

        yield return new WaitForSeconds(2.0f);

        looking = false;
        doAttack = true;
    }

    //やられる時エフェクトを止める
    public void DestroyEffect()
    {
        if (EffectFlag)
        {
            //FreameEffectのスクリプトに飛んでエフェクトを終了
            FrameEffect.GetComponent<FrameEffect>().StopEffect();
        }
    }
}