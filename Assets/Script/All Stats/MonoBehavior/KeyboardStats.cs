using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardStats : MonoBehaviour
{
    public KeyboardData_SO keyboardData;
    #region 读取数据
    public string KeyUp
    {
        get { if (keyboardData != null) return keyboardData.keyUp; else return "w"; }
        set { keyboardData.keyUp = value; }
    }
    public string KeyDown
    {
        get { if (keyboardData != null) return keyboardData.keyDown; else return "s"; }
        set { keyboardData.keyDown = value; }
    }
    public string KeyLeft
    {
        get { if (keyboardData != null) return keyboardData.keyLeft; else return "a"; }
        set { keyboardData.keyLeft = value; }
    }
    public string KeyRight
    {
        get { if (keyboardData != null) return keyboardData.keyRight; else return "d"; }
        set { keyboardData.keyRight = value; }
    }
    public string KeyShift
    {
        get { if (keyboardData != null) return keyboardData.keyShift; else return "left shift"; }
        set { keyboardData.keyShift = value; }
    }
    public string KeySpace
    {
        get { if (keyboardData != null) return keyboardData.keySpace; else return "space"; }
        set { keyboardData.keySpace = value; }
    }
    public string KeyMouse0
    {
        get { if (keyboardData != null) return keyboardData.keyMouse0; else return "mouse 0"; }
        set { keyboardData.keyMouse0 = value; }
    }
    public string KeyMouse1
    {
        get { if (keyboardData != null) return keyboardData.keyMouse1; else return "mouse 1"; }
        set { keyboardData.keyMouse1 = value; }
    }
    #endregion
}
