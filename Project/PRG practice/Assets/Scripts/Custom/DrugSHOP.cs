using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugSHOP : MonoBehaviour
{
    //药品商店物品栏
    public static DrugSHOP instance;

    private TweenPosition tween;
    private bool IsShow;

    private int Id;//购买的数量
    private GameObject NumberDialog;
    private UIInput Number;
    private void Awake()
    {
        instance = this;
        Id = 0;
        IsShow = true;
        tween = this.gameObject.GetComponent<TweenPosition>();
        NumberDialog = GameObject.Find("NumberDialog");
        Number = GameObject.Find("NumberBackGr").GetComponent<UIInput>();

        NumberDialog.SetActive(false);
       
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
    /// 药品商店的显示与隐藏
    /// </summary>
    public void TransformDrugShop()
    {
        if (IsShow==true)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Buy(int id)
    {
        Id=id;
        NumberDialog.SetActive(true);
        Number.value = "0";
    }
    /// <summary>
    /// 购买1001物品的第一步选择
    /// </summary>
    public void BuyButtonClick1001()
    {
        Buy(1001);

    }
    /// <summary>
    /// 购买1002物品的第一步选择
    /// </summary>
    public void BuyButtonClick1002()
    {
        Buy(1002);
    }
    /// <summary>
    /// 购买1003物品的第一步选择
    /// </summary>
    public void BuyButtonClick1003()
    {
        Buy(1003);
    }
    /// <summary>
    /// Ok按钮
    /// </summary>
    public void OkButtonCilck()
    {
        int number=int.Parse(Number.value);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoByid(Id);
        int price = info.buy *number ;
        if (price <=0) { return; }
        bool sucess = Inventory.instance.UpdateAndGetCoin(price);
        if (sucess == true)
        {
            
            Inventory.instance.PickUpCollect_ByGetid(Id, number);
            Debug.Log("购买成功");
        }

        NumberDialog.SetActive(false);
    }
    /// <summary>
    /// 检测药品物品栏是否在摄像机内
    /// </summary>
    /// <returns></returns>
    public bool  IsExist()
    {
        Vector3 position = new Vector3(1508,78,0);
        if (this.gameObject.transform.localPosition==position)
        {
            return true;
            
        }
        else return false;
        
    }
   
}
