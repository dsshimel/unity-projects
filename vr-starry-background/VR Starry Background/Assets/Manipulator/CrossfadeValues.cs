﻿using System;
using UnityEngine;

public class CrossfadeValues
{
    public static float PercentIn(float percentOut)
    {
        if (percentOut < 0 || 1 < percentOut)
        {
            throw new ArgumentException("percent must be between 0 and 1");
        }
        return 1 - percentOut;
    }

    public static Vector3 FadeVector3(Vector3 valueOut, Vector3 valueIn, float percentOut)
    {
        var percentIn = PercentIn(percentOut);
        var x = percentOut * valueOut.x + percentIn * valueIn.x;
        var y = percentOut * valueOut.y + percentIn * valueIn.y;
        var z = percentOut * valueOut.z + percentIn * valueIn.z;
        return new Vector3(x, y, z);
    }

    public static Color FadeColor(Color valueOut, Color valueIn, float percentOut)
    {
        var percentIn = PercentIn(percentOut);
        var r = percentOut * valueOut.r + percentIn * valueIn.r;
        var g = percentOut * valueOut.g + percentIn * valueIn.g;
        var b = percentOut * valueOut.b + percentIn * valueIn.b;
        var a = percentOut * valueOut.a + percentIn * valueIn.a;
        return new Color(r, g, b, a);
    }

    public static Gradient FadeGradient(Gradient valueOut, Gradient valueIn, float percentOut)
    {
        var colorKeysOut = valueOut.colorKeys;
        var colorKeysIn = valueIn.colorKeys;

        Gradient result = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colorKeysOut.Length];
        for (int indexOut = 0; indexOut < colorKeysOut.Length; indexOut++)
        {
            int indexIn = (indexOut * colorKeysIn.Length) / colorKeysOut.Length;
            var color = FadeColor(colorKeysOut[indexOut].color, colorKeysIn[indexIn].color, percentOut);
            var time = FadeFloat(colorKeysOut[indexOut].time, colorKeysIn[indexIn].time, percentOut);
            var colorKey = new GradientColorKey(color, time);
            colorKeys[indexOut] = colorKey;
        }

        // Assume alpha keys are the same
        result.SetKeys(colorKeys, valueOut.alphaKeys);

        return result;
    }

    public static float FadeFloat(float valueOut, float valueIn, float percentOut)
    {
        var percentIn = PercentIn(percentOut);
        return valueOut * percentOut + valueIn * percentIn;
    }
}