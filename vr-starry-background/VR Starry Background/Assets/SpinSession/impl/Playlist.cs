using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;
    private IList<Interval> intervals;
    private int currentEntryIndex;

    public Playlist()
    {
        bundles = new List<IBundle>();
        intervals = new List<Interval>();
        currentEntryIndex = 0;
    }

    public void AddEntry(IBundle bundle, Interval interval)
    {
        bundles.Add(bundle);
        intervals.Add(interval);
    }

    public void Reset()
    {
        currentEntryIndex = 0;
    }
}