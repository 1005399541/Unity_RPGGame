using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    //屏幕右下角的功能类


    /// <summary>
    /// 状态按钮
    /// </summary>
    public void OnStatusButtonClick()
    {
        Status.instance.TransformStatus();
    }
    /// <summary>
    /// 背包按钮
    /// </summary>

    public void OnBagButtonClick()
    {
        Inventory.instance.TransformState();
    }
    /// <summary>
    /// 装备按钮
    /// </summary>
    public void OnEquipButtonClick()
    {
        EquipmentUI.instance.EquipmentUITransformStaus();
    }
    /// <summary>
    /// 技能按钮
    /// </summary>
    public void OnSkillButtonClick()
    {
        SkillUI.instance.SkillUITransformStatus();
    }
    /// <summary>
    /// 设置按钮
    /// </summary>
    public void OnSettingButtonClick()
    {

    }
}
