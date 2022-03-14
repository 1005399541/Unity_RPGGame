using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDirection : MonoBehaviour
{

    //玩家的方向通过鼠标点击确定



    public GameObject Effect_PreFab_Cclick_normal;//正常点击的鼠标效果



    private bool IsMoving;//是否要朝向目标
    public Vector3 TargetPosition;//鼠标点击的点
    public Vector3 MousePosition;
    private PlayerMove playerMove;

    private PlayerStatus playerStatus;

    private void Awake()
    {
         playerStatus= this.GetComponent<PlayerStatus>();
    }
    void Start()
    {
       
        TargetPosition = transform.position;
        playerMove = this.GetComponent<PlayerMove>();

    }


    void Update()
    {
        if (DrugSHOP.instance.IsExist() == false) { return; }
        if (playerStatus.currentHP<=0) {playerMove.death(); return; }
        Movedirection_byMoutton(); 
         LookAtTarget();
       

    }


    /// <summary>
    /// 通过鼠标左键控制
    /// </summary>
    void Movedirection_byMoutton()
    {
       GameObject Quest =GameObject.Find("Quest");
                //Debug.Log(Quest);
                //Debug.Log(Input.GetMouseButton(0));
        if (Input.GetMouseButtonDown(0) &&Quest==null)
        {
            if (Inventory.instance.IsUI_Ventory) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHitInfo;
            bool IsCollider = Physics.Raycast(ray,out rayHitInfo);
            MousePosition = rayHitInfo.point;
            Debug.Log(rayHitInfo.collider.tag);
            if (IsCollider&&rayHitInfo.collider.tag==Tag.grond)
            {
                ShowClickEffect(rayHitInfo.point);
                IsMoving = true;

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsMoving = false;
        }

    }


    /// <summary>
    /// 主角的朝向
    /// </summary>
    void LookAtTarget()
    {

        if (IsMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHitInfo;
            bool IsCollider = Physics.Raycast(ray, out rayHitInfo);
            if (IsCollider && rayHitInfo.collider.tag == Tag.grond)
            {
                //Debug.Log(rayHitInfo.point);
                //Debug.Log(transform.position);
                TargetPosition = rayHitInfo.point;
                TargetPosition = new Vector3(rayHitInfo.point.x, transform.position.y, rayHitInfo.point.z);
                transform.LookAt(TargetPosition);
            }

        }
     
        //else { if (playerMove.IsMoving) transform.LookAt(TargetPosition); }
    }


    /// <summary>
    /// 点击特效
    /// </summary>
    void ShowClickEffect(Vector3 HitPoint)
    {
        HitPoint = new Vector3(HitPoint.x,HitPoint.y+0.2f,HitPoint.z);
        Instantiate(Effect_PreFab_Cclick_normal, HitPoint, Quaternion.identity);
    }
    
   
}
