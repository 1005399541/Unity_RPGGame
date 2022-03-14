using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNPC :NPC
    //武器商人
{

    //检测鼠标是否在人物身上
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeaponShopUI.instance.TransformStatus();
        }
    }
}
