using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //NPC的鼠标变化的通用类


    /// <summary>
    /// 划到npc的图标
    /// </summary>
    void OnMouseEnter()
    {
        CursorManager.instance.ChangeCursor_npc_talk();
    }

    /// <summary>
    /// 正常图标
    /// </summary>
    void OnMouseExit()
    {
        CursorManager.instance.ChangeCursor_normal();
    }
}
