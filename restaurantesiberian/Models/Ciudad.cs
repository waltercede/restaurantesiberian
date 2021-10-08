using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurantesiberian.Models
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Restaurantes = new HashSet<Restaurante>();
        }

        [Key]
        public int Idciudad { get; set; }
        public string NombreCiudad { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Restaurante> Restaurantes { get; set; }
    }
}
