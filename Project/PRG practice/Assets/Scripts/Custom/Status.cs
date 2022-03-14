using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Status : MonoBehaviour
{
    //场景中人物状态的显示
    public static Status instance;

    private TweenPosition tween;
    private bool IsShow;

    private UILabel AttackLable;
    private UILabel DefenseLable;
    private UILabel SpeedLable;
    private UILabel RemainPointLable;
    private UILabel SummaryLable;

    private GameObject Attack_Button;
    private GameObject Defense_Button;
    private GameObject Speed_Button;

    private PlayerStatus playerStatus;

    
    public void Awake()
    {
        instance = this;

        IsShow = true;

        AttackLable =GameObject.Find("AttackPoint").GetComponent<UILabel>();
        
        DefenseLable = GameObject.Find("DefensePoint").GetComponent<UILabel>();
        SpeedLable = GameObject.Find("SpeedPoint").GetComponent<UILabel>();
        RemainPointLable= GameObject.Find("RemainPoint").GetComponent<UILabel>();
        SummaryLable= GameObject.Find("Sum").GetComponent<UILabel>();
        Attack_Button = GameObject.Find("AttackButton").gameObject;
        Defense_Button = GameObject.Find("DefenseButton").gameObject;
        Speed_Button = GameObject.Find("SpeedButton");
        playerStatus = GameObject.FindWithTag(Tag.player).GetComponent<PlayerStatus>();
        tween = this.gameObject.GetComponent<TweenPosition>();
    }


    /// <summary>
    /// 更新角色状态的信息
    /// </summary>
    void UpdateStatus()
    {
        AttackLable.text = playerStatus.attack +"+"+ playerStatus.Attack_plus;
        DefenseLable.text = playerStatus.defense+"+"+ playerStatus.defense_plus;
        SpeedLable.text = playerStatus.speed +"+"+ playerStatus.speed_plus;
        SummaryLable.text = "攻击： "+(playerStatus.attack + playerStatus.Attack_plus).ToString() + "\n" +
                            "防御： "+(playerStatus.defense + playerStatus.defense_plus).ToString() + "\n" +
                            "速度： "+(playerStatus.speed + playerStatus.speed_plus).ToString();
        RemainPointLable.text = playerStatus.remainPoint.ToString();
        if (playerStatus.remainPoint >= 1)
        {
            Attack_Button.SetActive(true);
            Defense_Button.SetActive(true);
            Speed_Button.SetActive(true);
        }
        else
        {
            Attack_Button.SetActive(false);
            Defense_Button.SetActive(false);
            Speed_Button.SetActive(false);
        }
    }



    /// <summary>
    /// 状态的隐藏
    /// </summary>
    private void Hide()
    {
        IsShow = true;
        tween.PlayReverse();
    }
    /// <summary>
    /// 状态的显示
    /// </summary>
    private void Show()
    {
        IsShow = false;
        tween.PlayForward();
    }
    /// <summary>
    /// 状态的显示与隐藏
    /// </summary>
    public void TransformStatus()
    {
        if (IsShow == true)
        {
            Show();
            
        }
        else Hide();
        UpdateStatus();
    }


    /// <summary>
    /// 加点攻击
    /// </summary>
   public void OnAttackPointButton()
    {
        bool IsGet=playerStatus.GetPoint();
        if (IsGet == true)
        {
            playerStatus.Attack_plus += 1;
            UpdateStatus();
        }
    }
    /// <summary>
    /// 加点防御
    /// </summary>
    public void OnDefensePointButton()
    {
        bool IsGet = playerStatus.GetPoint();
        if (IsGet == true)
        {
            playerStatus.defense_plus += 1;
            UpdateStatus();
        }
    }
    /// <summary>
    /// 加点速度
    /// </summary>
    public void OnSpeedPointButton()
    {
        bool IsGet = playerStatus.GetPoint();
        if (IsGet == true)
        {
            playerStatus.speed_plus += 1;
            UpdateStatus();
        }
    }
}
