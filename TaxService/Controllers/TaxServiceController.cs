using Microsoft.AspNetCore.Mvc;
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
            return Utilities.IsUSZipCode(zip) ?
                Ok(new { results = _taxCalculator(TaxCalculators.CalculatorList.TaxJar).GetTaxRate(zip) })
            :
                Ok(new { results = "Invalid Zip Code" });
            
        }
        [HttpGet("GetTotalTax")]
        [Route("GetTotalTax")]
        public IActionResult GetTotalTax(string zip, double orderTotal)
        {
            return Utilities.IsUSZipCode(zip) ?
                Ok(new { results = _taxCalculator(TaxCalculators.CalculatorList.TaxJar).GetTotalTax(zip, orderTotal) })
                :            
                Ok(new { results = "Invalid Zip Code" });
            
        }
    }
}
