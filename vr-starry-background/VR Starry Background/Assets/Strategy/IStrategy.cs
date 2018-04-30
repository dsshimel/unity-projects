public interface IStrategy<T> : IStrategyUntyped
{
    // Get the value to be applied.
    T ComputeStrategyValue(int gameObjectId, float timeNow, float timeBefore);
}   