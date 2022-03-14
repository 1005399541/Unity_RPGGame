using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    //技能面板类，单例模式

    public static SkillUI instance;

    private TweenPosition tween;
    private bool IsShow;

    public UIGrid grid;
    public GameObject SkillItemPrefab;

    private PlayerStatus ps;
    public int[] MagicianSkillIdList,SwordmanSkillIdList;    


    private void Awake()
    {
        instance = this;

        tween = this.GetComponent<TweenPosition>();
        IsShow = true;
    }
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tag.player).gameObject.GetComponent<PlayerStatus>();
        SetSkill();
       
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    private void Hide()
    {
        tween.PlayReverse();
        IsShow = true;
    }

    /// <summary>
    /// 显示
    /// </summary>
    private void Show()
    {
        tween.PlayForward();
        IsShow = false;
    }

    /// <summary>
    /// 技能面板的显示与隐藏
    /// </summary>
    public void SkillUITransformStatus()
    {
        if (IsShow)
        {
            Show();
            UpdateShow();
        }
        else
        {
            Hide();
        }
    }


    //设置技能
    private void SetSkill()
    {
        int[] skill = null;
        if (ps.hearType == HearType.Swordman)
        {
            skill = SwordmanSkillIdList;
        }
        else if(ps.hearType==HearType.Magician)
        {
            skill = MagicianSkillIdList;

        }
        foreach(int id in skill)
        {
            
            GameObject GO = NGUITools.AddChild(grid.gameObject, SkillItemPrefab);
            GO.transform.parent= grid.transform;
            GO.GetComponent<SkillItem>().GetId(id);
            grid.enabled = true;

        }
    }

    /// <summary>
    /// 更新技能的遮罩
    /// </summary>
    private void UpdateShow()
    {
        SkillItem[] skillItems = this.GetComponentsInChildren<SkillItem>();
        foreach(SkillItem Item in skillItems)
        {
            Item.GetLevel(ps.grade);
        }
    }
}
