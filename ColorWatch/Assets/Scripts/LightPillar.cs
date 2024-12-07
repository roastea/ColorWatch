using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy; //敵を入れる配列
    [SerializeField] GameObject LightPillarManager;

    int rnd; //乱数を入れるint型
    [HideInInspector] public int normal = 0, shy = 0, boar = 0, octopus = 0, tutorial = 0; //敵のカウント

    List<int> enemyList = new List<int>(); //乱数が重複しないようにリストを使う

    void Start()
    {
        for (int i = 0; i < Enemy.Length; i++)
        {
            enemyList.Add(i);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rnd = Random.Range(0, enemyList.Count); //ランダムな要素を取得
            Destroy(Enemy[enemyList[rnd]]); //オブジェクトを削除
            enemyList.RemoveAt(rnd); //リストから削除

            //柱の名前「LightPillarNormal」などで倒した敵のカウントを増やす
            if (this.gameObject.name == "LightPillarNormal")
                normal++;
            else if (this.gameObject.name == "LightPillarShy")
                shy++;
            else if (this.gameObject.name == "LightPillarBoar")
                boar++;
            else if (this.gameObject.name == "LightPillarOctopus")
                octopus++;
            else
                tutorial++;

                if (enemyList.Count > 0)
            {
                LightPillarManager.GetComponent<LightPillarManager>().ChangePillarPoint(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
