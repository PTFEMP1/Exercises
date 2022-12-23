using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiploDeOnze.Models
{
    public class Numeros
    {
        public int Numero { get; set; }
        public bool MultiploOnze { get; set; }

        public Numeros(int numero, bool multiploOnze)
        {
            Numero = numero;
            MultiploOnze = multiploOnze;
        }
    }
}
