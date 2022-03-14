using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;


    public Texture2D Cursor_normal;//正常图标
    public Texture2D Cursor_npc_talk;//划到npc的图标
    public Texture2D Cursor_attack;//攻击的图标
    public Texture2D Cursor_lockTarget;//使用技能的图标
    public Texture2D Cursor_pick;//拾取物品的图标

    private Vector2 hotspot = Vector2.zero;//鼠标右上角为触发的
    private CursorMode cursorMode = CursorMode.Auto;


    private PlayerAttack playerAttack;
    private void Start()
    {
        instance = this;
        playerAttack = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerAttack>();
    }


    /// <summary>
    /// 正常图标
    /// </summary>
    public void ChangeCursor_normal()
    {
        Cursor.SetCursor(Cursor_normal, hotspot, cursorMode);
    }
    /// <summary>
    /// 划到npc的图标
    /// </summary>
    public void ChangeCursor_npc_talk()
    {
        if (!playerAttack.IsTarget)
        {
            Cursor.SetCursor(Cursor_npc_talk, hotspot, cursorMode);
        }
       
    }
    /// <summary>
    /// 攻击的图标
    /// </summary>
    public void ChangeCursor_attack()
    {
        if (!playerAttack.IsTarget)
        {
            Cursor.SetCursor(Cursor_attack, hotspot, cursorMode);
        }
       
    }
    /// <summary>
    /// 使用技能的图标
    /// </summary>
    public void ChangeCursor_lockTarget()
    {
        Cursor.SetCursor(Cursor_lockTarget, hotspot, cursorMode);
    }
    /// <summary>
    /// 拾取物品的图标
    /// </summary>
    public void ChangeCursor_pick()
    {
        Cursor.SetCursor(Cursor_pick, hotspot, cursorMode);
    }
}
