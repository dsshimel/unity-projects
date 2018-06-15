using UnityEngine;

/**
* A strategy collection consisting of one instance of each type of strategy
* available. 
*/
public interface IBundle
{
    void ApplyStrategies(float timeNow, float timeDelta);
    void ApplyStrategiesFade(IBundle bundleFadeIn, float fadeOutPercent, float timeNow, float timeDelta);

    void SetIntensities(float intensity);

    IStrategy<Vector3> MovementStrategy
    {
        get;
    }
    IStrategy<Color> ColorStrategy
    {
        get;
    }
    IStrategy<Gradient> TrailsStrategy
    {
        get;
    }
    IStrategy<Vector3> SizeStrategy
    {
        get;
    }
    IStrategy<float> ParticleSizeStrategy
    {
        get;
    }
}