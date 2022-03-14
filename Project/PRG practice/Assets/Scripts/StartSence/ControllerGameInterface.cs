using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGameInterface : MonoBehaviour
{

    //控制进入游戏界面

    private bool IsPressKey;//是否按下任意键
    private GameObject StartGameContainer;//获取进入游戏界面的父物体

    void Start()
    {
        IsPressKey = false;
        StartGameContainer = this.transform.parent.Find("StartGameContainer").gameObject;
    }

  
    void Update()
    {
        ControlerGameInterface();
    }



    /// <summary>
    /// 控制按键文字的消失，游戏登陆界面的出现
    /// </summary>
    void ControlerGameInterface()
    {
        if (IsPressKey == false)
        {
            if (Input.anyKey)
            {
                this.gameObject.SetActive(IsPressKey);
                StartGameContainer.SetActive(!IsPressKey);
            }
        }
    }
}
