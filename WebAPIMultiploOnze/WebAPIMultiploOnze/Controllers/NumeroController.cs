using Microsoft.AspNetCore.Mvc;
using WebAPIMultiploOnze.Models;
using WebAPIMultiploOnze.Business;

namespace WebAPIMultiploOnze.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumeroController : ControllerBase
    {


        private readonly ILogger<NumeroController> _logger;

        public NumeroController(ILogger<NumeroController> logger)
        {
            _logger = logger;
        }

        [HttpPost("VerifyMultiploOnze")]
        public IActionResult GetNumbers(NumerosRawData data)
        {
            int isNumber;
            List<NumerosProcessados> numerosProcessados = new List<NumerosProcessados>();
            foreach (string number in data.numbers)
            {
                if (!int.TryParse(number, out isNumber))
                {
                    numerosProcessados.Add(new NumerosProcessados
                    {
                        Number = number + " Not a Number",
                        IsMultiple = false
                    });
                }
                else
                {
                    numerosProcessados.Add(new NumerosProcessados
                    {
                        Number = number,
                        IsMultiple = BusinessLogic.isMultipleOfEleven(number)
                    });
                }

            }

            return Ok(numerosProcessados);

        }

    }
}