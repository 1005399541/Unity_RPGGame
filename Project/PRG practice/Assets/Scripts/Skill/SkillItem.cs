using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    //技能栏中单个技能的信息


    public int id;
    private SkillInfo skillInfo;

    private UISprite  IconSprite;
    private UILabel NameLable;
    private UILabel EffectTypeLable;
    private UILabel DesLable;
    private UILabel ConsumeMpLable;
    private UISprite Icon_Mask;//图片的遮罩


    /// <summary>
    /// 赋值技能初始信息
    /// </summary>
    private void InitialiseSkill()
    {

        IconSprite = transform.Find("iconname").gameObject.GetComponent<UISprite>();
        
        NameLable = transform.Find("Property_gb/name_gb/name").gameObject.GetComponent<UILabel>();
        
        EffectTypeLable = transform.Find("Property_gb/effecttype_gb/effecttype").gameObject.GetComponent<UILabel>();
        
        DesLable = transform.Find("Property_gb/des_gb/des").gameObject.GetComponent<UILabel>();
       
        ConsumeMpLable =transform.Find("Property_gb/consumeMP_gb/consumeMP").gameObject.GetComponent<UILabel>();

        Icon_Mask = transform.Find("IconMask").GetComponent<UISprite>();
        Icon_Mask.enabled = true;
        
    }


    /// <summary>
    /// 通过id找到技能信息，并赋值到技能面板
    /// </summary>
    public void GetId(int id)
    {
        InitialiseSkill();
        this.id = id;
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        //Debug.Log(skillInfo.icon_name+"+"+skillInfo.name+"+"+skillInfo.effectType+"+"+skillInfo.ConsumeMP);
        IconSprite.spriteName= skillInfo.icon_name;
        NameLable.text = skillInfo.name;
        switch (skillInfo.effectType)
        {
            case EffectType.Buff:  EffectTypeLable.text = "增益";    break;
            case EffectType.Passive: EffectTypeLable.text = "增强";  break;
            case EffectType.SingleTarget: EffectTypeLable.text = "单个目标"; break;
            case EffectType.MultiTarget:  EffectTypeLable.text = "多个目标"; break;
        }
        DesLable.text = skillInfo.Des;  
        ConsumeMpLable.text = skillInfo.ConsumeMP.ToString() +"MP";
        //Debug.Log(IconSprite.spriteName+"+"+NameLable.text+"+"+EffectTypeLable.text+"+"+ConsumeMpLable.text);
    }

    /// <summary>
    /// 得到技能所需的等级
    /// </summary>
    public void GetLevel(int level )
    {
       if(level>= skillInfo.NeedLevel)//技能可用
        {
            Icon_Mask.enabled = false;
            IconSprite.gameObject.GetComponentInParent<SkillIcon>().enabled = true;
        }
        else    //技能不可用
        {
            Icon_Mask.enabled = true;
            IconSprite.gameObject.GetComponentInParent<SkillIcon>().enabled = false;
        }
        
    }
}
