using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillIcon :UIDragDropItem
{
    //技能图标的管理


    private  int SkillId;
    protected override void OnDragDropStart()
    {     //拖拽的是克隆体
        base.OnDragDropStart();

        SkillId = transform.parent.GetComponent<SkillItem>().id;
        this.transform.parent = transform.root;
        this.GetComponent<UISprite>().depth = 40;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null && surface.tag == Tag.Shortcut)
        {
            //拖拽到快捷技能栏
            surface.GetComponent<ShortcutSkill>().SetSkillId(SkillId);
        }
    }
}
