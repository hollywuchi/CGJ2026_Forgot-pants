using System;
using UnityEngine;

public class Settings
{
    // const 常量，别的脚本无法更改该值
    public const float itemFadeDuration = 0.35f;
    public const float targetColor = 0.45f;

    public const float FadeDuration = 0.5f;

    public const int reapCount = 2;

    public const float gridCellSize = 1f;
    public const float gridCellDiagonalSize = 1.41f;

    public const float pixelSize = 0.05f;   // 如果像素大小为20*20占一个格子，所以一个格子的一个像素点大小为0.05
    public const float animationBreakTime = 5f;

    public const float maxGridSize = 9999;


    public static Vector3 playerStartPos = new Vector3(1.5f, -14.55f, 0);

    public const float SkipTime = 1;
}
