using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormaClear : MonoBehaviour
{
    public GameObject enObj;
    [SerializeField] EnemyNormal en;

    bool isClearFlag = false;

    [SerializeField] GameObject ClearZone;
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    private void Start()
    {
        en = enObj.GetComponent<EnemyNormal>();
    }

    private void Update()
    {
        if(en.kill==10)
        {
            if(!isClearFlag)
            {
                isClearFlag = true;
                Instantiate(ClearZone);
                Destroy(door1);
                Destroy(door2);
            }
        }
    }
}
