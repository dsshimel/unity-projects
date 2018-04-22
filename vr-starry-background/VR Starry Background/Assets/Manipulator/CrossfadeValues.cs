using UnityEngine;
using UnityEditor;

public class CrossfadeValues
{ 
    public class Vector3XFade
    {
        private Vector3 valueOne;
        private Vector3 valueTwo;
        private float percentOne;

        public Vector3XFade(Vector3 valueOne, Vector3 valueTwo, float percentOne)
        {
            if (percentOne < 0 || percentOne > 1)
            {
                throw new System.ArgumentException("percent must be between 0 and 1");
            }
            this.valueOne = valueOne;
            this.valueTwo = valueTwo;
            this.percentOne = percentOne;
        }

        private float percentTwo()
        {
            return 1 - percentOne;
        }

        public Vector3 getXFadeValue()
        {
            var x = percentOne * valueOne.x + percentTwo() * valueTwo.x;
            var y = percentOne * valueOne.y + percentTwo() * valueTwo.y;
            var z = percentOne * valueOne.z + percentTwo() * valueTwo.z;
            return new Vector3(x, y, z);
        }
    }