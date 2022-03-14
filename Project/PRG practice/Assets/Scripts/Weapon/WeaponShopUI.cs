using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopUI : MonoBehaviour
{
    //武器商人物品栏
    public static WeaponShopUI instance;


    public int[] weaponIdArray;
    public GameObject weaponPrefab;
    public UIGrid grid;

    private GameObject WeaponDialog;
    private UILabel WeaponNumberInput;
    private int Bugid;

    private TweenPosition tween;
    private bool IsShow;
    private Vector3 StartPosition;
    private void Awake()
    {
        StartPosition = new Vector3(1508, 0, 0);
        this.transform.position = StartPosition;
        instance = this;
        tween = this.GetComponent<TweenPosition>();
        IsShow = true;
        WeaponDialog =transform.Find("WeaponNumberDialog").gameObject;
        WeaponNumberInput =transform.Find("WeaponNumberDialog/NumberBackGr/NameLabel").GetComponent<UILabel>();
        WeaponDialog.SetActive(false);
        WeaponNumberInput.text = "0";

    }
    private void Start()
    {
        InitWeaponShop();
    }

    private void Hide()
    {
        tween.PlayReverse();
        IsShow = true;
    }
    private void Show()
    {
        tween.PlayForward();
        IsShow = false;
    }

    /// <summary>
    /// 控制状态栏的显示与隐藏
    /// </summary>
    public void TransformStatus()
    
    {
        if (IsShow == true)
        {
            Show();
        }
        else Hide();

    }


    /// <summary>
    /// 关闭商人物品栏
    /// </summary>
    public void Colse()
    {
        Hide();
    }


    /// <summary>
    /// 初始化武器商人武器物品
    /// </summary>
    private void InitWeaponShop()
    {
        foreach(int id in weaponIdArray)
        {
            GameObject GO = NGUITools.AddChild(grid.gameObject, weaponPrefab);
            GO.transform.parent = grid.transform;
            GO.GetComponent<WeaponItem>().SetId(id);
        }
    }

    

    /// <summary>
    /// 武器物品的购买
    /// </summary>
    /// <param name="id"></param>
    public void BuyButton()
    {
        
        int count = int.Parse(WeaponNumberInput.text);
        if (count > 0)
        {
            Inventory.instance.PickUpCollect_ByGetid(Bugid, count);
            ObjectInfo info = ObjectsInfo.instance.GetObjectInfoByid(Bugid);
            int allprice = info.buy * count;
            
            Inventory.instance.UpdateAndGetCoin(allprice);
        }
        Bugid = 0;
        WeaponNumberInput.text = "0";
        WeaponDialog.SetActive(false);
        

    }


    /// <summary>
    /// ok按钮的点击
    /// </summary>
    public void OkButton(int id)
    {

         Bugid = id;
        WeaponDialog.SetActive(true);
       

    }
}
