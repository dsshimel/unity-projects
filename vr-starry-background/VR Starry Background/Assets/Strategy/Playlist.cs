using System.Collections.Generic;

public class Playlist : IPlaylist
{
    private IList<IBundle> bundles;

    public Playlist()
    {
        bundles = new List<IBundle>();
    }

    public void AddBundle(IBundle bundle)
    {
        bundles.Add(bundle);
    }
}