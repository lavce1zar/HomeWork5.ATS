using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class Port
    {
        public PortStatus Status { get; set; }

        public Port()
        {
            Status = PortStatus.Disconnected;
        }
    }
}
