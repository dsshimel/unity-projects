﻿public interface IStrategyUntyped
{
    // Set the intensity of the strategy.
    void SetIntensity(float intensity);

    void Apply(float timeNow, float timeBefore);

    CometProperty Property
    {
        get;
    }
}   