using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    //EnemyPatrol
    [SerializeField] Transform player;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool IsDetected = false;

    //Count(life & kill)
    public TextMeshProUGUI lifeCount;
    public TextMeshProUGUI killCount;
    public int life;
    public int kill;

    //Color
    [SerializeField] GameObject obj_g;
    [SerializeField] Material m_g;
    int green = 0;

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
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            life--;
            lifeCount.SetText("Life : {0}", life);
        }
        if(other.CompareTag("LightPillar"))
        {
            Destroy(this.gameObject);
            kill++;
            killCount.SetText("Enemy : {0} / 15", kill);
            if(other.gameObject.name=="LightPillarPink")
            {
                Debug.Log("other.gameObject.name==LightPillarPink");
                green++;
                if(green == 1){
                    GameObject obj = GameObject.Find("LightPillarPink");
                    Destroy(obj);
                    obj_g.GetComponent<Renderer>().material = m_g;
                }
            }
        }
    }
}
