using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item :UIDragDropItem
{
    private UISprite ui_sprite;
    private bool IsHover;
    private int id;
    
   

    protected override void Awake()
    {
        base.Awake();
        IsHover = false;
        ui_sprite = this.GetComponent<UISprite>();
    }

    protected override void Update()
    {
        base.Update();
        if (IsHover)
        {
            if (Input.GetMouseButtonDown(1)) //若按下鼠标右键则装备物品到装备栏
            {
               bool IsSucceed   =EquipmentUI.instance.EquipProp(id);
                if (IsSucceed)
                {
                    EquipmentUI.instance.UpdateProperty();
                    Inventory_Item_Grid grid = this.gameObject.GetComponentInParent<Inventory_Item_Grid>();
                    grid.EquipProp(this.gameObject);
                    
                }
                
            }

            InventoryDes.instance.GetDes(id);
            //是否显示信息
           
            
        }
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)//拖拽后，拖拽表面不为空
        { Debug.Log(surface.tag);
            if (surface.tag == Tag.Inventory_Item_Grid)//拖拽后，拖拽表面为本身或空格子
            {
                if (surface == this.transform.parent.gameObject)//拖拽后，拖拽表面为本身
                {
                   
                }
                else//拖拽后，拖拽表面为空格子
                {
                    Inventory_Item_Grid oldgrid = this.transform.parent.gameObject.GetComponent<Inventory_Item_Grid>();
                    Inventory_Item_Grid surfacegrid = surface.GetComponent<Inventory_Item_Grid>();
                    int id = oldgrid.id_Item;int number = oldgrid.number_Item;
                    this.transform.parent = surface.transform;
                    surfacegrid.SetId(id,number);
                    oldgrid.ClearInfo();
                    
                }
            }
            if (surface.tag == Tag.Inventory_Item)//拖拽后，表面为另一个物品
            {
                Inventory_Item_Grid oldgrid = this.transform.parent.gameObject.GetComponent<Inventory_Item_Grid>();
                Inventory_Item_Grid surfacegrid = surface.transform.parent.gameObject.GetComponent<Inventory_Item_Grid>();
                int oldid = oldgrid.id_Item;int oldnumber = oldgrid.number_Item;
                int newid = surfacegrid.id_Item;int newnumber = surfacegrid.number_Item;
                oldgrid.SetId(newid, newnumber);
                surfacegrid.SetId(oldid,oldnumber);
                //Debug.Log("dddd");
            }
            if (surface.tag == Tag.Shortcut)
            {
                surface.gameObject.GetComponent<ShortcutSkill>().SetDrugId(id);
            }
        }



        SetPosition();
    }



    /// <summary>
    /// 物品位置归为中心
    /// </summary>
    void SetPosition()
    {
        this.transform.localPosition = Vector3.zero;
    }


    public void SetId(int id)
    {
        ObjectInfo objectInfo = ObjectsInfo.instance.GetObjectInfoByid(id);
        ui_sprite.spriteName = objectInfo.icon_name;
    }


    public void SetIconName( int id,string icon_name)
    {
        ui_sprite.spriteName = icon_name;
        this.id = id;
    }


    /// <summary>
    /// 鼠标滑入该物体，显示物品信息
    /// </summary>
    public void  OnHoverOver()
    {
        IsHover = true;
    }


    /// <summary>
    /// 鼠标滑出该物体，隐藏物品信息
    /// </summary>
    public void OnHoverOut()
    {
        IsHover = false;
    }






}

