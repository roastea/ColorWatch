using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnColorShy : MonoBehaviour
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
        if (lightPillarScript.shy == 0)
        {
            GetComponent<Renderer>().material = changed;
        }
    }
}
