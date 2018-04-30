public class Interval
{
    public readonly float ActiveIntervalSeconds;
    public readonly float RestIntervalSeconds;
    public readonly float FadeOutSeconds;

    private bool isFinished = false;

    public Interval(float activeIntervalSeconds, float restIntervalSeconds, float fadeOutSeconds)
    {
        if (activeIntervalSeconds < 0 || restIntervalSeconds < 0)
        {
            throw new System.ArgumentException("intervals must be positive");
        }
        if (fadeOutSeconds > activeIntervalSeconds + restIntervalSeconds)
        {
            throw new System.ArgumentException("can't xfade longer than the total interval");
        }
        ActiveIntervalSeconds = activeIntervalSeconds;
        RestIntervalSeconds = restIntervalSeconds;
        FadeOutSeconds = fadeOutSeconds;
    }

    public bool IsActive(float time)
    {
        return 0 <= time && time < ActiveIntervalSeconds;
    }

    public bool IsResting(float time)
    {
        return ActiveIntervalSeconds <= time && time < ActiveIntervalSeconds + RestIntervalSeconds;
    }

    public float GetFadeOutPercent(float time)
    {
        if (!IsActive(time))
        {
            return 0;
        }

        float fadeOutStartTime = ActiveIntervalSeconds + RestIntervalSeconds - FadeOutSeconds;
        float timeIntoFadeOut = time - fadeOutStartTime;
        if (timeIntoFadeOut < 0)
        {
            return 1;
        }

        return 1.0f - (timeIntoFadeOut / FadeOutSeconds);
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
