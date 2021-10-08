using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontrestaurante.Data
{
    public class RestauranteModels
    {
        public int Idrestaurante { get; set; }
        public string NombreRestaurante { get; set; }
        public int? Idciudad { get; set; }
        public int? NumeroAforo { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaCreacion { get; set; }

    }
}
