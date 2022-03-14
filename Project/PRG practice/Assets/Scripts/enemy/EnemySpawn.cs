using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //小狼孵化器

    public int  maxcount;
    public int currentcount;

    public GameObject enemybabyPrefab;

    private void Awake()
    {
        maxcount = 6;
        currentcount = 0;
    }
    private void Update()
    {
        if (maxcount>currentcount)
        {
            Vector3 pos = transform.position;
            pos.x +=Random.Range(-8,8);
            pos.z +=Random.Range(-8,8);
            GameObject go = GameObject.Instantiate(enemybabyPrefab,pos,Quaternion.identity);
            go.transform.parent = this.gameObject.transform;
            currentcount++;
        }
    }
    public void countReduce()
    {
        currentcount--;
    }
}
