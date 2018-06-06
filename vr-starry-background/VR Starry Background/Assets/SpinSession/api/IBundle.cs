﻿using UnityEngine;

/**
* A strategy collection consisting of one instance of each type of strategy
* available. 
*/
public interface IBundle
{
    void ApplyStrategies(float timeNow, float timeBefore);
    void ApplyStrategiesFade(IBundle bundleFadeIn, float fadeOutPercent, float timeNow, float timeBefore);

    void SetIntensities(float intensity);

    IStrategy<Vector3> GetMovementStrategy();
    IColorStrategy GetColorStrategy();
    ITrailsStrategy GetTrailsStrategy();
    ISizeStrategy GetSizeStrategy();
}