using UnityEngine;
using System.Collections.Generic;

// TODO: The strategy appliers are responsible for picking which manipulator method
// to call. So perhaps it makes more sense for the strategies to be tasked only with
// providing a kind of value (e.g. Vector3, Color, Gradient, etc.). This might be a
// more flexible approach because right now the strategies at least nominally know
// what property they're manipulating.
public class TrailsStrategyApplier : IStrategyApplier<Gradient, ITrailsStrategy>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public TrailsStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GameObjectIds;
    }

    void IStrategyApplier<Gradient, ITrailsStrategy>.Apply(ITrailsStrategy strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
            // TODO: I'm no longer setting the size of the particles relative to the size of the comets, e.g.
            // manipulator.SetParticleRadius(gameObjectId, sizeStrategy.ComputeValue(gameObjectId, 0, 0).magnitude);
        }
    }

    void IStrategyApplier<Gradient, ITrailsStrategy>.ApplyFade(ITrailsStrategy strategyOut, ITrailsStrategy strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            // TODO: Figure out how to fade between gradients
            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore));

            //var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore);
            //var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeBefore);

        }
    }
}