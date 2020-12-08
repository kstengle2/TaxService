using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.TaxCalculators.Interface;

namespace TaxService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxServiceController : ControllerBase
    {
        public delegate ITaxCalculator TaxCalcMapper(string key);
        private TaxCalcMapper _taxCalculator;
        public TaxServiceController(TaxCalcMapper taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }
        public IActionResult Index()
        {
            return Ok(new { results = _taxCalculator("NewCalculator").GetTaxRate("07747") });            
        }

        [HttpGet("GetTaxRate")]
        public IActionResult GetTaxRate(int orderId)
        {
            return Ok(new { results = _taxCalculator("NewCalculator").GetTaxRate("07747")});
        }
        [HttpGet("GetShippingCost")]
        public IActionResult GetShippingCost(double TaxRate, double OrderTotal)
        {
            return Ok(new { results = _taxCalculator("TaxJar").GetTaxRate("07747") });
        }
    }
}
