using UnityEngine;
using System.Collections;

public class MovieCamera : MonoBehaviour
{

    //控制游戏开始镜头

    public float speed = 10;

    private float endZ = -20;

	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (transform.position.z < endZ) {//还没有达到目标位置，需要移动
            transform.Translate( Vector3.forward*speed*Time.deltaTime);
        }
	}
}
