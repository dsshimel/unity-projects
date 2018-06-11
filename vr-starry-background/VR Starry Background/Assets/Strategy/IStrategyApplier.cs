public interface IStrategyApplier<TValue>
{
    void Apply(float timeNow, float timeBefore);
    void ApplyFade(IStrategy<TValue> strategyIn, float fadeOutPercent, float timeNow, float timeBefore);
    IStrategy<TValue> Strategy
    {
        get;
    }
}