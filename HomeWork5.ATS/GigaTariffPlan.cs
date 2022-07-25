using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class GigaTariffPlan : TariffPlan
    {
        public override double PricePerOneMinute => 3.12d;

        public override double SubscriptionFee => 9.99d;

        public override string Name => "GigaTariffPlan";
    }
}
