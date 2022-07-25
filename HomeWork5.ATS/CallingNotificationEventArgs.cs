namespace HomeWork5.ATS
{
    public class CallingNotificationEventArgs
    {
        public Client Recipient { get; set; }

        public DateTime DateTime { get; set; }

        public CallingNotificationEventArgs(Client recipient, DateTime dateTime)
        {
            Recipient = recipient;
            DateTime = dateTime;
        }
    }
}