using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatic
{
    /// <summary>
    /// 斜方向向量控制为1
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
