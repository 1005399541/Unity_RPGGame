using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    //背包的相关功能
    public static Inventory instance;//单例模式
    private TweenPosition tween;//背包动画
    private int CoinCount;//金币数量
    private bool IsShow;//背包是否在显示


    public bool IsUI_Ventory;
    public List<Inventory_Item_Grid> item_Grids = new List<Inventory_Item_Grid>();//存储背包单元格
    public UILabel CoinCountLable;
    public GameObject Item;

    PlayerStatus playerStatus;



    public void Start()
    {
        IsShow = true;
        instance = this;
        tween = this.GetComponent<TweenPosition>();
        playerStatus = GameObject.Find("Player_Magician(Clone)").GetComponent<PlayerStatus>();
        UpdateCoin();

       
    }
    private void Update()
    {

        IsUI();

        if (Input.GetKeyDown(KeyCode.X))
        {
           
            PickUpCollect_ByGetid(Random.Range(2001, 2019));
        }
        
        
    }


    /// <summary>
    /// 更新金币的显示
    /// </summary>
    public void UpdateCoin()
    {
        CoinCount = playerStatus.GetCoin();
        CoinCountLable.text = CoinCount.ToString();
    }

    public bool UpdateAndGetCoin(int coin)
    {  
        bool IsExist=playerStatus.ChangeCoin(coin);
        UpdateCoin();
        return IsExist;
    }
    /// <summary>
    /// 显示背包
    /// </summary>
    private void Show()
    {
        IsShow =false;
        tween.PlayForward();

    }


    /// <summary>
    /// 隐藏背包
    /// </summary>
    private void Hide()
    {
        IsShow =true;
        tween.PlayReverse();
    }




    /// <summary>
    /// 判断是否在UI_Ventory上
    /// </summary>
    public void IsUI()
    {
        if (UICamera.isOverUI)
        {
            //Debug.Log(UICamera.hoveredObject.name);
            if (UICamera.hoveredObject.name!=Tag.grond)
            {
                   IsUI_Ventory=true;
                return;
            }
            IsUI_Ventory = false;return;
        }
         IsUI_Ventory=false;return;
    }


    /// <summary>
    /// 背包状态的改变
    /// </summary>
    public void TransformState()
    {
        if (IsShow)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    /// <summary>
    /// 拾取物品
    /// </summary>
   public void PickUpCollect_ByGetid(int id,int number=1)
    {
        //1判断物品是否存在
        Inventory_Item_Grid grid = null;
        foreach(Inventory_Item_Grid item_Grid in item_Grids)
        {
            if (id == item_Grid.id_Item)
            { grid = item_Grid;break; }
        }
        //2若存在则number+1或多个
        if (grid != null)
        {
            grid.PlusNumber(number);
        }
        else//3若不存在
        {
            foreach (Inventory_Item_Grid item_Grid in item_Grids)
            {
                if (item_Grid.id_Item == 0) { grid = item_Grid; break; }
            }


            if (grid!=null)
            {
                GameObject ItemGo = NGUITools.AddChild(grid.gameObject, Item);
                ItemGo.transform.localPosition = Vector3.zero;
                grid.SetId(id,number);
            }
        }
    }



    /// <summary>
    /// 使用药品到快捷栏
    /// </summary>
     public bool OnUseDrug(int id, out bool outofnumber, int count=1)
    {
        //1判断物品是否存在
        Inventory_Item_Grid grid = null;
        foreach (Inventory_Item_Grid item_Grid in item_Grids)
        {
            if (id == item_Grid.id_Item)
            {
                grid = item_Grid;

                Debug.Log(item_Grid.id_Item);
            }
            if (grid == null)
            {
                outofnumber = false;
                return false; 
            }
            else
            {
                GameObject Item=grid.GetComponentInChildren<Inventory_Item>().gameObject;
                outofnumber=grid.GetComponent<Inventory_Item_Grid>().EquipProp(Item);
                grid = null;return true;
            }
        }
        outofnumber = false;
        return true;
    }

}
