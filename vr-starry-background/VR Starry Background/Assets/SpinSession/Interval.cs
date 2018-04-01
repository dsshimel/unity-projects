using UnityEngine;
using UnityEditor;

namespace SpinSession
{
    public class Interval
    {
        public float activeIntervalSeconds = 20.0f;
        public float restIntervalSeconds = 10.0f;

        public Interval(float activeIntervalSeconds, float restIntervalSeconds)
        {
            this.activeIntervalSeconds = activeIntervalSeconds;
            this.restIntervalSeconds = restIntervalSeconds;
        }
    }
}
