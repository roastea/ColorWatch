using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public TextMeshProUGUI lifeCount;
    public TextMeshProUGUI killCount;
    public int life;
    public int kill;

    private void Start()
    {
        life = 3;
        kill = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("life -1");
            life--;
            lifeCount.SetText("Life : {0}", life);
        }
        if(other.CompareTag("LightPillar"))
        {
            Debug.Log("Enemy is dead.");
            Destroy(this.gameObject);
            kill++;
            killCount.SetText("Enemy : {0} / 15", kill);
        }
    }
}
