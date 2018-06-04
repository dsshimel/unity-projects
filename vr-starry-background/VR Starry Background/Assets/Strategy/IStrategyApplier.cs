public interface IStrategyApplier<T>
{
    void Apply(IStrategy<T> strategy, float timeNow, float timeBefore);
    void ApplyFade(IStrategy<T> strategyOut, IStrategy<T> strategyIn, float fadeOutPercent, float timeNow, float timeBefore);
}