using System.Collections.Generic;

public class PlaylistFactory
{
    private readonly BundleFactory bundleFactory;

    public PlaylistFactory(BundleFactory bundleFactory)
    {
        this.bundleFactory = bundleFactory;
    }

    public IPlaylist create(int length)
    {
        if (length <= 0)
        {
            throw new System.ArgumentException("length must be greater than 0");
        }

        var playlist = new Playlist(new List<IBundle>(), new List<Interval>());
        var interval = new Interval(5, 2.5f, 5.0f);
   
        for (var i = 0; i < length; i++)
        {
            playlist.AddEntry(bundleFactory.create(), interval);
        }

        return playlist;
    }
}