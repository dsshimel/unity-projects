using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;
    private int currentBundleIndex;

    public Playlist()
    {
        bundles = new List<IBundle>();
        currentBundleIndex = 0;
    }

    public void AddBundle(IBundle bundle)
    {
        bundles.Add(bundle);
    }

    public void Reset()
    {
        currentBundleIndex = 0;
    }
}