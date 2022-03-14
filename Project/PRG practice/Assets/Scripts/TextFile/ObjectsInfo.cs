using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 物品信息管理类
/// </summary>
public class ObjectsInfo : MonoBehaviour
{
    //物品信息管理类
    public static ObjectsInfo instance;
    public TextAsset GameObjectListText;
    private Dictionary<int, ObjectInfo> ObjectInfoDir = new Dictionary<int, ObjectInfo>();
    private void Awake()
    {
        instance = this;
        ReadObjectInfo();
        print("背包道具数目"+ObjectInfoDir.Keys.Count);
      
    }
    
    /// <summary>
    /// 通过键名找物品数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ObjectInfo GetObjectInfoByid(int id)
    {
        ObjectInfo info = null;
        ObjectInfoDir.TryGetValue(id, out info);
        return info;
    }

    /// <summary>
    /// 读取物品信息
    /// </summary>
    void ReadObjectInfo()
    {
        string text = GameObjectListText.text;
        string[] strArray = text.Split('\n');
  
        foreach(string str in strArray)
        {
            ObjectInfo info = new ObjectInfo();
            string[] proArray = str.Split(',');
             int id = int.Parse(proArray[0]);
             
   
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
            ObjectType objectType = ObjectType.drug;
            switch (str_type)
            {
                case "drug": objectType = ObjectType.drug; break;
                case "equip": objectType = ObjectType.equip; break;
                case "mat": objectType = ObjectType.mat; break;
            }
            info.id = id; info.name = name; info.icon_name = icon_name; info.objecttype = objectType;
            if (objectType == ObjectType.drug)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int sell = int.Parse(proArray[6]);
                int bug = int.Parse(proArray[7]);
                info.hp = hp;info.mp = mp;info.sell = sell;info.buy = bug;
            }
            else if (objectType == ObjectType.equip)
            {
                int attack = int.Parse(proArray[4]);
                int defense = int.Parse(proArray[5]);
                int speed = int.Parse(proArray[6]);
                int sell = int.Parse(proArray[9]);
                int buy = int.Parse(proArray[10]);
                info.attack = attack;info.defense = defense;info.speed = speed;
                info.sell = sell;info.buy = buy;
                string dresstype = proArray[7];
                switch (dresstype)
                {
                    case "Headgear": info.dressType = DressType.Headgear; break;
                    case "Armor": info.dressType = DressType.Armor;break;
                    case "RightHand": info.dressType = DressType.RightHand; break;
                    case "LeftHand": info.dressType = DressType.LeftHand; break;
                    case "Shoe": info.dressType = DressType.Shoe; break;
                    case "Accessory": info.dressType = DressType.Accessory; break;
                }
                string charactertype = proArray[8];
                switch (charactertype)
                {
                    case "Magician": info.roleType = RoleType.Magician; break;
                    case "Swordman": info.roleType = RoleType.Swordman; break;
                    case "Common": info.roleType = RoleType.Common; break;

                }
            }

            ObjectInfoDir.Add(id, info);//在字典中以id为键名info为值，储存数据
        }
    }

}

//  0     1       2            3            4           5           6         7             8           9      10
// id;  名称;  icon名称;  类型(Drug);   加血量值;   加魔法值;    出售价;    购买价;
//  
// id   名称;   icon名称; 类型(Equip);  加伤害值;   加防御值;   加速度值;   穿戴类型;   适用类型;    出售价;  购买价;     
//  
// id  名称;    icon名称; 类型(mat);     出售价;      购买价;      
//   
//   类型（drug,equip,mat）
//1000  药品
//2000  装备
//3000  材料

public enum ObjectType
{
    //物品类型
    drug,
    equip,
    mat
}

public enum DressType
{
    //穿戴类型
    Headgear,        //帽子
    Armor,           //甲
    RightHand, //
    LeftHand,  //
    Shoe,            //鞋子
    Accessory        //饰品

}
public enum RoleType
{
    //适用角色类型
    Magician,    //魔法师
    Swordman,    //剑士
    Common       //通用
}
public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name;
    public ObjectType objecttype;
    public int hp;
    public int mp;
    public int sell;
    public int buy;

    public  RoleType roleType;
    public  DressType dressType;
    public int attack;
    public int defense;
    public int speed;
}
