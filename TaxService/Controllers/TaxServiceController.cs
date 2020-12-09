using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.TaxCalculators.Interface;

namespace TaxService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxServiceController : ControllerBase
    {
        public delegate ITaxCalculator TaxCalcMapper(string key);
        private TaxCalcMapper _taxCalculator;
        public TaxServiceController(TaxCalcMapper taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }        
        [HttpGet("GetTaxRate")]
        [Route("GetTaxRate")]
        public IActionResult GetTaxRate(string zip)
        {            
            return Ok(new { results = _taxCalculator("TaxJar").GetTaxRate(zip)});
        }
        [HttpGet("GetTotalTax")]
        [Route("GetTotalTax")]
        public IActionResult GetTotalTax(string zip, double OrderTotal)
        {
            return Ok(new { results = _taxCalculator("TaxJar").GetTotalTax(zip, OrderTotal) });
        }
    }
}
