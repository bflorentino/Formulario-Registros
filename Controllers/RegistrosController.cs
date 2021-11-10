using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulario_de_Registro.Models;

namespace Formulario_de_Registro.Controllers
{
    public class RegistrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Formulario()
        {
            return View();
        }

        public IActionResult DatosGuardados()
        {
            Estudiante estudiante = new Estudiante
            {
                Nombre = Request.Form["nombre"],
                Matricula = Request.Form["matricula"],
                Apellidos = Request.Form["apellidos"],
                FechaNacimiento = DateTime.Parse(Request.Form["fechaNacimiento"]).ToString("dd/MM/yyyy"),
                Carrera = Request.Form["carrera"],
                Direccion = Request.Form["direccion"],
                Telefono = Request.Form["telefono"],
                EMail = Request.Form["correo"]
            };

            // ESTRATEGIA PARA REGISTRO DE LOS DATOS EN EL ARCHIVO CORRESPONDIENTE

            IDataManager formato = FormatoArchivo.GetFormatoArchivo(Request.Form["formato"]);
            ContextoDatos almacenamiento = new ContextoDatos(formato);
            almacenamiento.GuardarDatos(estudiante);

            return View();
        }
        
        public IActionResult MostrarDatosDeTXT()
        {
            var estudiantes = from est in GetEstudiantes("TXT")
                              select est;

            return View(estudiantes);
        }
        public IActionResult MostrarDatosDeJSON()
        {
            var estudiantes = from est in GetEstudiantes("JSON")
                              select est;

            return View(estudiantes);
        }
        public IActionResult MostrarDatosDeEXCEL()
        {
            var estudiantes = from est in GetEstudiantes("EXCEL")
                              select est;

            return View(estudiantes);
        }

        [NonAction]
        public List<Estudiante> GetEstudiantes(string formato)
        {
            // ESTRATEGIA PARA BUSQUEDA DE DATOS EN EL ARCHIVO CORRESPONDIENTE

            IDataManager formatoArchivo = FormatoArchivo.GetFormatoArchivo(formato);
            ContextoDatos contextoBusqueda = new ContextoDatos(formatoArchivo);
            return contextoBusqueda.BuscarDatos();
        }
    }
}