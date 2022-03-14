using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //玩家状态的控制

    private PlayerMove playerMove;
    private Animation playerAnim;
    private PlayerAttack playerAttack;
    void Start()
    {
        playerMove = this.GetComponent<PlayerMove>();
        playerAnim = this.GetComponent<Animation>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }



    void LateUpdate()
    {

        if (playerAttack.controllerAttack == ControllerAttack.controllerWalk)//控制行走的状态
        {
            if (playerMove.State == PlayerMove.PlayerState.Idle)
            {
                playerAnimation("Idle");
                //Debug.Log("站立");
            }
            if (playerMove.State == PlayerMove.PlayerState.Run)
            {
                playerAnimation("Run");
            }
        }
        //if (playerAttack.controllerAttack == ControllerAttack.normalAttack)//进入一般攻击状态
        //{
        //    if (playerAttack.inAttackState == InAttackState.walk)
        //    {
        //        playerAnimation("Run");
        //    }
        //    if (playerAttack.inAttackState == InAttackState.idle)
        //    {
        //        playerAnimation("Idle");
        //    }

           
        //}
    }


    /// <summary>
    /// 播放动画
    /// </summary>
    void playerAnimation(string anim_name)
    {
        playerAnim.CrossFade(anim_name);
    }

}
