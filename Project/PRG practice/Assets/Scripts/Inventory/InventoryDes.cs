using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDes : MonoBehaviour
{
    //鼠标停留在物品上时物品信息的显示
    
    public static InventoryDes instance;

    private  UILabel ItemDes;
    private float timer;//信息显示的时间
    
   public void Awake()
    {
        instance=this;
        timer = 0;
        ItemDes = this.gameObject.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);


    }
    private void Update()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            timer -= Time.deltaTime;

        }
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }



    //得到物品的信息
    public void GetDes(int id)
    {
        this.gameObject.SetActive(true);timer = 0.2f;
       transform.position= UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoByid(id);
        string str = "";
        switch (info.objecttype)
        {
            case ObjectType.drug:  str= GetDrugDes(info);break;
            case ObjectType.equip: str= GetEquipmentDes(info);break;
        }

        ItemDes.text = str;
    }



    /// <summary>
    /// 获得的是药品信息
    /// </summary>
      string GetDrugDes(ObjectInfo info)
    {
        //0   id;
        //1   名称;
        //2   icon名称;
        //3   类型（drug,equip,mat）;
        //4   加血量值;
        //5   加魔法值;
        //6   出售价;
        //7   购买价;
        string str = "";
        str += "药品  " + "\n";
        str += "名称: " + info.name + "\n";
        str += "HP: " + info.hp + "\n";
        str += "MP: " + info.mp + "\n";
        str += "buy: " + info.buy + "\n";
        str += "sell: " + info.sell;

        return str;
    }


    /// <summary>
    /// 得到装备的信息
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    string GetEquipmentDes(ObjectInfo info)
    {
    
        string str = "";
        str += "装备  " + "\n";
        str += "适用人物" + info.roleType+"\n";
        str += "名称: " + info.name + "\n";
        str += "攻击值: " + info.attack + "\n";
        str += "防御值:" + info.defense + "\n";
        str += "速度值:" + info.speed + "\n";
        str += "出售价:" + info.sell + "\n";
        str += "购买价 " + info.buy;

        return str;
    }
}
