using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class NoLimitsTariffPlan : TariffPlan
    {
        public override double PricePerOneMinute => 1.01d;

        public override double SubscriptionFee => 29.99d;

        public override string Name => "NoLimitsTariffPlan";
    }
}
