using UnityEngine;

public class CrossfadeValues
{
    public static float PercentIn(float percentOut)
    {
        return 1 - percentOut;
    }

    public static Vector3 FadeVector3(Vector3 valueOut, Vector3 valueIn, float percentOut)
    {
        if (percentOut < 0 || percentOut > 1)
        {
            throw new System.ArgumentException("percent must be between 0 and 1");
        }
        var percentIn = PercentIn(percentOut);
        var x = percentOut * valueOut.x + percentIn * valueIn.x;
        var y = percentOut * valueOut.y + percentIn * valueIn.y;
        var z = percentOut * valueOut.z + percentIn * valueIn.z;
        return new Vector3(x, y, z);
    }
}