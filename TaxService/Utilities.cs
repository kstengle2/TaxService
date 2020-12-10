using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaxService
{
    public static class Utilities
    {
        
        public static bool IsUSZipCode(string zipCode)
        {
            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var validZipCode = true;
            if (!Regex.Match(zipCode, _usZipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }
    }
}
