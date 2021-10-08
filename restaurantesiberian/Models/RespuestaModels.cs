using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurantesiberian.Models
{
    public class RespuestaModels
    {
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public object DatosJson { get; set; }
    }
}
