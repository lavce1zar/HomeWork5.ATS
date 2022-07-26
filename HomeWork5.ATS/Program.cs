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

            ReadingNamesFromFile(names, filePath);

            FillingCompany(names, atsCompany);

            GeneratingTime(atsCompany);

        }

        private static void FillingCompany(List<string> names, AtsCompany atsCompany)
        {
            foreach (var name in names)
            {
                atsCompany.SignContract(new Client(name));
            }

            foreach (var client in atsCompany.Clients)
            {
                client.Terminal.Port.Status = PortStatus.Connected;
            }
        }

        private static void GeneratingTime(AtsCompany atsCompany)
        {
            DateTime dateTime = DateTime.Now;

            for (int i = 0; i < 60; i++)
            {
                foreach (var client in atsCompany.Clients)
                {
                    var tempTime = dateTime;
                    var randomClient = atsCompany.Clients[new Random().Next(atsCompany.Clients.Count)];

                    if (client != randomClient)
                    {
                        client.CallingToClient(randomClient, ref dateTime);
                    }
                    
                    if (tempTime.Month != dateTime.Month)
                    {
                        atsCompany.EndingMonth(tempTime.Month);
                        atsCompany.Clients[0].SortCallsByDuration(); // demonstrate sorting Calls
                        atsCompany.LoggingToFile(atsCompany.Clients[0], tempTime.Month);
                    }
                }

                var tempDateTime = dateTime.AddDays(1);

                if (tempDateTime.Month != dateTime.Month)
                {
                    atsCompany.EndingMonth(dateTime.Month);
                    atsCompany.Clients[0].SortCallsByDuration(); // demonstrate sorting Calls
                    atsCompany.LoggingToFile(atsCompany.Clients[0], dateTime.Month);
                }

                dateTime = tempDateTime;
            }
        }

        private static void ReadingNamesFromFile(List<string> names, string filePath)
        {
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
        }
    }
}