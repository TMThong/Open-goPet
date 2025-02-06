using Gopet.IO;
using UnityEngine;

public class GameController : IHandleMessage
{
    private Session session;
    public GameController(Session session)
    {
        this.session = session;
    }
    public void onDisconnected()
    {
        Debug.Log("Disconnected");
    }

    public void onMessage(Message ms)
    {
        Debug.Log("Received message: " + ms.id);
    }
}