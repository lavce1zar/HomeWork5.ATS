using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class Terminal
    {
        public string? Number { get; }

        public Port? Port;

        public Terminal(string? number, Port? port)
        {
            Number = number;
            Port = port;
        }

        public bool CanCall(Terminal terminal) => terminal.Port.Status == PortStatus.Connected;
    }
}
