using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSeting : MonoBehaviour
{
    //  经验条的管理
    public static ExpSeting instance;

    private UISlider expslider;//经验进度条
    private UILabel LevelLable;//等级的显示


    private void Awake()
    {
        instance = this;
        expslider = this.GetComponent<UISlider>();
        LevelLable = this.transform.root.transform.Find("Head/Name/Level").GetComponent<UILabel>();
    }



    /// <summary>
    /// 经验条的显示与更新
    /// </summary>
    public void GetandUpdateExp(float value,int grade)
    {
        expslider.value = value;
        LevelLable.text= "LV"+grade.ToString();
    }
}
