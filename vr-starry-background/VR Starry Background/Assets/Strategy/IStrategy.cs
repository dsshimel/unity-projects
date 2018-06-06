public interface IStrategy<T> : IStrategyUntyped
{
    // Get the value to be applied.
    T ComputeValue(int gameObjectId, float timeNow, float timeDelta);
}   