public class Interval
{
    public readonly float ActiveIntervalSeconds;
    public readonly float RestIntervalSeconds;
    public readonly float FadeSeconds;

    private bool isFinished = false;

    public Interval(float activeIntervalSeconds, float restIntervalSeconds, float fadeSeconds)
    {
        if (activeIntervalSeconds < 0 || restIntervalSeconds < 0 || fadeSeconds < 0)
        {
            throw new System.ArgumentException("intervals must be positive");
        }
        ActiveIntervalSeconds = activeIntervalSeconds;
        RestIntervalSeconds = restIntervalSeconds;
        FadeSeconds = fadeSeconds;
    }

    public bool IsActive(float time)
    {
        return 0 <= time && time < ActiveIntervalSeconds;
    }

    public bool IsResting(float time)
    {
        return ActiveIntervalSeconds <= time && time < FadeStartTime;
    }

    public bool IsFading(float time)
    {
        return FadeStartTime <= time && time < FadeStartTime + FadeSeconds;
    }

    public float GetFadePercent(float time)
    {
        // Unless we're fading, return the max.
        if (!IsFading(time))
        {
            return 1;
        }

        float timeIntoFade = time - FadeStartTime;
        if (timeIntoFade > FadeSeconds)
        {
            return 0;
        }

        return 1.0f - (timeIntoFade / FadeSeconds);
    }

    public float FadeStartTime
    {
        get
        {
            return ActiveIntervalSeconds + RestIntervalSeconds;
        }
    }

    public void Finish()
    {
        isFinished = true;
    }

    public bool IsFinished()
    {
        return isFinished;
    }
}
