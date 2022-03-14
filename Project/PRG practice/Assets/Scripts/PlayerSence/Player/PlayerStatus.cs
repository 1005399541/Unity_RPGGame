using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HearType
{
    //人物的所选角色的种类
    Magician,
    Swordman
}
public class PlayerStatus : MonoBehaviour
{
    //玩家的状态和面板信息

    public string playername; 
    public int  grade;//等级     每级所需经验 100+grade*50
    public int hp;//血量
    public int mp;//魔法
    public int currentHP;
    public int currentMP;
    public int coin=1000;//金币
    public int exp;

    public int attack;//初始攻击
    public int Attack_plus;//附加攻击（只指升级加的点数）
    public int defense;//初始防御
    public int defense_plus;//附加防御
    public int speed;//初始攻击速度
    public int speed_plus;//附加攻击速度
    public int remainPoint;//加状态的点数

    public HearType hearType;


    private PlayerAttack playerAttack;
    private PlayerDirection playerDirection;
    public void Awake()
    {
        //playername = "玩家";
        grade = 1;
        hp = 100;
        mp = 200;
        currentHP = 80;
        currentMP = 70;
        coin = 1000;
        exp = 0;
        attack = 20;
        defense = 5;
        speed = 1;
        remainPoint = 0;
        playerDirection = this.GetComponent<PlayerDirection>();
       playerAttack = this.GetComponent<PlayerAttack>();
    }
    private void Start()
    {
        ShowUpdateGrade(0);
    }

    /// <summary>
    /// 金币的改变
    /// </summary>
    public void CollectCoin(int change)
    {
        coin += change;
    }


    /// <summary>
    /// 属性的加点
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool GetPoint(int point=1)
    {
        if (remainPoint >= point)
        {
            remainPoint -= point;
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 仅得到金币的数量
    /// </summary>
    /// <returns></returns>
    public int GetCoin()
    {
        return coin;
    }


    //金币的使用
    public bool ChangeCoin(int coin)
    {
        if (this.coin>coin)
        {
            this.coin -= coin;
            return true;
        }

        return false;
    }

    //MP HP 的增加
    public void ChangeMpHp(int mp=0,int hp=0)
    {
        if (currentHP+hp >=this.hp)
        {
            currentHP = this.hp;
        }
        else
        {
            currentHP += hp;
        }
        if (currentMP + mp >= this.mp)
        {
            currentMP = this.mp;
        }
        else
        {
            currentMP += mp;
        }
        
    }

    /// <summary>
    /// 扣血
    /// </summary>
    public void ReduceHp(int reduce)
    {
        if (currentHP > reduce)
        {
            currentHP -= reduce;
        }
        else   //人物死亡
        {
            currentHP = 0;
        }
        ShowUpdateHPMP();
    }

    /// <summary>
    /// 血量与蓝条的更新
    /// </summary>
   private void ShowUpdateHPMP()
    {
        HeadUI.instance.UpdateShow();
    }


    /// <summary>
    /// 得到角色的总攻击点
    /// </summary>
    /// <returns></returns>
    public int AllTakeAttack(float rate)
    {
      
        this.attack =(int)(this.attack * rate);
        this.Attack_plus = (int)(this.Attack_plus* rate);
        EquipmentUI.instance.attack_pius = (int)(EquipmentUI.instance.attack_pius*rate);
        int allattack=attack + Attack_plus + EquipmentUI.instance.attack_pius;
        return allattack;
    }

    /// <summary>
    /// 得到角色的总防御点
    /// </summary>
    /// <returns></returns>
    public void AllTakeDefense(float rate)
    {
        
        this.defense = (int)(this.defense * rate);
        this.defense_plus = (int)(this.defense_plus * rate);
        EquipmentUI.instance.defense_pius = (int)(EquipmentUI.instance.defense_pius * rate);
       
    }

    /// <summary>
    /// 得到角色的总攻击速度点
    /// </summary>
    /// <returns></returns>
    public void AllTakeAttackSpeed(float rate)
    {
       
        this.speed = (int)(this.speed * rate);
        this.speed_plus = (int)(this.speed_plus * rate);
        EquipmentUI.instance.speed_plus = (int)(EquipmentUI.instance.speed_plus * rate);
     
    }


    /// <summary>
    /// 等级的提升，经验的更新
    /// </summary>
    public void ShowUpdateGrade(int getexp)
    {
        int  MaxExp=grade*30+100;
        exp += getexp;
        while (exp >= MaxExp)  //判断是否升级，如果是就升级
        {
            exp=exp - MaxExp;
            grade++;
            MaxExp = grade * 30 + 100;
        }
        ExpSeting.instance.GetandUpdateExp((float)exp/MaxExp,grade);
    }


    /// <summary>
    /// 得到现有蓝值
    /// </summary>
    /// <returns></returns>
    public bool TakeMP(int usemp)
    {
        if (currentMP<usemp)
        {
            return false;   //蓝不够
        }
        else
        {
            currentMP -= usemp;
            return true;
        }
    }
}
