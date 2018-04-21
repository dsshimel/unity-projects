
/**
 * A complete spin experience.
 */
public interface ISession
{
    float IncrementTime(float delta);
    void Start();
    void Pause();
    bool IsCountingDown();
}