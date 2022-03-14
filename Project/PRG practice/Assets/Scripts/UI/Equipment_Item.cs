using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Item : MonoBehaviour
{

    //装备栏里的物品类
    private UISprite UISprite_Equipment;

    public int id;

    private bool IsHover;//是否鼠标以上

    private void Awake()
    {
        UISprite_Equipment = this.GetComponent<UISprite>();
        UISprite_Equipment.spriteName = null;
        IsHover = false;
        id = 0;
    }
    private void Update()
    {
        OnHover(IsHover);
        if (IsHover&&Input.GetMouseButtonDown(1))
        {//鼠标移入装备栏上的物品并按下右键
            EquipmentUI.instance.DestoryEquipAndUpdeateShow(this.gameObject, id);
        }
    }


    /// <summary>
    /// 通过id装备到物品栏
    /// </summary>
    public void SetId(int id)
    {
        
        ObjectInfo info=ObjectsInfo.instance.GetObjectInfoByid(id);
        SetInfo(info);
    }

    /// <summary>
    /// 通过info装备到物品栏
    /// </summary>
    public void SetInfo(ObjectInfo info)
    {
        if (id != 0) { Inventory.instance.PickUpCollect_ByGetid(id); }
        id = info.id;
        UISprite_Equipment.spriteName = info.icon_name;
    }


   /// <summary>
   /// NGUI中鼠标移入UI物体上时的方法
   /// </summary>
   /// <param name="IsOver"></param>
    private void OnHover(bool IsOver)
    {
        IsHover=IsOver;
    }
}
