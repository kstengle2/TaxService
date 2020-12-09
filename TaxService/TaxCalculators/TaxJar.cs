using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaxService.TaxCalculators.Interface;

namespace TaxService.TaxCalculators
{

    public class TaxJar : ITaxCalculator
    {
        public string taxJarApiKey;
        public TaxJar(IConfiguration configuration)
        {
            taxJarApiKey = configuration.GetValue<string>("AppConstants:TaxJarApiKey");
        }


        public double GetTotalTax(string zip, double orderTotal)
        {
            double ret = CallTaxJar("https://api.taxjar.com/v2/rates/" + zip);
            return orderTotal * ret;
        }

        public double GetTaxRate(string zip)
        {
               double ret = CallTaxJar("https://api.taxjar.com/v2/rates/" + zip);
            return ret;
        }
        private double CallTaxJar(string url)
        {
            HttpWebRequest myHttpWebRequest = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Timeout = 15000;
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + taxJarApiKey);

            WebResponse myWebResponse = myHttpWebRequest.GetResponse();
            string result = "";
            using (var reader = new StreamReader(myWebResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            var jsonObject = JsonConvert.DeserializeObject<Rootobject>(result).rate;
            return double.Parse(jsonObject.combined_rate);
        }

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

