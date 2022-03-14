using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    //技能信息管理类
    public static SkillsInfo instance;    //单例模式

    public TextAsset SkillsText; 

    private Dictionary<int, SkillInfo> Dict = new Dictionary<int, SkillInfo>();
    private void Awake()
    {
        instance = this;
        ReadSkilllsText();
        print("技能数目"+Dict.Keys.Count);
    }

    /// <summary>
    /// 通过id找到对应技能数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo skillInfo = new SkillInfo();
        Dict.TryGetValue(id, out skillInfo);
        return skillInfo;
    }

    /// <summary>
    /// 读取技能文本文件的信息
    /// </summary>
    private void ReadSkilllsText()
    {
        string text = SkillsText.text;
        string[] strArray = text.Split('\n');
        foreach(string str in strArray)
        {
            SkillInfo skillInfo = new SkillInfo();
            string[] proArray = str.Split(',');
            int id;                 id=int.Parse(proArray[0]);                       
            string name;            name = proArray[1];
            string icon_name;       icon_name = proArray[2];
            string describe;        describe = proArray[3];
            skillInfo.id = id; skillInfo.name = name; skillInfo.icon_name = icon_name; skillInfo.Des = describe;
            string effecttype=proArray[4];
            switch (effecttype)
            {
                case "Passive":      skillInfo.effectType = EffectType.Passive;      break;
                case "Buff":         skillInfo.effectType = EffectType.Buff;         break;
                case "SingleTarget": skillInfo.effectType = EffectType.SingleTarget; break;
                case "MultiTarget":  skillInfo.effectType = EffectType.MultiTarget;  break;
                    
            }

            string effectproperty = proArray[5];
            switch (effectproperty)
            {
                case "Attack":         skillInfo.effectProerty = EffectProerty.Attack;      break;
                case "Defense":        skillInfo.effectProerty = EffectProerty.Defense;     break;
                case "Speed":          skillInfo.effectProerty = EffectProerty.Speed;       break;
                case "AttackSpeed":    skillInfo.effectProerty = EffectProerty.AttackSpeed; break;
                case "HP":             skillInfo.effectProerty = EffectProerty.HP;          break; 
                case "MP":             skillInfo.effectProerty = EffectProerty.MP;          break;
            }

            int propertyvalue;      propertyvalue=int.Parse(proArray[6]);
            int effecttime;         effecttime = int.Parse(proArray[7]);
            int consumMP;           consumMP = int.Parse(proArray[8]);      
            int coolingtime;        coolingtime = int.Parse(proArray[9]);
            skillInfo.ProertyValue = propertyvalue;skillInfo.EffectTime = effecttime;
            skillInfo.ConsumeMP = consumMP; skillInfo.CoolingTime = coolingtime;


            string effectrole=proArray[10];
            switch (effectrole)
            {
                case "Swordman":    skillInfo.effectRole = EffectRole.Swordman;  break;
                case "magician":    skillInfo.effectRole = EffectRole.magician;  break;
                
            }
            int needlevel;   needlevel =int.Parse(proArray[11]);
            int distance;    distance = int.Parse(proArray[13]);
            skillInfo.NeedLevel = needlevel;     skillInfo.Distance = distance;
            string releasetype; releasetype =proArray[12];
            switch (releasetype)
            {
                case "Self":       skillInfo.releaseType = ReleaseTYpe.Self;     break;
                case "Enemy":      skillInfo.releaseType = ReleaseTYpe.Enemy;    break;
                case "Position":   skillInfo.releaseType = ReleaseTYpe.Position; break;
            }
            string effname;   effname = proArray[14];
            string animname;  animname = proArray[15];
            float animtime;   animtime =float.Parse( proArray[16]);
            skillInfo.effectname = effname;
            skillInfo.animname = animname;
            skillInfo.animtime = animtime;

            Dict.Add(id, skillInfo);   //在字典中以id为键名info为值，储存数据
        }
    }
}



//  0     1       2              3          4          5       6         7           8             9        10           11          12         13         14         15          16
// id   名称  名称（icon）  描述（Des） 作用类型   作用属性  作用值  作用时间s   消耗魔法值   冷却时间s   适用角色   适用等级    释放类型    释放距离   特效名称    动画名称   动画时间
//
//
//适用角色（EffectRole）     Swordman , magician
// 
//作用类型（EffectType）    增加（加HP,MP）Passive,   增益 （加伤害，防御，移动速度，攻击速度） Buff,（SingleTarget）单个目标，（MultiTarget）多个目标
//
//作用属性（EffectProerty）     Attack，Defense，Speed，AttackSpeed，HP，MP
//
//释放类型（ReleaseTYpe）   Self（当前位置释放），Enemy（指定敌人释放），Position（指定位置释放）
//

/// <summary>
/// 作用类型 增加（加HP,MP）Passive,   增益 （加伤害，防御，移动速度，攻击速度） Buff,（SingleTarget）单个目标，（MultiTarget）多个目标
/// </summary>
public enum EffectType
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}

/// <summary>
/// 作用属性     Attack，Defense，Speed，AttackSpeed，HP，MP
/// </summary>
public enum EffectProerty
{
    Attack,
    Defense,
    Speed,
    AttackSpeed,
    HP,
    MP
}
/// <summary>
/// 适用角色     Swordman , magician
/// </summary>
public enum EffectRole
{
    Swordman,
    magician
}

/// <summary>
/// 释放类型   Self（当前位置释放），Enemy（指定敌人释放），Position（指定位置释放）
/// </summary>
public enum ReleaseTYpe
{
    Self,
    Enemy,
    Position
}
/// <summary>
/// 单个技能信息
/// </summary>
public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string Des;
    public EffectType effectType;
    public EffectProerty effectProerty;
    public int ProertyValue;
    public int EffectTime;
    public int ConsumeMP;//消耗的魔法值
    public int CoolingTime;//冷却时间
    public EffectRole effectRole;
    public int NeedLevel;//所需等级
    public ReleaseTYpe releaseType;
    public int Distance;
    public string effectname;
    public string animname;
    public float animtime;


}
