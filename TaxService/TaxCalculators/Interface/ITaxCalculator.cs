﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxService.TaxCalculators.Interface
{
    public interface ITaxCalculator
    {
        double GetTotalTax(string zip, double orderTotal);
        double GetTaxRate(string zip);
    }
}
