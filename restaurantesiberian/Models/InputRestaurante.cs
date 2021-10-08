using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurantesiberian.Models
{
    public class InputRestaurante
    {
        public int? Idrestaurante { get; set; }
        public string NombreRestaurante { get; set; } 
        public int? Idciudad { get; set; }
        public int? NumeroAforo { get; set; }
        public string Telefono { get; set; }
    }


    public class InputCiudad
    {
        public int Idciudad { get; set; }
        public string NombreCiudad { get; set; }
    }

}
