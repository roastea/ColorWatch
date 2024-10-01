//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using TMPro;

//public class Enemy3 : MonoBehaviour
//{
//    Enemy e;

//    //EnemyPatrol
//    [SerializeField] Transform player;
//    [SerializeField] float detectDistance;
//    public Transform[] points;
//    private int destPoint = 0;
//    NavMeshAgent agent;
//    bool IsDetected = false;

//    //Color
//    [SerializeField] GameObject obj_p;
//    [SerializeField] Material m_p;
//    int pink = 0;

//    //public float speed = 5f;
//    //private Vector3 target;
//    //protected GameObject player;

//    //void Start()
//    //{
//    //    Rigidbody rb = this.transform.GetComponent<Rigidbody>();

//    //    player = GameObject.FindWithTag("Player");
//    //    target = player.transform.position;
//    //    if(target.x<this.transform.position.x)
//    //    {
//    //        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
//    //    }
//    //}

//    //void Update()
//    //{
//    //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y), speed * Time.deltaTime);
//    //}

//    private void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        GotoNextPoint();
//    }

//    private void Update()
//    {
//        float distance;

//        distance = Vector3.Distance(transform.position, player.position);

//        if (distance <= detectDistance)
//        {
//            IsDetected = true;
//        }
//        else
//        {
//            IsDetected = false;
//        }

//        if (IsDetected)
//        {
//            agent.destination = player.position;
//        }
//        else
//        {
//            if (!agent.pathPending && agent.remainingDistance < 0.5f)
//            {
//                GotoNextPoint();
//            }
//        }
//    }

//    void GotoNextPoint()
//    {
//        if (points.Length == 0)
//        {
//            return;
//        }

//        agent.destination = points[destPoint].position;

//        destPoint = (destPoint + 1) % points.Length;
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            e.life--;
//            e.lifeCount.SetText("Life : {0}", e.life);
//        }
//        if (other.CompareTag("LightPillar"))
//        {
//            Destroy(this.gameObject);
//            e.kill++;
//            e.killCount.SetText("Enemy : {0} / 15", e.kill);
//            if (other.gameObject.name == "LightPillarPink")
//            {
//                Debug.Log("other.gameObject.name==LightPillarPink");
//                pink++;
//                if (pink == 1)
//                {
//                    GameObject obj = GameObject.Find("LightPillarPink");
//                    Destroy(obj);
//                    obj_p.GetComponent<Renderer>().material = m_p;
//                }
//            }
//        }
//    }
//}
