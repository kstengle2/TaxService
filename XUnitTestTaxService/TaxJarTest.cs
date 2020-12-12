using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Configuration;
using System.IO;
using TaxService.Controllers;
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
        [Fact]
        public void TaxJar_GetTaxRateController_InValidZip()
        {
            var mock = new Mock<TaxCalcMapper>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            mock.Setup(x => x.Invoke(It.IsAny<string>())).Returns(new TaxJar(configuration));            
            var taxServiceController = new TaxServiceController(mock.Object);
            var response = taxServiceController.GetTaxRate(zip: "123");
            var payload = response as OkObjectResult;
            Assert.Equal(200, payload.StatusCode);
            Assert.Contains("Invalid Zip Code", payload.Value.ToString());                  
        }
        [Fact]
        public void TaxJar_GetTaxRateController_ValidZip()
        {
            var mock = new Mock<TaxCalcMapper>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            mock.Setup(x => x.Invoke(It.IsAny<string>())).Returns(new TaxJar(configuration));
            var taxServiceController = new TaxServiceController(mock.Object);
            var response = taxServiceController.GetTaxRate(zip: "07747");
            var payload = response as OkObjectResult;                        
            Assert.Equal(200, payload.StatusCode);
            Assert.Contains(taxrateNJ.ToString(), payload.Value.ToString());
            
        }
        [Fact]
        public void TaxJar_GetTotalTaxController_InValidZip()
        {
            var mock = new Mock<TaxCalcMapper>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            mock.Setup(x => x.Invoke(It.IsAny<string>())).Returns(new TaxJar(configuration));
            var taxServiceController = new TaxServiceController(mock.Object);
            var response = taxServiceController.GetTotalTax(zip: "123", orderTotal: 100);
            var payload = response as OkObjectResult;
            Assert.Equal(200, payload.StatusCode);
            Assert.Contains("Invalid Zip Code", payload.Value.ToString());
        }
        [Fact]
        public void TaxJar_GetTotalTaxController_ValidZip()
        {
            var mock = new Mock<TaxCalcMapper>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            mock.Setup(x => x.Invoke(It.IsAny<string>())).Returns(new TaxJar(configuration));
            var taxServiceController = new TaxServiceController(mock.Object);
            var response = taxServiceController.GetTotalTax(zip: "07747", orderTotal: 100);
            var payload = response as OkObjectResult;
            Assert.Equal(200, payload.StatusCode);
            Assert.Contains((taxrateNJ*100).ToString(), payload.Value.ToString());

        }
    }
}
