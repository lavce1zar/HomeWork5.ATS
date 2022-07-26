using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public delegate void CallingHandler(Client sender, CallingNotificationEventArgs eventArgs);

    public class Client
    {
        public event CallingHandler NotifyStartCall;
        public event CallingHandler NotifyEndCall;
        public event CallingHandler NotifyFailCall;

        public string? Name { get; private set; }
        
        public Terminal? Terminal { get; set; }

        public TariffPlan? Plan { get; set; }

        public List<Call>? Calls { get; set; }

        public List<Call>? LastMonthCalls { get; set; }

        public bool IsChangedPlanThisMonth = false;

        public Client(string? name)
        {
            Name = name;

            Calls = new List<Call>();
            LastMonthCalls = new List<Call>();
        }

        public void CallingToClient(Client recipient, ref DateTime dateTime)
        {
            ConnectTerminalToPort();

            if (Terminal.CanCall(recipient.Terminal))
            {
                var call = new Call(this, recipient, dateTime);

                NotifyStartCall?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));

                dateTime = dateTime.AddMilliseconds(new Random().Next(60000, 300000));

                NotifyEndCall?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));
                
                call.EndOfCall = dateTime;
                Calls.Add(call);
            }
            else
            {
                NotifyFailCall?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));
            }
        }

        public void ConnectTerminalToPort()
        {
            if (Terminal?.Port?.Status == PortStatus.Disconnected)
            {
                Terminal.Port.Status = PortStatus.Connected;
            }
        }

        public void DisconnectTerminalToPort()
        {
            if (Terminal?.Port?.Status == PortStatus.Connected)
            {
                Terminal.Port.Status = PortStatus.Disconnected;
            }
        }

        public void ChangeTariffPlan(TariffPlan newTariffPlan)
        {
            if (!IsChangedPlanThisMonth)
            {
                Plan = newTariffPlan;
                IsChangedPlanThisMonth = true;
            }
            else
            {
                Console.WriteLine($"Changing of the tariff plan for client {Name} was declined. The tariff plan has already changed this month");
            }
        }

        public void SortCallsByDate()
        {
            var sortedByDate = LastMonthCalls?.OrderBy(x => x.StartOfCall).ToList();
            LastMonthCalls = sortedByDate;
        }

        public void SortCallsByCost()
        {
            var sortedByCost = LastMonthCalls?.OrderBy(x => x.Cost).ToList();
            LastMonthCalls = sortedByCost;
        }

        public void SortCallsByRecipient()
        {
            var sortedByRecipient = LastMonthCalls?.OrderBy(x => x.Recipient).ToList();
            LastMonthCalls = sortedByRecipient;
        }

        public void SortCallsByDuration()
        {
            var sortedByDuration = LastMonthCalls?.OrderBy(x => x.Duration).ToList();
            LastMonthCalls = sortedByDuration;
        }

        public void StartCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {this.Name} starting a call to {eventArgs.Recipient.Name} at {eventArgs.DateTime}");
        }

        public void FailCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {this.Name} can't reach {eventArgs.Recipient.Name} at {eventArgs.DateTime}. Port status is {eventArgs.Recipient.Terminal.Port.Status}");
        }

        public void EndCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {this.Name} ending a call to {eventArgs.Recipient.Name} at {eventArgs.DateTime}");
        }
    }
}
