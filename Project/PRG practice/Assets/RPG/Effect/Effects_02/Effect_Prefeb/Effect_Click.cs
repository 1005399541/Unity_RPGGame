using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Click : MonoBehaviour
{
  // 控制点击特效的消失
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject,0.2f);
    }
}
