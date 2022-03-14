using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadUI : MonoBehaviour
{
    public static HeadUI instance;  //屏幕坐上角的UI显示，单例模式

    private UILabel Name;
    private UISlider HP;
    private UILabel  HPinfo;
    private UISlider MP;
    private UILabel MPinfo;
    private PlayerStatus ps;
    private void Awake()
    {
        instance = this;
        Name= this.transform.Find("Name").GetComponent<UILabel>();
        HP = this.transform.Find("HP").GetComponent<UISlider>();
        HPinfo = this.transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
        MP = this.transform.Find("MP").GetComponent<UISlider>();
        MPinfo = this.transform.Find("MP/Thumb/Label").GetComponent<UILabel>();
    }

    private void Start()
    {
        ps = GameObject.FindObjectOfType<PlayerStatus>();
        UpdateShow();
    }
    /// <summary>
    /// 更新屏幕左上角的信息
    /// </summary>
    public void UpdateShow()
    {
        Name.text ="  "+ ps.playername;
        HP.value = ps.currentHP / (float)ps.hp;
        HPinfo.text = ps.currentHP.ToString()+"/" + ps.hp.ToString();
        MP.value = ps.currentMP / (float)ps.mp;
        MPinfo.text = ps.currentMP.ToString() + "/" + ps.mp.ToString();
    }
}
