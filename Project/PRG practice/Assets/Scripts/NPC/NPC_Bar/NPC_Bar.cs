using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Bar :NPC
{

    public static NPC_Bar instance;
    
    public TweenPosition tweenPosition;
    public GameObject OkButton;//ok按钮
    public GameObject AcceptButton;//Accept按钮
    public GameObject CancelButton;//Cancel按钮
    public UILabel TaskDes;//任务描述

    public bool IsTasking;//是否在任务中；
    public int Killnumber;//任务完成的数量

    private PlayerStatus playerStatus;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        hideGameObjece();
        OkButton.gameObject.SetActive(false);
        AcceptButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
        playerStatus = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerStatus>();
      
    }

    /// <summary> 
    ///  打开任务面板
    /// </summary>
    public void OnMouseOver()
    {
        // 系统提供的方法，鼠标停留在ui碰撞器上时每帧调用
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTasking)
            {
                OnTaskDescription();
            }
            else { OutTaskDescription(); }
            ShowQuest();
        }

    }


    /// <summary>
    /// 显示任务面板
    /// </summary>
    void ShowQuest()
    {
        tweenPosition.gameObject.SetActive(true);
        tweenPosition.PlayForward();
    }

    void hideGameObjece()
    {
        tweenPosition.gameObject.SetActive(false);
    }



    /// <summary>
    /// 关闭任务面板，X，cancel的监听
    /// </summary>
   public void HideQuest()
    {
        tweenPosition.PlayReverse();

        Invoke("hideGameObjece", 2);
    }


    /// <summary>
    /// Accept按钮的监听,接受任务
    /// </summary>
    public void AcceptTask()
    {
        OnTaskDescription();
        IsTasking = true;
    }


    /// <summary>
    /// Ol的监听，完成任务
    /// </summary>
   public void FinishTask()
    {
        if (Killnumber >= 10)
        {
            Killnumber =0;
            playerStatus.CollectCoin(100);
            Inventory.instance.UpdateCoin();
            OutTaskDescription();
        }
        else
        {
            tweenPosition.PlayReverse();
            Invoke("hideGameObjece", 2);
        }
    }

   /// <summary>
   /// 任务进行时的描述
   /// </summary>
   void OnTaskDescription()
    {
        TaskDes.text = "任务：\n杀死了" + Killnumber + "/10只狼。\n\n奖励：\n100金币。";
        OkButton.gameObject.SetActive(true);
        AcceptButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);

    }


    /// <summary>
    /// 任务未开始的描述
    /// </summary>
    void OutTaskDescription()
    {
        TaskDes.text = "任务：\n杀死"+Killnumber+"/10只狼。\n\n奖励：\n100金币。";
        OkButton.gameObject.SetActive(false);
        AcceptButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);

    }

    public void addKillcount()
    {
        if (IsTasking)
        {
            Killnumber++;
        }
    }
}
