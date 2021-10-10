using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurantesiberian.Models
{
    public partial class Restaurante
    {
       [Key]
        public int Idrestaurante { get; set; }
        public string NombreRestaurante { get; set; }
        public int? Idciudad { get; set; }
        public int? NumeroAforo { get; set; }
        public string Telefono { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual Ciudad IdciudadNavigation { get; set; }
    }
}
