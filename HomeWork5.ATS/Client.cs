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
        public event CallingHandler NotifyAts;

        public string? Name { get; set; }
        
        public Terminal? Terminal;

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

            if (this.Terminal.CanCall(recipient.Terminal))
            {
                this.Terminal.Port.Status = PortStatus.Call;
                recipient.Terminal.Port.Status = PortStatus.Call;

                var call = new Call(this, recipient, dateTime);

                NotifyAts += AtsCompany.StartCall;
                NotifyAts?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));
                NotifyAts -= AtsCompany.StartCall;

                // var thread = new Thread(Terminal.Call);

                // thread.Start();

                dateTime = dateTime.AddMilliseconds(new Random().Next(60000, 300000));

                NotifyAts += AtsCompany.EndCall;
                NotifyAts?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));
                NotifyAts -= AtsCompany.EndCall;

                call.EndOfCall = dateTime;
                Calls.Add(call);

                this.Terminal.Port.Status = PortStatus.Connected;
                recipient.Terminal.Port.Status = PortStatus.Connected;
            }
            else
            {
                NotifyAts += AtsCompany.FailCall;
                NotifyAts?.Invoke(this, new CallingNotificationEventArgs(recipient, dateTime));
                NotifyAts -= AtsCompany.FailCall;
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
    }
}
