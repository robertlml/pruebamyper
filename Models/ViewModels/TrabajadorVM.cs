using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebMyper.Models.ViewModels
{
    public class TrabajadorVM
    {
        public Trabajadore oTrabajador { get; set; }
        public List<SelectListItem> oListaDepartamento { get; set; }

        public List<SelectListItem> oListaProvincia { get; set; }
        public List<SelectListItem> oListaDistrito { get; set; }
    }
}
