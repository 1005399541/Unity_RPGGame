using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Grid : MonoBehaviour
{
    //背包网格储存物品的id，物品的图片，数量等
    public int id_Item;//这个物品栏中物品的id，id若为0则无物品
    private ObjectInfo Info;//这个物品的信息
    public int number_Item;//物品的数量
    private UILabel Number_label;
    private void Start()
    {
        id_Item = 0;
        Info = null;
        number_Item = 0;
        Number_label = this.GetComponentInChildren<UILabel>();
    }


    public void SetId(int id,int number=1)
    {
        id_Item = id;
        Info = ObjectsInfo.instance.GetObjectInfoByid(id);
        Inventory_Item item = this.GetComponentInChildren<Inventory_Item>();
        item.SetIconName(id,Info.icon_name);
        Number_label.enabled = true; ;
        number_Item = number;
        Number_label.text = number.ToString();
    }

    /// <summary>
    /// 清除物品单元格的物品信息
    /// </summary>
    public void ClearInfo()
    {
        id_Item = 0;
        number_Item = 0;
        Number_label.text = number_Item.ToString();
        Number_label.enabled = false;
    }


    /// <summary>
    /// 控制物品数量的增加
    /// </summary>
    public void PlusNumber(int number=1)
    {
        number_Item+=number;
        Number_label.text = number_Item.ToString();
    }

    /// <summary>
    /// 装备道具到装备栏，并减少背包中的装备道具
    /// </summary>
    /// <param name="reducenumber"></param>
    public bool EquipProp(GameObject gameObject, int reducenumber = 1)
    {
       
        number_Item -= reducenumber;
        if (number_Item <= 0)
        {

            GameObject.Destroy(gameObject);
            ClearInfo();
            return true; ;
        }
        
        Number_label.text = number_Item.ToString();
        return false;
    }


}
