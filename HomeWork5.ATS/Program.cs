using System.Text;

namespace HomeWork5.ATS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>();
            var filePath = "names.txt";

            var atsCompany = new AtsCompany();

            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        names.Add(line);
                    }
                }
            }

            foreach(var name in names)
            {
                atsCompany.SignContract(new Client(name));
            }

            foreach(var client in atsCompany.Clients)
            {
                client.Terminal.Port.Status = PortStatus.Connected;
            }

            DateTime dateTime = DateTime.Now;

            for (int i = 0; i < 60; i++)
            {
                foreach (var client in atsCompany.Clients)
                {
                    var tempTime = dateTime;
                    client.CallingToClient(atsCompany.Clients[new Random().Next(atsCompany.Clients.Count)], ref dateTime);
                    if (tempTime.Month != dateTime.Month)
                    {
                        atsCompany.EndingMonth(tempTime.Month);
                        atsCompany.Clients[0].SortCallsByDuration();
                        atsCompany.LoggingToFile(atsCompany.Clients[0], tempTime.Month);
                    }
                }

                var tempDateTime = dateTime.AddDays(1);

                if (tempDateTime.Month != dateTime.Month)
                {
                    atsCompany.EndingMonth(dateTime.Month);
                    atsCompany.Clients[0].SortCallsByDuration();
                    atsCompany.LoggingToFile(atsCompany.Clients[0], dateTime.Month);
                }

                dateTime = tempDateTime;
            }

            


            
        }
    }
}