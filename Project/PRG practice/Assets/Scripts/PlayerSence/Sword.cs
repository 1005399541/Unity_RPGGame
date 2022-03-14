using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attack = 0;
    private List<int> enemyList = new List<int>();

    private PlayerStatus playerStatus;
    private void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerStatus>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.enemy)
        {
            int id=other.GetInstanceID();
            int index = enemyList.IndexOf(id);
            if (index == -1)
            {
                switch (other.gameObject.name)
                {
                    case "WolfBaby(Clone)":
                        other.GetComponent<WolfBaby>().Hurt(playerStatus.AllTakeAttack(1)*attack );
                        break;
                    case "WolfBoss(Clone)":
                        other.GetComponent<WolfBoss>().Hurt(playerStatus.AllTakeAttack(1)*attack);
                        break;
                    case "WolfNormal(Clone)":
                        other.GetComponent<WolfNormal>().Hurt(playerStatus.AllTakeAttack(1)*attack);
                        break;
                }
                enemyList.Add(id);
            }

        }
    }
    
    
}
