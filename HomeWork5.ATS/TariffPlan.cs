using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public abstract class TariffPlan
    {
        public abstract string Name { get; }
        
        public abstract double PricePerOneMinute { get; }

        public abstract double SubscriptionFee { get; }
    }
}
