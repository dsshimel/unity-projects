using System.Collections.Generic;

public class PlaylistFactory
{
    private readonly BundleFactory bundleFactory;
    public readonly float maxIntensity;
    public readonly float minIntensity;

    public PlaylistFactory(
        BundleFactory bundleFactory, 
        float maxIntensity, 
        float minIntensity)
    {
        this.bundleFactory = bundleFactory;
        this.maxIntensity = maxIntensity;
        this.minIntensity = minIntensity;
    }

    public IPlaylist create(int length)
    {
        if (length <= 0)
        {
            throw new System.ArgumentException("length must be greater than 0");
        }

        var playlist = new Playlist(new List<IBundle>(), new List<Interval>());
        var interval = new Interval(5.0f, 1.0f, 2.5f, 5.0f, maxIntensity, minIntensity);
   
        for (var i = 0; i < length; i++)
        {
            playlist.AddEntry(bundleFactory.create(), interval);
        }

        return playlist;
    }
}