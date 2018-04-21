using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;
    private IList<Interval> intervals;
    private int currentEntryIndex;
    private float timeInEntry;

    public Playlist()
    {
        bundles = new List<IBundle>();
        intervals = new List<Interval>();
        currentEntryIndex = 0;
        timeInEntry = 0;
    }

    public void AddEntry(IBundle bundle, Interval interval)
    {
        bundles.Add(bundle);
        intervals.Add(interval);
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
        currentEntryIndex += 1;
        if (currentEntryIndex >= Length())
        {
            currentEntryIndex = 0;
        }
        Play();
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

    public void Play()
    {
        timeInEntry = 0;
    }

    public void Reset()
    {
        currentEntryIndex = 0;
        Play();
    }

    public int Length()
    {
        return intervals.Count;
    }

    public float IncrementTime(float delta)
    {
        timeInEntry += delta;
        return timeInEntry;
    }

    private IBundle GetCurrentBundle()
    {
        return bundles[currentEntryIndex];
    }

    private Interval GetCurrentInterval()
    {
        return intervals[currentEntryIndex];
    }
}