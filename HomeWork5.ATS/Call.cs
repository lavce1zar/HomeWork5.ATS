using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class Call
    {
        public Client? Caller { get; set; }

        public Client? Recipient { get; set; }
        
        public DateTime StartOfCall { get; set; }

        public DateTime EndOfCall { get; set; }

        public TimeSpan Duration { get => EndOfCall - StartOfCall; }

        public double Cost { get => Math.Ceiling(Duration.TotalMinutes) * Caller.Plan.PricePerOneMinute; }

        public Call(Client caller, Client recipient, DateTime startOfCall)
        {
            Caller = caller;
            Recipient = recipient;
            StartOfCall = startOfCall;
        }

        public Call(Client caller, Client recipient, DateTime startOfCall, DateTime endOfCall)
        {
            Caller = caller;
            Recipient = recipient;
            StartOfCall = startOfCall;
            EndOfCall = endOfCall;
        }

        public override string ToString()
        {
            return $"Call to {Recipient.Name}\t\tDate: {StartOfCall.Date:d}\tDuration: {Duration.Minutes} minutes and {Duration.Seconds} seconds\tCost: {Cost:c2}";
        }
    }
}
