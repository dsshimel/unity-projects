
public class Session : ISession
{
    private IPlaylist playlist;

    public Session(IPlaylist playlist)
    {
        this.playlist = playlist;
    }
}