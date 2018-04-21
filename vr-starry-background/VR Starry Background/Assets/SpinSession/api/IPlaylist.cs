
/**
 * A sequence of strategy collections that is played back to the user during
 * their experience.
 */
public interface IPlaylist
{
    void AddEntry(IBundle bundle, Interval interval);

    void Reset();
}