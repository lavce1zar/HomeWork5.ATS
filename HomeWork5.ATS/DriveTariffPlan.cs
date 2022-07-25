using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork5.ATS
{
    public class DriveTariffPlan : TariffPlan
    {
        public override double PricePerOneMinute => 0.544d;

        public override double SubscriptionFee => 15.99d;

        public override string Name => "DriveTariffPlan";
    }
}
