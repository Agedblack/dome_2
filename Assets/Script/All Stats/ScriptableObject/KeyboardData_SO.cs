using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Data",menuName = "Keyboard Stats/Data")]
public class KeyboardData_SO : ScriptableObject
{
    [Header("按键设置")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyShift;
    public string keySpace;
    public string keyMouse0;
    public string keyMouse1;
}
