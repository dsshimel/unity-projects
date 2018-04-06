namespace SpinSession
{
    public class Interval
    {
        public readonly float activeIntervalSeconds;
        public readonly float restIntervalSeconds;

        private bool isFinished = false;

        public Interval(float activeIntervalSeconds, float restIntervalSeconds)
        {
            this.activeIntervalSeconds = activeIntervalSeconds;
            this.restIntervalSeconds = restIntervalSeconds;
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
}
