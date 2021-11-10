using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario_de_Registro.Models
{
    // IMPLEMENTACION DE PATRON DE DISEÑO STRATEGY
    public class ContextoDatos
    {
        private readonly IDataManager formatoArchivo;

        public ContextoDatos(IDataManager formatoArchivo)
        {
            this.formatoArchivo = formatoArchivo;
        }

        public void GuardarDatos(Estudiante estudiante)
        {
            formatoArchivo.AlmacenarDatos(estudiante);
        }

        public List<Estudiante> BuscarDatos()
        {
            return formatoArchivo.BuscarDatos();
        }
    }
}
