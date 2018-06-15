public class Interval
{
    public readonly float ActiveIntervalSeconds;
    public readonly float SlowdownIntervalSeconds;
    public readonly float RestIntervalSeconds;
    public readonly float FadeIntervalSeconds;

    private bool isFinished = false;

    public Interval(
        float activeIntervalSeconds,
        float slowdownIntervalSeconds,
        float restIntervalSeconds,
        float fadeSeconds)
    {
        if (activeIntervalSeconds < 0 || slowdownIntervalSeconds < 0 || restIntervalSeconds < 0 || fadeSeconds < 0)
        {
            throw new System.ArgumentException("intervals must be positive");
        }
        ActiveIntervalSeconds = activeIntervalSeconds;
        SlowdownIntervalSeconds = slowdownIntervalSeconds;
        RestIntervalSeconds = restIntervalSeconds;
        FadeIntervalSeconds = fadeSeconds;
    }

    public bool IsActive(float time)
    {
        return MathHelper.IsBetween(time, 0, SlowingDownStartTime);
    }

    public bool IsSlowingDown(float time)
    {
        return MathHelper.IsBetween(time, SlowingDownStartTime, RestingStartTime);
    }

    public bool IsResting(float time)
    {
        return MathHelper.IsBetween(time, RestingStartTime, FadeStartTime);
    }

    public bool IsFading(float time)
    {
        return MathHelper.IsBetween(time, FadeStartTime, FadeEndTime);
    }

    public float GetFadePercent(float time)
    {
        // Unless we're fading, return the max.
        if (!IsFading(time))
        {
            return 1;
        }

        float timeIntoFade = time - FadeStartTime;
        if (timeIntoFade > FadeIntervalSeconds)
        {
            return 0;
        }

        return 1.0f - (timeIntoFade / FadeIntervalSeconds);
    }

    public float GetSlowdownIntensity(float time)
    {
        if (time < SlowingDownStartTime)
        {
            return 1.0f;
        }
        if (time >= RestingStartTime)
        {
            return 0.5f;
        }

        float timeIntoSlowdown = time - SlowingDownStartTime;
        return 1.0f - (timeIntoSlowdown / SlowdownIntervalSeconds);
        
    }

    public float SlowingDownStartTime
    {
        get
        {
            return ActiveIntervalSeconds;
        }
    }

    public float RestingStartTime
    {
        get
        {
            return SlowingDownStartTime + SlowdownIntervalSeconds;
        }
    }

    public float FadeStartTime
    {
        get
        {
            return RestingStartTime + RestIntervalSeconds;
        }
    }

    public float FadeEndTime
    {
        get
        {
            return FadeStartTime + FadeIntervalSeconds;
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
