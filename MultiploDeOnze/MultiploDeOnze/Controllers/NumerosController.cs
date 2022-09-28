using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiploDeOnze.Data;
using MultiploDeOnze.Models;

namespace MultiploDeOnze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumerosController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly ApiContext _context;

        public NumerosController(ApiContext context)
        {
            _context = context;
        }
        // Create
        [HttpPost]
        public JsonResult CreateEdit(int[] numeros)
        {
            List<Numeros> list = new List<Numeros>();
            foreach (int num in numeros)
            {
                Numeros Num = new Numeros(num, num % 11 == 0 ? true : false);
                list.Add(Num);
            }
            return new JsonResult(list);
        }
    }
}
