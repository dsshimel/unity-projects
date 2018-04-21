public class Interval
{
    public readonly float ActiveIntervalSeconds;
    public readonly float RestIntervalSeconds;

    private bool isFinished = false;

    public Interval(float activeIntervalSeconds, float restIntervalSeconds)
    {
        if (activeIntervalSeconds < 0 || restIntervalSeconds < 0)
        {
            throw new System.ArgumentException("intervals must be positive");
        }
        ActiveIntervalSeconds = activeIntervalSeconds;
        RestIntervalSeconds = restIntervalSeconds;
    }

    public bool IsActive(float time)
    {
        return 0 <= time && time < ActiveIntervalSeconds;
    }

    public bool IsResting(float time)
    {
        return ActiveIntervalSeconds <= time && time < ActiveIntervalSeconds + RestIntervalSeconds;
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
