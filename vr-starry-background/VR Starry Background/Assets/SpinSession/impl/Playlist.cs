using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;
    private IList<Interval> intervals;
    private int currentEntryIndex;
    private float timeInEntry;
    private bool activeFinished;

    public Playlist()
    {
        bundles = new List<IBundle>();
        intervals = new List<Interval>();
        currentEntryIndex = 0;
        timeInEntry = 0;
        activeFinished = false;
    }

    // Resets the current entry to the beginning. Not the same as Resume()
    public void Play()
    {
        timeInEntry = 0;
        activeFinished = false;
        SetIntensities(1.0f);
        IncrementTime(0);
    }

    public float IncrementTime(float delta)
    {
        timeInEntry += delta;
        if (IsActive(timeInEntry))
        {
            ApplyStrategies(timeInEntry, timeInEntry - delta);
        } else if (IsResting(timeInEntry))
        {
            if (!activeFinished)
            {
                SetIntensities(0.5f);
                activeFinished = true;   
            }
            ApplyStrategies(timeInEntry, timeInEntry - delta);
        } else
        {
            // We finished this interval, go to the next.
            Next();
        }
        
        return timeInEntry;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        var fadeOutPercent = GetCurrentInterval().GetFadeOutPercent(timeNow);
        GetCurrentBundle().ApplyStrategies(timeNow, timeBefore);
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
    
    public bool IsActive(float time)
    {
        return GetCurrentInterval().IsActive(time);
    }

    public bool IsResting(float time)
    {
        return GetCurrentInterval().IsResting(time);
    }

    public void Next()
    {
        currentEntryIndex = GetNextIndex();
        Play();
    }

    private int GetNextIndex()
    {
        var next = currentEntryIndex + 1;
        if (next >= Length())
        {
            return 0;
        }
        return next;
    }

    public void Previous()
    {
        currentEntryIndex -= 1;
        if (currentEntryIndex <= 0)
        {
            currentEntryIndex = Length() - 1;
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

    public int Length()
    {
        return intervals.Count;
    }

    private void SetIntensities(float intensity)
    {
        GetCurrentBundle().SetIntensities(intensity);
    }
    
    private IBundle GetCurrentBundle()
    {
        return bundles[currentEntryIndex];
    }

    private IBundle GetNextBundle()
    {
        return bundles[GetNextIndex()];
    }

    private Interval GetCurrentInterval()
    {
        return intervals[currentEntryIndex];
    }
}