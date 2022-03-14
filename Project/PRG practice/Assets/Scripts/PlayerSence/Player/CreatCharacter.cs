using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatCharacter : MonoBehaviour
{
    //根据第二场景的人物和名字，生成角色和名字

    private int playerindex=-1;
    private string playername;
    public GameObject magic;
    public GameObject Sword;
    private bool Iscreat=false;

    private PlayerStatus playerStatus;

    private void Awake()
    {
        playerindex = PlayerPrefs.GetInt("SelectCharacterIndex");
        if (playerindex != -1 && !Iscreat)
        {
           
            if (playerindex == 0)
            {
                GameObject go = Instantiate(magic, transform.position, Quaternion.identity);
                playerStatus = go.GetComponent<PlayerStatus>();
                playerStatus.playername = PlayerPrefs.GetString("SelecterCharacterName");
                //HeadUI.instance.UpdateShow();
                Iscreat = true;
            }
            else
            {
                GameObject go = Instantiate(Sword, transform.position, Quaternion.identity);
                playerStatus = go.GetComponent<PlayerStatus>();
                playerStatus.playername = PlayerPrefs.GetString("SelecterCharacterName");
                //HeadUI.instance.UpdateShow();
                Iscreat = true;
            }
        }

    }





}
