using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LightPillarManager : MonoBehaviour
{
    [SerializeField] GameObject[] PillarPoint; //スポーン位置を入れる配列
    [SerializeField] GameObject[] LightPillar; //光の柱を入れる配列
    GameObject nowPillarNormal;
    GameObject nowPillarShy;
    GameObject nowPillarBoar;
    GameObject nowPillarOctopus;

    int rnd; //乱数を入れるint型

    List<GameObject> PillarList = new List<GameObject>();

    void Start()
    {
        //スポーン位置を配列に入れる
        for (int i = 0; i < PillarPoint.Length; i++)
        {
            PillarList.Add(PillarPoint[i]);
        }

        //光の柱をランダムな位置に入れる
        for (int i = 0; i < LightPillar.Length; i++)
        {
            rnd = Random.Range(0, PillarList.Count);
            LightPillar[i].transform.position = PillarList[rnd].transform.position; //位置を変更

            //Debug.Log(PillarList[rnd].name);

            if (LightPillar[i].name == "LightPillarNormal") //Normalの柱の場所を記憶
                nowPillarNormal = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarShy") //Shyの柱の場所を記憶
                nowPillarShy = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarBoar") //Boarの柱の場所を記憶
                nowPillarBoar = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarOctopus") //Octopusの柱の場所を記憶
                nowPillarOctopus = PillarList[rnd];

                PillarList.Remove(PillarList[rnd]); //スポーン位置から削除
        }
    }

    //光の柱の位置の変更
    public void ChangePillarPoint(GameObject setPillar)
    {
        rnd = Random.Range(0, PillarList.Count);
        setPillar.transform.position = PillarList[rnd].transform.position; //位置を変更

        if (setPillar.name == "LightPillarNormal")
        {
            PillarList.Add(nowPillarNormal); //元あった柱の場所をスポーン位置に追加
            nowPillarNormal = PillarList[rnd]; //Normalの柱の場所を記憶
        }
        else if (setPillar.name == "LightPillarShy")
        {
            PillarList.Add(nowPillarShy); //元あった柱の場所をスポーン位置に追加
            nowPillarShy = PillarList[rnd]; //Shyの柱の場所を記憶
        }
        else if (setPillar.name == "LightPillarBoar")
        {
            PillarList.Add(nowPillarBoar); //元あった柱の場所をスポーン位置に追加
            nowPillarBoar = PillarList[rnd]; //Boarの柱の場所を記憶
        }
        else if (setPillar.name == "LightPillarOctopus")
        {
            PillarList.Add(nowPillarOctopus);  //元あった柱の場所をスポーン位置に追加
            nowPillarOctopus = PillarList[rnd]; //Octopusの柱の場所を記憶
        }

        PillarList.Remove(PillarList[rnd]); //新しい柱の位置をスポーン位置から削除
    }
}
