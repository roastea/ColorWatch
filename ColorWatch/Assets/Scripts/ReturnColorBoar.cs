using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnColorBoar : MonoBehaviour
{
    public GameObject ebObj;
    [SerializeField] EnemyBoar eb;

    [SerializeField] Material changed;

    private void Start()
    {
        eb = ebObj.GetComponent<EnemyBoar>();
    }

    private void Update()
    {
        if (eb.boar == 4)
        {
            GetComponent<Renderer>().material = changed;
        }
    }
}
