using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario_de_Registro.Models
{
    public class Estudiante
    {
        public string Matricula { get; set;}
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public string Carrera { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string EMail { get; set; }
    }
}