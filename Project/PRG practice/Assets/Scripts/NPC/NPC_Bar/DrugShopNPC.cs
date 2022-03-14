using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShopNPC :NPC
{
    /// <summary>
    /// 鼠标停留在在NPC身上时
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrugSHOP.instance.TransformDrugShop();
        }
    }
}
