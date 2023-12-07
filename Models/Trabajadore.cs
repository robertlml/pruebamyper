using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PruebMyper.Models
{
    public partial class Trabajadore
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public string TipoDocumento { get; set; }
        [Required(ErrorMessage = "El tipo de numero de documento es obligatorio.")]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Debe elegir una opcion es obligatorio.")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "Debe elegir una opcion es obligatorio.")]
        public int? IdDepartamento { get; set; }
        [Required(ErrorMessage = "Debe elegir una opcion es obligatorio.")]
        public int? IdProvincia { get; set; }
        [Required(ErrorMessage = "Debe elegir una opcion es obligatorio.")]
        public int? IdDistrito { get; set; }

        public virtual Departamento oDepartamento { get; set; }
        public virtual Distrito oDistrito { get; set; }
        public virtual Provincium oProvincia { get; set; }
    }
}
