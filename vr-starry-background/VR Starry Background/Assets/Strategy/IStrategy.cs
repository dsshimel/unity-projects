public interface IStrategy<T> : IStrategyUntyped
{
    // Get the value to be applied.
    T ComputeStrategyValue(int gameObjectId, float timeNow, float timeBefore);

    T CrossFadeStrategyValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);

    void ApplyStrategyWithCrossfade(float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);
}   