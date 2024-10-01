using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public TextMeshProUGUI lifeCount;
    public TextMeshProUGUI killCount;
    public int life;
    public int kill;

    //敵の巡回、追跡のための宣言
    [SerializeField] Transform player;
    [SerializeField] float detectDistance; //探知距離
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool IsDetected = false;

    private void Start()
    {
        life = 3;
        kill = 0;

        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    private void Update()
    {
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
            agent.destination = player.position;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void GotoNextPoint()
    {
        //地点が何も設定されてない時に返す
        if (points.Length == 0)
        {
            return;
        }

        //設定された目的地に向かう
        agent.destination = points[destPoint].position;

        //次の目的地の設定
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("life -1");
            life--;
            lifeCount.SetText("Life : {0}", life);
        }
        if(other.CompareTag("LightPillar"))
        {
            Debug.Log("Enemy is dead.");
            Destroy(this.gameObject);
            kill++;
            killCount.SetText("Enemy : {0} / 15", kill);
        }
    }
}
