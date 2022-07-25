using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class AtsCompany
    {
        public List<Port>? Ports { get; private set; }

        public List<Terminal>? Terminals { get; private set; }

        public List<Client>? Clients { get; private set; }

        public List<string>? Numbers { get; private set; }

        public TariffPlan[] TariffPlans = { new DriveTariffPlan(), new GigaTariffPlan(), new NoLimitsTariffPlan() };

        public List<Dictionary<Client, List<Call>>> BaseOfCalls { get; private set; }

        public AtsCompany()
        {
            Ports = new List<Port>();
            Numbers = new List<string>();
            Terminals = new List<Terminal>();
            Clients = new List<Client>();
            BaseOfCalls = new List<Dictionary<Client, List<Call>>>();


            for (var i = 0; i < 100; i++)
            {
                var number = GenerateNumber();

                if (!Numbers.Contains(number))
                {
                    Numbers.Add(number);
                }
            }

            foreach (var number in Numbers)
            {
                var port = new Port();
                Terminals.Add(new Terminal(number, port));
                Ports.Add(port);
            }
        }

        public void SignContract(Client client)
        {
            Clients.Add(client);
            var indexOfClient = Clients.IndexOf(client);
            client.Terminal = Terminals[indexOfClient];
            client.Plan = TariffPlans[new Random().Next(3)];
        }

        public void EndingMonth(int month)
        {
            var monthReport = new Dictionary<Client, List<Call>>();

            foreach(var client in Clients)
            {
                monthReport.Add(client, client.Calls);
                client.LastMonthCalls = client.Calls;
                LoggingToFile(client, month);
                client.Calls = new List<Call>();
                client.IsChangedPlanThisMonth = false;
            }

            BaseOfCalls.Add(monthReport);
        }

        public static void StartCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {sender.Name} starting a call to {eventArgs.Recipient.Name} at {eventArgs.DateTime}");
        }

        public static void FailCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {sender.Name} can't reach {eventArgs.Recipient.Name} at {eventArgs.DateTime}. Port status is {eventArgs.Recipient.Terminal.Port.Status}");
        }

        public static void EndCall(Client sender, CallingNotificationEventArgs eventArgs)
        {
            Console.WriteLine($"Client {sender.Name} ending a call to {eventArgs.Recipient.Name} at {eventArgs.DateTime}");
        }

        public void LoggingToFile(Client client, int month)
        {
            var filePath = $"{client.Name} for {month} month.txt";

            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine($"Report for {month} for client {client.Name} (tariff plan: {client.Plan.Name}):");
                sw.WriteLine();

                var summ = 0d;

                foreach (var call in client.LastMonthCalls)
                {
                    summ += call.Cost;
                    sw.WriteLine(call.ToString());
                }

                sw.WriteLine();
                sw.WriteLine($"Total Cost for month - {summ + client.Plan.SubscriptionFee:c2} (subscription fee - {client.Plan.SubscriptionFee:c2})");
            }
        }

        private string GenerateNumber()
        {
            var builder = new StringBuilder();
            var digit = new Random().Next(1, 10);
            builder.Append(digit);

            for (var i = 0; i < 3; i++)
            {
                digit = new Random().Next(10);
                builder.Append(digit);
                digit = new Random().Next(10);
                builder.Append(digit);
                if (i != 2)
                {
                    builder.Append('-');
                }
            }

            return builder.ToString();
        }
    }
}
