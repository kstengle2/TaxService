using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TaxService.TaxCalculators.Interface;

namespace TaxService.TaxCalculators
{

    public class TaxJar : ITaxCalculator
    {
        public string taxJarApiKey, TaxJarApiRateEnpoint;
        public TaxJar(IConfiguration configuration)
        {
            taxJarApiKey = configuration.GetValue<string>("AppConstants:TaxJarApiKey");
            TaxJarApiRateEnpoint = configuration.GetValue<string>("AppConstants:TaxJarApiRateEnpoint");
        }


        public double GetTotalTax(string zip, double orderTotal)
        {
            double rate = GetRate(zip);
            return orderTotal * rate;
        }

        public double GetTaxRate(string zip)
        {               
            return GetRate(zip);
        }
        private double GetRate(string zip)
        {
            string result = Utilities.CallRestAPI(TaxJarApiRateEnpoint + zip, taxJarApiKey);
            var jsonObject = JsonConvert.DeserializeObject<Rootobject>(result).rate;
            return double.Parse(jsonObject.combined_rate);
            
        }
        /* The classes are to Deserialize the return from TaxJar. 
         * I keep these here because they are only for the TaxJar return and not needed anywhere else
         */
        public class Rootobject
        {
            public Rate rate { get; set; }
        }

        public class Rate
        {
            public string city { get; set; }
            public string city_rate { get; set; }
            public string combined_district_rate { get; set; }
            public string combined_rate { get; set; }
            public string country { get; set; }
            public string country_rat { get; set; }
            public string county { get; set; }
            public string county_rate { get; set; }
            public bool freight_taxable { get; set; }
            public string state { get; set; }
            public string state_rate { get; set; }
            public string zip { get; set; }
        }

    }
}

