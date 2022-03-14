using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTextParent : MonoBehaviour
{
    public static HUDTextParent instance;

    private void Awake()
    {
        instance = this;
    }
}
