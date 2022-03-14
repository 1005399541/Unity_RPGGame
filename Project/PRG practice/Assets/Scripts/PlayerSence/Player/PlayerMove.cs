using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //角色的移动
    public enum PlayerState
    {
        Idle,
        Run
    }
    public PlayerState State;//玩家的状态

    public int Speed;//角色的速度
    public bool IsMoving;//通过是否移动实时纠正朝向

    
    private CharacterController CharaC;//角色控制器
    private PlayerDirection PlayerDir;//玩家的朝向信息的类
    private PlayerAttack PlayerAttack;
    void Start()
    {
        IsMoving = false;
        State = PlayerState.Idle;
        CharaC = this.GetComponent<CharacterController>();
        PlayerDir = this.GetComponent<PlayerDirection>();
        PlayerAttack = this.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();
    }

    /// <summary>
    /// 角色的移动
    /// </summary>
    void Move()
    {
        if (PlayerAttack.controllerAttack == ControllerAttack.controllerWalk)//控制行走状态
        {

            
            float Distance = Vector3.Distance(PlayerDir.TargetPosition, transform.position);
            if (Distance > 0.3f)
            {

                CharaC.SimpleMove(transform.forward * Speed);

                State = PlayerState.Run;
                IsMoving = true;

            }
            else { State = PlayerState.Idle; IsMoving = false; }


        }


    }

    public void death()
    {
        PlayerAttack.Animdeath();
    }

    /// <summary>
    /// 控制移动
    /// </summary>
   public void  simpleMove(Vector3 position)
    {
        transform.LookAt(position);
        CharaC.SimpleMove(transform.forward * Speed);
    }
}
