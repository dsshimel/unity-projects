using System.Collections.Generic;

public class Bundle : IBundle
{
    private IList<IStrategy> strategies;

    public Bundle(IMovementStrategy movementStrat, IColorStrategy colorStrat, ITrailsStrategy trailsStrat, ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategy>();
        strategies.Add(movementStrat);
        strategies.Add(colorStrat);
        strategies.Add(trailsStrat);
        strategies.Add(sizeStrat);
    }

    public void ApplyStrategies(float time)
    {
        foreach (IStrategy strat in strategies)
        {
            strat.ApplyStrategy(time);
        }
    }

    public void SetIntensities(float intensity)
    {
        foreach (IStrategy strat in strategies)
        {
            strat.SetIntensity(intensity);
        }
    }
}