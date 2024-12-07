using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy; //敵を入れる配列
    [SerializeField] GameObject LightPillarManager;

    int rnd; //乱数を入れるint型

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
