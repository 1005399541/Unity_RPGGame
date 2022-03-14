using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minmap : MonoBehaviour
{

    Camera Minmapcamera;
    private void Start()
    {
        Minmapcamera = GameObject.FindGameObjectWithTag(Tag.Minmap).gameObject.GetComponent<Camera>();
    }

    /// <summary>
    /// 加号放大小地图
    /// </summary>
    public void MinmapInClick()
    {
        Minmapcamera.orthographicSize++;
    }


    /// <summary>
    /// 减号缩小小地图
    /// </summary>
    public void MinmapOutClick()
    {
        Minmapcamera.orthographicSize--;
    }
}
