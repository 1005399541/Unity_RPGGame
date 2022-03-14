using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 技能栏中的图片类型
/// </summary>
public enum ShortcutType
{
    Drug,
    Skill,
    None
}
public class ShortcutSkill : MonoBehaviour
{
    //单个快捷技能


    public KeyCode keyCode;

    private int id;
    private UISprite IconSprite;
    private ShortcutType shortcutType;

    SkillInfo skillinfo;
    ObjectInfo druginfo;

    private PlayerStatus ps;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        //IconSprite = transform.Find("Icon").GetComponent<UISprite>();
        //IconSprite.gameObject.SetActive(false);
        //shortcutType = ShortcutType.None;
        //playerAttack = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerAttack>();
    }


    private void Start()
    {
        IconSprite = transform.Find("Icon").GetComponent<UISprite>();
        IconSprite.gameObject.SetActive(false);
        shortcutType = ShortcutType.None;
        playerAttack = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerAttack>();

        ps = GameObject.FindWithTag(Tag.player).GetComponent<PlayerStatus>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            
            //使用药品
            if (shortcutType == ShortcutType.Drug)
            {
                UseDrug(druginfo.mp,druginfo.hp);
               
            }

            //使用技能
            if (shortcutType == ShortcutType.Skill)
            {
                //先判断蓝是否够
                bool mp = ps.TakeMP(skillinfo.ConsumeMP);
                if (!mp)
                {

                }
                else    //蓝够，释放技能
                {
                    playerAttack.UseSkill(skillinfo);
                }
            }

        }
    }


    /// <summary>
    /// 通过技能id设置技能图标
    /// </summary>
    /// <param name="id"></param>
    public void SetSkillId(int id)
    {
        skillinfo = SkillsInfo.instance.GetSkillInfoById(id);
        IconSprite.gameObject.SetActive(true);
        IconSprite.spriteName = skillinfo.icon_name;
        shortcutType = ShortcutType.Skill;

    }
    /// <summary>
    /// 通过药品id设置技能图标
    /// </summary>
    public void SetDrugId(int id,int count=1)
    {
        this.id = id;
        druginfo = ObjectsInfo.instance.GetObjectInfoByid(id);
        IconSprite.gameObject.SetActive(true);
        IconSprite.spriteName = druginfo.icon_name;
        shortcutType = ShortcutType.Drug;
    }


    /// <summary>
    /// 使用药品
    /// </summary>
    /// <param name="mp"></param>
    /// <param name="hp"></param>
    public void UseDrug(int mp,int  hp)
    {
        bool outnumber;
        bool success=Inventory.instance.OnUseDrug(id,out outnumber);
        Debug.Log(success);
        if (success)
        {
            ps.ChangeMpHp(mp, hp);
            HeadUI.instance.UpdateShow();
        }

        if (outnumber)
        {
            IconSprite.gameObject.SetActive(false);
            shortcutType = ShortcutType.None;

        }
         
      
    }
}
