using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.TaxCalculators.Interface;

namespace TaxService.TaxCalculators
{
    public class NewCalculator : ITaxCalculator
    {
        public double GetShippingCost(string zip, double orderTotal)
        {
            throw new NotImplementedException();
        }

        public double GetTaxRate(string zip)
        {
            return 5.3;
        }
    }
}
