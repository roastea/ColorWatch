using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnColor : MonoBehaviour //敵の残りの数表示と色戻すためのスクリプト
{
    public GameObject enObj;
    public GameObject ebObj;
    public GameObject esObj;
    public GameObject eoObj;

    public EnemyNormal en;
    public EnemyBoar eb;
    public EnemyShy es;
    public EnemyOctopus eo;

    private bool normalFlag = false;
    private bool boarFlag = false;
    private bool shyFlag = false;
    private bool octopusFlag = false;

    private Material greenMaterial;
    private Material redMaterial;
    private Material blueMaterial;
    private Material yellowMaterial;

    private void Start()
    {
        en = enObj.GetComponent<EnemyNormal>();
        eb = enObj.GetComponent<EnemyBoar>();
        es = enObj.GetComponent<EnemyShy>();
        eo = enObj.GetComponent<EnemyOctopus>();

        greenMaterial = Resources.Load<Material>("GreenMaterial");
        redMaterial = Resources.Load<Material>("RedMaterial");
        blueMaterial = Resources.Load<Material>("BlueMaterial");
        yellowMaterial = Resources.Load<Material>("YellowMaterial");

        greenMaterial.shader = Shader.Find("Unlit/BlackLight");
        redMaterial.shader = Shader.Find("Unlit/BlackLight");
        blueMaterial.shader = Shader.Find("Unlit/BlackLight");
        yellowMaterial.shader = Shader.Find("Unlit/BlackLight");
    }

    private void Update()
    {
        if (en.normal == 0 && eb.boar == 0 && es.shy == 0 && eo.octopus == 0) //全ての敵が消えたらクリア
        {
            SceneManager.LoadScene("ClearScene");
        }

        if (!normalFlag && en.normal == 0)
        {
            normalFlag = true;
            ReturnColorNormal();
        }
        if (!boarFlag && eb.boar == 0)
        {
            boarFlag = true;
            ReturnColorBoar();
        }
        if (!shyFlag && es.shy == 0)
        {
            shyFlag = true;
            ReturnColorShy();
        }
        if (!octopusFlag && eo.octopus == 0)
        {
            octopusFlag = true;
            ReturnColorOctopus();
        }
    }

    private void ReturnColorNormal()
    {
        greenMaterial.shader = Shader.Find("Standard");
    }

    private void ReturnColorBoar()
    {
        redMaterial.shader = Shader.Find("Standard");
    }
    private void ReturnColorShy()
    {
        blueMaterial.shader = Shader.Find("Standard");
    }
    private void ReturnColorOctopus()
    {
        yellowMaterial.shader = Shader.Find("Standard");
    }
}
