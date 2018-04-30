using UnityEngine;
using UnityEditor;

public class CrossfadeValues
{
    public class Vector3XFade : AbstractXFader<Vector3>
    {
        public Vector3XFade(Vector3 valueOne, Vector3 valueTwo, float percentOne) : base(valueOne, valueTwo, percentOne)
        {
        }

        public override Vector3 GetXFadeValue()
        {
            var x = percentOne * valueOne.x + PercentTwo() * valueTwo.x;
            var y = percentOne * valueOne.y + PercentTwo() * valueTwo.y;
            var z = percentOne * valueOne.z + PercentTwo() * valueTwo.z;
            return new Vector3(x, y, z);
        }
    }
}