using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    //武器商人的单个物品

    private int id;
    private ObjectInfo info;

    private UISprite weaponIcon;
    private UILabel weaponName;
    private UILabel weaponEffect;
    private UILabel weaponPrice;

    private void Awake()
    {
        weaponIcon = transform.Find("Icon").GetComponent<UISprite>();
        weaponName = transform.Find("Name/NameLabel").gameObject.GetComponent<UILabel>();
        weaponEffect = transform.Find("Effect/EffectLabel").gameObject.GetComponent<UILabel>();
        weaponPrice = transform.Find("BugValue/BugValueLabel").gameObject.GetComponent<UILabel>();
    }


    /// <summary>
    /// 通过id来设置武器信息
    /// </summary>
    /// <param name="id"></param>
    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoByid(id);


        weaponIcon.spriteName = info.icon_name;
        weaponName.text = info.name;
        string text = "";
        if (info.attack > 0)
        {
            text += "攻击+" + info.attack.ToString();
        }
        if (info.speed > 0)
        {
            text += "速度+" + info.speed.ToString();
        }
        if (info.defense > 0)
        {
            text += "防御+" + info.defense.ToString();
        }
        weaponEffect.text = text;
        weaponPrice.text = info.buy.ToString();

    }



    /// <summary>
    /// ok按钮的调用
    /// </summary>
    public void OnokButtonClick()
    {
        WeaponShopUI.instance.OkButton(this.id);
    }

  
}
