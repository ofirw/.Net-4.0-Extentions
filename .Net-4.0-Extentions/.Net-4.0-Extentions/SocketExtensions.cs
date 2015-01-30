namespace Net_4._0_Extentions
{
    using System.Net.Sockets;

    public static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            bool blockingState = socket.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                socket.Blocking = false;
                socket.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                return e.NativeErrorCode.Equals(10035);
            }
            finally
            {
                socket.Blocking = blockingState;
            }
        }
    }
}