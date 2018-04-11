using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpinSession
{
    public class OldSession : MonoBehaviour
    {
        public int countdownTimeSeconds = 1;

        public event EventHandler CountdownStart;
        public event EventHandler CountdownEnd;
        public event EventHandler ActivePeriodStart;
        public event EventHandler ActivePeriodEnd;
        public event EventHandler RestPeriodStart;
        public event EventHandler RestPeriodEnd;

        private bool going = false;
        private bool countdownEndMilestone = false;
        private bool activePeriodMilestone = false;
        private int currentIntervalIndex = 0;
        private Interval currentInterval = null;
        private float timeSinceLastEvent = 0;

        private IList<Interval> intervals;

        void Start()
        {
            intervals = new List<Interval>();
            for (int i = 0; i < 5; i++)
            {
                intervals.Add(new Interval(10, 5));
            }
        }

        void Update()
        {
            if (going)
            {
                timeSinceLastEvent += Time.deltaTime;

                if (!countdownEndMilestone)
                {
                    if (timeSinceLastEvent >= countdownTimeSeconds)
                    {
                        OnCountdownEnd(new EventArgs());
                        countdownEndMilestone = true;
                        timeSinceLastEvent = 0;
                    }
                }

                if (currentInterval == null)
                {
                    if (currentIntervalIndex >= intervals.Count)
                    {
                        // We're done, don't do anything further
                        return;
                    }
                    OnActivePeriodStart(new EventArgs());
                    currentInterval = intervals[currentIntervalIndex];
                    timeSinceLastEvent = 0;
                } else
                {
                    if (!activePeriodMilestone)
                    {
                        if (timeSinceLastEvent > currentInterval.activeIntervalSeconds)
                        {
                            OnActivePeriodEnd(new EventArgs());
                            OnRestPeriodStart(new EventArgs());
                            activePeriodMilestone = true;
                            timeSinceLastEvent = 0;
                        }
                    } else 
                    {
                        if (timeSinceLastEvent > currentInterval.restIntervalSeconds)
                        {
                            OnRestPeriodEnd(new EventArgs());
                            activePeriodMilestone = false;
                            currentInterval = null;
                            currentIntervalIndex += 1;
                            timeSinceLastEvent = 0;
                        }
                    }
                }
            }
        }

        public void Begin()
        {
            going = true;
            OnCountdownStart(new EventArgs());
        }

        protected virtual void OnCountdownStart(EventArgs e)
        {
            EventHandler handler = CountdownStart;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCountdownEnd(EventArgs e)
        {
            EventHandler handler = CountdownEnd;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnActivePeriodStart(EventArgs e)
        {
            EventHandler handler = ActivePeriodStart;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnActivePeriodEnd(EventArgs e)
        {
            EventHandler handler = ActivePeriodEnd;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRestPeriodStart(EventArgs e)
        {
            EventHandler handler = RestPeriodStart;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRestPeriodEnd(EventArgs e)
        {
            EventHandler handler = RestPeriodEnd;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}