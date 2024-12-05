using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LightPillarManager : MonoBehaviour
{
    [SerializeField] GameObject[] PillarPoint; //スポーン位置を入れる配列
    [SerializeField] GameObject[] LightPillar; //光の柱を入れる配列

    int rnd; //乱数を入れるint型
    int nowPillarNormal;
    int nowPillarShy;
    int nowPillarBoar;

    List<GameObject> PillarList = new List<GameObject>();
    List<GameObject> nowPillarList = new List<GameObject>();

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

            nowPillarList.Add(PillarList[rnd]); //使用中に追加
            Debug.Log(PillarList[rnd].name);

            if (LightPillar[i].name == "LightPillarNormal")
                nowPillarNormal = nowPillarList.IndexOf(PillarList[rnd]); //Normalの柱の場所を記憶
            else if (LightPillar[i].name == "LightPillarShy")
                nowPillarShy = nowPillarList.IndexOf(PillarList[rnd]); //Shyの柱の場所を記憶
            else if (LightPillar[i].name == "LightPillarBoar")
                nowPillarBoar = nowPillarList.IndexOf(PillarList[rnd]); //Boarの柱の場所を記憶

            PillarList.Remove(PillarList[rnd]); //スポーン位置から削除
        }
    }

    //光の柱の位置の変更
    public void ChangePillarPoint(GameObject setPillar)
    {
        int index = 0;

        if (setPillar.name == "LightPillarNormal")
            index = nowPillarNormal;
        else if (setPillar.name == "LightPillarShy")
            index = nowPillarShy;
        else if (setPillar.name == "LightPillarBoar")
            index = nowPillarBoar;

        rnd = Random.Range(0, PillarList.Count);
        setPillar.transform.position = PillarList[rnd].transform.position; //位置を変更

        nowPillarList.Add(PillarList[rnd]); //使用中に追加
        PillarList.Add(nowPillarList[index]); //元あった位置をスポーン位置に追加
        nowPillarList.Remove(nowPillarList[index]); //元あった位置を使用中から削除

        if (setPillar.name == "LightPillarNormal")
            nowPillarNormal = nowPillarList.IndexOf(PillarList[rnd]); //Normalの柱の場所を記憶
        else if (setPillar.name == "LightPillarShy")
            nowPillarShy = nowPillarList.IndexOf(PillarList[rnd]); //Shyの柱の場所を記憶
        else if (setPillar.name == "LightPillarBoar")
            nowPillarBoar = nowPillarList.IndexOf(PillarList[rnd]); //Boarの柱の場所を記憶

        PillarList.Remove(PillarList[rnd]); //スポーン位置から削除
    }
}
