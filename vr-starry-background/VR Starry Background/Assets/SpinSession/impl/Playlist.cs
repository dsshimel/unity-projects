using System.Collections.Generic;

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
                ApplyStrategies(timeInEntry, timeInEntry - delta);
                break;
            case State.ACTIVE_FINISHED:
                SetIntensities(0.5f);
                ApplyStrategies(timeInEntry, timeInEntry - delta);
                break;
            case State.RESTING:
                ApplyStrategies(timeInEntry, timeInEntry - delta);
                break;
            case State.RESTING_FINISHED:
                ApplyStrategies(timeInEntry, timeInEntry - delta);
                break;
            case State.FADING:
                ApplyStrategies(timeInEntry, timeInEntry - delta);
                break;
            case State.FADING_FINISHED:
                Next();
                break;
            default:
                throw new System.InvalidOperationException("Unrecognized state " + state);
        }
        
        return timeInEntry;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        if (state == State.FADING)
        {
            CurrentBundle.ApplyStrategiesFade(NextBundle, CurrentInterval.GetFadePercent(timeNow), timeNow, timeBefore);
        } else
        {
            CurrentBundle.ApplyStrategies(timeNow, timeBefore);
        }
        // TODO: If we apply the crossfaded version of a strategy in one frame, we don't
        // want to apply the non-crossfaded version as well.
        // TODO: This code doesn't work. Investigate.
        //if (fadeOutPercent < 1)
        //{
        //    GetCurrentBundle().GetMovementStrategy().ApplyStrategyWithCrossfade(
        //        timeNow, timeBefore, GetNextBundle().GetMovementStrategy(), fadeOutPercent);
        //} else
        //{
        //    GetCurrentBundle().ApplyStrategies(timeNow, timeBefore);
        //}
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
        else if (IsResting())
        {
            if (currentState == State.ACTIVE)
            {
                state = State.ACTIVE_FINISHED;
            }
            else
            {
                state = State.RESTING;
            }
        }
        else if (IsFading())
        {
            if (currentState == State.RESTING)
            {
                state = State.RESTING_FINISHED;
            }
            else
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
        RESTING,
        RESTING_FINISHED,
        FADING,
        FADING_FINISHED
    }
}