using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace frontrestaurante.Data
{
    public class RestauranteModels
    {
        public int Idrestaurante { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Agrega iformacion valida", MinimumLength = 1)]
        public string NombreRestaurante { get; set; }
        public int? Idciudad { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Debe estar entre 1 y 10000.")]
        public int? NumeroAforo { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Digite un numero de telefono valido.", MinimumLength = 10)]
        public string Telefono { get; set; }
        public DateTime? FechaCreacion { get; set; }

    }
}
