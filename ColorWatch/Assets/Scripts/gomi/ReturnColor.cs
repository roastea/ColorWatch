using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnColor : MonoBehaviour //色戻すためのスクリプト
{
    public GameObject lightPillarObj;
    public LightPillar lightPillar;

    private bool normalFlag = false;
    private bool boarFlag = false;
    private bool shyFlag = false;
    private bool octopusFlag = false;

    //private Material greenMaterial;
    //private Material redMaterial;
    //private Material blueMaterial;
    //private Material yellowMaterial;

    public GameObject[] greenObj;
    public GameObject[] redObj;
    public GameObject[] blueObj;
    public GameObject[] yellowObj;

    private void Start()
    {
        lightPillar = lightPillarObj.GetComponent<LightPillar>();

        //greenMaterial = Resources.Load<Material>("GreenMaterial");
        //redMaterial = Resources.Load<Material>("RedMaterial");
        //blueMaterial = Resources.Load<Material>("BlueMaterial");
        //yellowMaterial = Resources.Load<Material>("YellowMaterial");

        //greenMaterial.shader = Shader.Find("Unlit/BlackLight");
        //redMaterial.shader = Shader.Find("Unlit/BlackLight");
        //blueMaterial.shader = Shader.Find("Unlit/BlackLight");
        //yellowMaterial.shader = Shader.Find("Unlit/BlackLight");

        for (int i = 0; i < greenObj.Length; i++)
        {
            greenObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Unlit/BlackLight");
        }
        for (int i = 0; i < redObj.Length; i++)
        {
            redObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Unlit/BlackLight");
        }
        for (int i = 0; i < blueObj.Length; i++)
        {
            blueObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Unlit/BlackLight");
        }
        for (int i = 0; i < yellowObj.Length; i++)
        {
            yellowObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Unlit/BlackLight");
        }
    }

    private void Update()
    {
        if (lightPillar.normal == 0 && lightPillar.boar == 0 && lightPillar.shy == 0 && lightPillar.octopus == 0) //全ての敵が消えたらクリア
        {
            SceneManager.LoadScene("ClearScene");
        }

        if (normalFlag == false && lightPillar.normal == 0)
        {
            normalFlag = true;
            ReturnColorNormal();
        }
        if (!boarFlag && lightPillar.boar == 0)
        {
            boarFlag = true;
            ReturnColorBoar();
        }
        if (!shyFlag && lightPillar.shy == 0)
        {
            shyFlag = true;
            ReturnColorShy();
        }
        if (!octopusFlag && lightPillar.octopus == 0)
        {
            octopusFlag = true;
            ReturnColorOctopus();
        }
    }

    private void ReturnColorNormal()
    {
        //greenMaterial.shader = Shader.Find("Standard");
        for(int i=0; i<greenObj.Length; i++)
        {
            greenObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }

    private void ReturnColorBoar()
    {
        //redMaterial.shader = Shader.Find("Standard");
        for (int i = 0; i < redObj.Length; i++)
        {
            redObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }
    private void ReturnColorShy()
    {
        //blueMaterial.shader = Shader.Find("Standard");
        for (int i = 0; i < blueObj.Length; i++)
        {
            blueObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }
    private void ReturnColorOctopus()
    {
        //yellowMaterial.shader = Shader.Find("Standard");
        for (int i = 0; i < yellowObj.Length; i++)
        {
            yellowObj[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }
}
