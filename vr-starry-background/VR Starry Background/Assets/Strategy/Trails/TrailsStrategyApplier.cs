using UnityEngine;
using System.Collections.Generic;

// TODO: The strategy appliers are responsible for picking which manipulator method
// to call. So perhaps it makes more sense for the strategies to be tasked only with
// providing a kind of value (e.g. Vector3, Color, Gradient, etc.). This might be a
// more flexible approach because right now the strategies at least nominally know
// what property they're manipulating.
public class TrailsStrategyApplier : IStrategyApplier<Gradient>
{
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;

    public TrailsStrategyApplier(Manipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
    }

    void IStrategyApplier<Gradient>.Apply(IStrategy<Gradient> strategy, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
            // TODO: I'm no longer setting the size of the particles relative to the size of the comets, e.g.
            // manipulator.SetParticleRadius(gameObjectId, sizeStrategy.ComputeValue(gameObjectId, 0, 0).magnitude);
        }
    }

    void IStrategyApplier<Gradient>.ApplyFade(IStrategy<Gradient> strategyOut, IStrategy<Gradient> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, CrossfadeValues.FadeGradient(valueOut, valueIn, fadeOutPercent));
        }
    }
}