namespace CountingApp.Droid
{
    public class ActivityMediator
    {
        public delegate void MessageReceivedEventHandler(string message);
        public event MessageReceivedEventHandler ActivityMessageReceived;

        public void Send(string response)
        {
            ActivityMessageReceived?.Invoke(response);
        }
    }
}