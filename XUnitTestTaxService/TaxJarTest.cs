using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Configuration;
using System.IO;
using TaxService.TaxCalculators;
using TaxService.TaxCalculators.Interface;
using Xunit;
using static TaxService.Controllers.TaxServiceController;

namespace XUnitTestTaxService
{
    
    public class TaxJarTest
    {
        public double taxrateNJ = 0.06625;
        
        
        [Fact]
        public void TaxJar_GetTaxRate_Success()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            TaxJar tj = new TaxJar(configuration);
            string zipcode = "07747";
            double rate = tj.GetTaxRate(zipcode);
            Assert.Equal(taxrateNJ, rate);

        }
        [Fact]
        public void TaxJar_GetTotalTax_Success()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            TaxJar tj = new TaxJar(configuration);            
            string zipcode = "07747";
            double orderTotal = 10.00;
            double totaltax = tj.GetTotalTax(zipcode, orderTotal);
            Assert.Equal(orderTotal * taxrateNJ, totaltax);

        }
        
    }
}
