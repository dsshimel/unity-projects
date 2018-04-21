
/**
 * A strategy collection consisting of one instance of each type of strategy
 * available. 
 */
public interface IBundle
{
    void ApplyStrategies(float time);

    void SetIntensities(float intensity);
}