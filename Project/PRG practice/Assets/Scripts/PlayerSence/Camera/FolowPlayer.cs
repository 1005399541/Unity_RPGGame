using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowPlayer : MonoBehaviour
{
    //摄像机的跟随
    private Transform PlayerTrans;//玩家的位置
    private Vector3 Offest;//偏移量
    private bool IsRolate;//视野是否旋转



    public float Distance;//玩家到摄像机的距离
    public float ScrollSpeed;//滚轮的速度
    public float RolateSpeed;//视野旋转的速度

    void Start()
    {
        
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(PlayerTrans);

        Offest = transform.position - PlayerTrans.position;
        Distance = Offest.magnitude;
        ScrollSpeed = 10f;
        IsRolate = false;
        RolateSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Offest + PlayerTrans.position;   //摄像机跟随玩家移动
        RolateView();
        ScrollView();
    }



    /// <summary>
    /// 通过鼠标的滚动来拉近拉远视野
    /// </summary>
    void ScrollView()
    {
     
        float Mouse_ScrollWheel=Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(Mouse_ScrollWheel);
        if (Mouse_ScrollWheel != 0)//负值拉近，正值拉远
        {
            Distance += ScrollSpeed * Mouse_ScrollWheel;
            Distance=Mathf.Clamp(Distance, 1.24f, 20);
            Offest = Offest.normalized * Distance;
            
        }
    }


    /// <summary>
    /// 通过按住鼠标左键按y轴旋转
    /// </summary>
    void RolateView()
    {
        //Debug.Log(IsRolate);
        if(Input.GetMouseButtonDown(1))
        {
            IsRolate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            IsRolate = false;
        }
        if (IsRolate)
        {
            transform.RotateAround(PlayerTrans.position,Vector3.up, RolateSpeed * Input.GetAxis("Mouse X"));

            Vector3 OriginalPosition = transform.position;
            Quaternion OriginalRolation = transform.rotation;
            transform.RotateAround(PlayerTrans.position, transform.right, -RolateSpeed * Input.GetAxis("Mouse Y"));
            
            float X = transform.eulerAngles.x;
            if (X>50||X<5)
            {
                transform.position = OriginalPosition;
                transform.rotation = OriginalRolation;
            }
            Offest = transform.position - PlayerTrans.position;
        }
    }




}
