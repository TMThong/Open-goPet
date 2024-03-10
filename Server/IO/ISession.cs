using Gopet.IO;
using System.Net.Sockets;

public interface ISession
{
    bool clientOK { get; set; }
    Socket sc { get; set; }

    void Close();
    void Exit(object state);
    bool isConnected();
    void readKey();
    void run();
    void sendMessage(Message message);
    void setClientOK(bool ok);
    void setHandler(IHandleMessage messageHandler);
    void setReader(MsgReader r);
    void setSender(MsgSender s);
}