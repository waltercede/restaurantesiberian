using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontrestaurante.Data
{
    public class RespuestaModels
    {
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public List<RestauranteModels> DatosJson { get; set; }
    }

    public class RespuestaCiudadModels
    {
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public List<CiudadModels> DatosJson { get; set; }
    }
}
