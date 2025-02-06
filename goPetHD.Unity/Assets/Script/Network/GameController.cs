using Gopet.IO;

public class GameController : IHandleMessage
{
    private Session session;
    public GameController(Session session)
    {
        this.session = session;
    }
    public void onDisconnected()
    {
        
    }

    public void onMessage(Message ms)
    {
         
    }
}