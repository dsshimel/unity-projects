
public class Session : ISession
{
    private IPlaylist playlist;
    private float countdownTime;
    private bool countdownFinished;
    private bool isRunning;
    private float time;

    public Session(IPlaylist playlist, float countdownTime)
    {
        this.playlist = playlist;
        this.countdownTime = countdownTime;
        countdownFinished = false;
        isRunning = false;
        time = 0;
    }

    public void Start()
    {
        if (isRunning)
        {
            throw new System.ApplicationException("can't start a running session");
        }

        // Apply the strategies of the first bundle in the playlist at time 0 to initialize
        // everything.
        playlist.ApplyStrategies(0, 0);

        isRunning = true;
    }

    public void Pause()
    {
        if (!isRunning)
        {
            throw new System.ApplicationException("can't pause a stopped session");
        }

        isRunning = false;
    }

    public bool IsCountingDown()
    {
        return isRunning && 0 <= time && time < countdownTime;
    }

    public float IncrementTime(float delta)
    {
        if (isRunning)
        {
            time += delta;
            if (!IsCountingDown())
            {
                if (countdownFinished)
                {
                    // Tell the playlist to do its thing
                    playlist.IncrementTime(delta);
                } else
                {
                    // We just finished counting down, so the playlist needs to start
                    playlist.Play();
                    playlist.IncrementTime(delta);
                    countdownFinished = true;
                }
            }
        }
        return time;
    }
}