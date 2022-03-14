using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI instance;

    private TweenPosition tween;
    private bool IsShow;//是否显示


    private GameObject Headgear;
    private GameObject Armor;
    private GameObject Right_Hand;
    private GameObject Left_Hand;
    private GameObject Shoe;
    private GameObject Accessory;

    public GameObject EquipmentItem;

    public int attack_pius;//装备附加的攻击
    public int defense_pius;//装备附加的防御
    public int speed_plus;//装备附加的速度

    private PlayerStatus ps;
    public void Awake()
    {
        instance = this;
        IsShow = true;
        attack_pius = 0;
        defense_pius = 0;
        speed_plus = 0;
        tween = this.gameObject.GetComponent<TweenPosition>();
        Headgear = GameObject.Find("Headgear").gameObject;
        Armor = GameObject.Find("Armor").gameObject;
        Right_Hand = GameObject.Find("RightHand").gameObject;
        Left_Hand = GameObject.Find("LeftHand").gameObject;
        Shoe = GameObject.Find("Shoe").gameObject;
        Accessory = GameObject.Find("Accessory").gameObject;

        ps = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerStatus>();
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
    /// 隐藏
    /// </summary>
    private void Hide()
    {
        tween.PlayReverse();
        IsShow = true;
    }

    /// <summary>
    /// 装备栏的显示与隐藏
    /// </summary>
    public void EquipmentUITransformStaus()
    {
        if (IsShow == true)
        {
            Show();
        }
        else Hide();
    }


    /// <summary>
    /// 装备物品，成功装备返回true，失败返回false
    /// </summary>
    public bool EquipProp(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoByid(id);
        if (info.objecttype != ObjectType.equip) { return false; }
        if (ps.hearType==HearType.Magician&&info.roleType==RoleType.Swordman)
        {
            return false;
        }
        if (ps.hearType == HearType.Swordman && info.roleType == RoleType.Magician)
        {
            return false;
        }
        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear: parent=Headgear;break;
            case DressType.Armor: parent = Armor;break;
            case DressType.LeftHand: parent = Left_Hand;break;
            case DressType.RightHand: parent = Right_Hand;break;
            case DressType.Shoe: parent = Shoe;break;
            case DressType.Accessory: parent = Accessory;break;
        }
        Equipment_Item Item= parent.gameObject.GetComponentInChildren<Equipment_Item>();
        if (Item!=null)
        {//这个物品栏已经装备了物品
          
            parent.gameObject.GetComponentInChildren<Equipment_Item>().SetInfo(info);

        }
        else
        {
            //这个物品栏没有装备物品
            GameObject GO = NGUITools.AddChild(parent, EquipmentItem);
            GO.transform.localPosition = Vector3.zero;
            GO.GetComponent<Equipment_Item>().SetInfo(info);
        }





       return true;
    }


    /// <summary>
    /// 装备的总附加值，并更新装备的总属性
    /// </summary>
    public void UpdateProperty()
    {
        attack_pius = 0;
        defense_pius = 0;
        speed_plus = 0;

        Equipment_Item headgear = Headgear.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(headgear);
        Equipment_Item armor = Armor.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(armor);
        Equipment_Item leftHand = Left_Hand.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(leftHand);
        Equipment_Item rightHand = Right_Hand.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(rightHand);
        Equipment_Item shoe = Shoe.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(shoe);
        Equipment_Item accessory = Accessory.gameObject.GetComponentInChildren<Equipment_Item>();
        EquipmentProperty(accessory);
        Debug.Log("攻击"+attack_pius);
        Debug.Log("防御"+defense_pius);
        Debug.Log("速度" +speed_plus);
   
    }


    /// <summary>
    /// 每件装备的附加属性
    /// </summary>
    /// <param name="item"></param>
    private void EquipmentProperty(Equipment_Item item)
    {
        if (item != null)
        {
            ObjectInfo equipmentinfo = ObjectsInfo.instance.GetObjectInfoByid(item.id);
            attack_pius += equipmentinfo.attack;
            defense_pius += equipmentinfo.defense;
            speed_plus += equipmentinfo.speed;
        }
    }


    /// <summary>
    /// 销毁装备栏中的物品，生成到物品栏，并更新显示
    /// </summary>
    public void DestoryEquipAndUpdeateShow(GameObject gameObject,int id)
    {
        Inventory.instance.PickUpCollect_ByGetid(id);
        GameObject.DestroyImmediate(gameObject);
        EquipmentUI.instance.UpdateProperty();
    }



}
