﻿using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;
    private IList<Interval> intervals;
    private int currentEntryIndex;
    private float timeInEntry;
    private State state;

    public Playlist(IList<IBundle> bundles, IList<Interval> intervals)
    {
        this.bundles = bundles;
        this.intervals = intervals;
        currentEntryIndex = 0;
        timeInEntry = 0;
        state = State.UNRECOGNIZED;
    }

    // Resets the current entry to the beginning. Not the same as Resume()
    public void Play()
    {
        state = State.ACTIVE;
        timeInEntry = 0;
        SetIntensities(1.0f);
        IncrementTime(0);
    }

    public float IncrementTime(float delta)
    {
        timeInEntry += delta;
        UpdateState();

        switch (state)
        {
            case State.ACTIVE:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.ACTIVE_FINISHED:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.SLOWING_DOWN:
                SetIntensities(GetSlowdownIntensity(timeInEntry));
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.SLOWING_DOWN_FINISHED:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.RESTING:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.RESTING_FINISHED:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.FADING:
                ApplyStrategies(timeInEntry, delta);
                break;
            case State.FADING_FINISHED:
                Next();
                break;
            default:
                throw new System.InvalidOperationException("Unrecognized state " + state);
        }
        
        return timeInEntry;
    }

    public void ApplyStrategies(float timeNow, float timeDelta)
    {
        if (state == State.FADING)
        {
            CurrentBundle.ApplyStrategiesFade(NextBundle, CurrentInterval.GetFadePercent(timeNow), timeNow, timeDelta);
        } else
        {
            CurrentBundle.ApplyStrategies(timeNow, timeDelta);
        }
    }

    public void Next()
    {
        currentEntryIndex = NextIndex;
        Play();
    }

    public void Previous()
    {
        currentEntryIndex -= 1;
        if (currentEntryIndex <= 0)
        {
            currentEntryIndex = Length - 1;
        }
        Play();
    }

    public void Reset()
    {
        currentEntryIndex = 0;
        Play();
    }

    public void AddEntry(IBundle bundle, Interval interval)
    {
        bundles.Add(bundle);
        intervals.Add(interval);
    }

    public int Length
    {
        get
        {
            return intervals.Count;
        }
    }

    private float GetSlowdownIntensity(float timeNow)
    {
        return CurrentInterval.GetSlowdownIntensity(timeNow);
    }

    private void SetIntensities(float intensity)
    {
        CurrentBundle.SetIntensities(intensity);
    }

    private void UpdateState()
    {
        State currentState = state;
        if (IsActive())
        {
            state = State.ACTIVE;
        }
        else if (IsSlowingDown())
        {
            if (currentState == State.ACTIVE)
            {
                state = State.ACTIVE_FINISHED;
            } else
            {
                state = State.SLOWING_DOWN;
            }
        } else if (IsResting())
        {
            if (currentState == State.SLOWING_DOWN)
            {
                state = State.SLOWING_DOWN_FINISHED;
            } else
            {
                state = State.RESTING;
            }
        }
        else if (IsFading())
        {
            if (currentState == State.RESTING)
            {
                state = State.RESTING_FINISHED;
            } else
            {
                state = State.FADING;
            }
        }
        else
        {
            state = State.FADING_FINISHED;
        }
    }

    private IBundle CurrentBundle
    {
        get
        {
            return bundles[currentEntryIndex];
        }
    }

    private IBundle NextBundle
    {
        get
        {
            return bundles[NextIndex];
        }
    }

    private bool IsActive()
    {
        return CurrentInterval.IsActive(timeInEntry);
    }

    private bool IsSlowingDown()
    {
        return CurrentInterval.IsSlowingDown(timeInEntry);
    }

    private bool IsResting()
    {
        return CurrentInterval.IsResting(timeInEntry);
    }

    private bool IsFading()
    {
        return CurrentInterval.IsFading(timeInEntry);
    }

    private Interval CurrentInterval
    {
        get
        {
            return intervals[currentEntryIndex];
        }
    }

    private int NextIndex
    {
        get
        {
            var next = currentEntryIndex + 1;
            if (next >= Length)
            {
                return 0;
            }
            return next;
        }
    }

    enum State
    {
        UNRECOGNIZED = 0,
        ACTIVE,
        ACTIVE_FINISHED,
        SLOWING_DOWN,
        SLOWING_DOWN_FINISHED,
        RESTING,
        RESTING_FINISHED,
        FADING,
        FADING_FINISHED
    }
}