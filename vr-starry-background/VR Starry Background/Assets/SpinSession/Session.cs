using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace SpinSession
{
    public class Session : MonoBehaviour
    {
        public int countdownTimeSeconds = 1;

        public event EventHandler CountdownEnd;

        private bool going = false;
        private bool countdownEndMilestone = false;
        private float beginTime;

        void Start()
        {
            
        }

        void Update()
        {
            if (going)
            {
                if (!countdownEndMilestone)
                {
                    if (Time.time - beginTime >= countdownTimeSeconds)
                    {
                        OnCountdownEnd(new EventArgs());
                        countdownEndMilestone = true;
                    }
                }
            }
        }

        public void Begin()
        {
            beginTime = Time.time;
            going = true;
        }

        protected virtual void OnCountdownEnd(EventArgs e)
        {
            EventHandler handler = CountdownEnd;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        
    }
}