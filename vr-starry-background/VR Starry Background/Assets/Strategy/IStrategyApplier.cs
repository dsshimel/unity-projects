public interface IStrategyApplier<TValue>
{
    void Apply(IStrategy<TValue> strategy, float timeNow, float timeBefore);

    void ApplyFade(IStrategy<TValue> strategyOut, IStrategy<TValue> strategyIn, float fadeOutPercent, float timeNow, float timeBefore);
}