
public class Session : ISession
{
    private IPlaylist playlist;
    private float countdownTime;

    public Session(IPlaylist playlist, float countdownTime)
    {
        this.playlist = playlist;
        this.countdownTime = countdownTime;
    }
}