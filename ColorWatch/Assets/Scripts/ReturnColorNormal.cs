using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnColorNormal : MonoBehaviour
{
    public GameObject lightPillarObj;
    [SerializeField] LightPillar lightPillarScript;

    [SerializeField] Material changed;

    private void Start()
    {
        lightPillarScript = lightPillarObj.GetComponent<LightPillar>();
    }

    private void Update()
    {
        if (lightPillarScript.normal == 0)
        {
            GetComponent<Renderer>().material = changed;
        }
    }
}
