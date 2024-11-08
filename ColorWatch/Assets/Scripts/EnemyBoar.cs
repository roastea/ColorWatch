using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyBoar : MonoBehaviour
{
    public GameObject enObj;
    [SerializeField] EnemyNormal en;

    public int boar;

    private void Start()
    {
        en = enObj.GetComponent<EnemyNormal>();
        boar = 0;
    }

    private void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LightPillarBoar")
        {
            Destroy(this.gameObject);
            en.kill++;
            en.killCount.SetText("Enemy : {0} / 10", en.kill);
            boar++;
            if (boar == 3)
            {
                GameObject gObj = GameObject.Find("LightPillarBoar");
                Destroy(gObj);
            }
        }
    }
}
