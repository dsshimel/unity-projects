public interface IStrategy<T> : IStrategyUntyped
{
    // Get the value to be applied.
    T ComputeValue(int gameObjectId, float timeNow, float timeBefore);

    T CrossFadeValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);

    void ApplyStrategyWithCrossfade(float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);
}   