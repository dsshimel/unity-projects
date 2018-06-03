public interface IStrategyUntyped
{
    // Set the intensity of the strategy.
    void SetIntensity(float intensity);

    // Apply the strategy at time t.
    void ApplyStrategy(float timeNow, float timeBefore);
}   