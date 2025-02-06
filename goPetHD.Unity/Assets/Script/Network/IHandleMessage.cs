

using Gopet.IO;
/// <summary>
/// Interface xử lí tin nhắn
/// </summary>
public interface IHandleMessage {
    /// <summary>
    /// Xử lí tin nhắn
    /// </summary>
    /// <param name="ms"></param>
    void onMessage(Message ms);
    /// <summary>
    /// Khi ngắt kết nối
    /// </summary>
    void onDisconnected();
}
