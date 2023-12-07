using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebMyper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebMyper.Models;
using Microsoft.AspNetCore.Mvc;
using PruebMyper.Models.ViewModels;

namespace PruebMyper.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public HomeController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          
          List<Trabajadore> lista = _context.Trabajadores
             .Include(t => t.oDepartamento)
             .Include(t => t.oProvincia)
             .Include(t => t.oDistrito)
             .ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Trabajador_Detalle(int idTrabajador)
        {
            TrabajadorVM otrabajadorVM = new TrabajadorVM()
            {
                oTrabajador = new Trabajadore(),
                oListaDepartamento = _context.Departamentos.Select(departamento => new SelectListItem()
                {
                    Text = departamento.NombreDepartamento,
                    Value = departamento.Id.ToString()
                }).ToList(),


                oListaProvincia = _context.Provincia.Select(provincia => new SelectListItem()
                {
                    Text = provincia.NombreProvincia,
                    Value = provincia.Id.ToString()
                }).ToList(),

                oListaDistrito = _context.Distritos.Select(distrito => new SelectListItem()
                {
                    Text = distrito.NombreDistrito,
                    Value = distrito.Id.ToString()
                }).ToList()

            };
         
            if(idTrabajador != 0)
            {
                otrabajadorVM.oTrabajador = _context.Trabajadores.Find(idTrabajador);
            }
         
            return View(otrabajadorVM);

        }

        [HttpGet]
        public ActionResult CargarProvincias(int idDepartamento)
        {
            if (idDepartamento > 0)
            {
                List<SelectListItem> provincia = _context.Provincia.Where(p => p.IdDepartamento == idDepartamento)
                .Select(provincia => new SelectListItem()
                {
                    Text = provincia.NombreProvincia,
                    Value = provincia.Id.ToString()
                }).ToList();

                return Json(provincia);
            }
            return null;
        }

        [HttpGet]
        public ActionResult CargarDistritos(int idProvincia)
        {
          
            if (idProvincia > 0)
            {
                List<SelectListItem> distrito = _context.Distritos.Where(p => p.IdProvincia == idProvincia)
                .Select(distrito => new SelectListItem()
                {
                    Text = distrito.NombreDistrito,
                    Value = distrito.Id.ToString()
                }).ToList();

                return Json(distrito);
            }
            return null;
        }

        [HttpPost]
        public IActionResult Trabajador_Detalle(TrabajadorVM oTrabajadorVM)
        {
            try
            {
                // Verifica si el trabajador ya existe
                if (oTrabajadorVM.oTrabajador.Id == 0)
                {
                 
                    var departamento = _context.Departamentos.Find(oTrabajadorVM.oTrabajador.IdDepartamento);
                    if (departamento != null)
                    {
                        oTrabajadorVM.oTrabajador.oDepartamento = departamento;
                    }

              
                    var provincia = _context.Provincia.Find(oTrabajadorVM.oTrabajador.IdProvincia);
                    if (provincia != null)
                    {
                        oTrabajadorVM.oTrabajador.oProvincia = provincia;
                    }

          
                    var distrito = _context.Distritos.Find(oTrabajadorVM.oTrabajador.IdDistrito);
                    if (distrito != null)
                    {
                        oTrabajadorVM.oTrabajador.oDistrito = distrito;
                    }

                    _context.Trabajadores.Add(oTrabajadorVM.oTrabajador);
                  
                }
                else
                {
                    _context.Trabajadores.Update(oTrabajadorVM.oTrabajador);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
                return View(oTrabajadorVM);
            }

        }


        [HttpGet]
        public IActionResult Eliminar(int idTrabajador)
        {
            // Busca al trabajador por Id y carga sus datos
            Trabajadore trabajador = _context.Trabajadores
            .Include(t => t.oDepartamento)
            .Include(t => t.oProvincia)
            .Include(t => t.oDistrito)
            .FirstOrDefault(t => t.Id == idTrabajador);

            //verifica si se encuentra
             if (trabajador == null)
             {
                return NotFound();
             }

            return View(trabajador);
 
        }

        [HttpPost]
        public IActionResult Eliminar(Trabajadore oTrabajador)
        {
            
            _context.Trabajadores.Remove(oTrabajador);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
         
        }
    }
}
