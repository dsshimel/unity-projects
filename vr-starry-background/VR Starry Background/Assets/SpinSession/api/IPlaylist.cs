
/**
 * A sequence of strategy collections that is played back to the user during
 * their experience.
 */
public interface IPlaylist
{
    void AddEntry(IBundle bundle, Interval interval);

    bool IsActive(float time);
    bool IsResting(float time);

    void Next();
    void Previous();
    void Play();

    float IncrementTime(float delta);
    int Length();

    void Reset();
}