public interface IStrategyApplier<TValue, TStrategy> where TStrategy : IStrategy<TValue>
{
    void Apply(TStrategy strategy, float timeNow, float timeBefore);

    void ApplyFade(TStrategy strategyOut, TStrategy strategyIn, float fadeOutPercent, float timeNow, float timeBefore);
}