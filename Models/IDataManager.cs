using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario_de_Registro.Models
{
    public interface IDataManager
    {
        // INTERFACE QUE PERMITE IMPLEMENTAR LA ESTRATEGIA DE ALMACENAMIENTO Y OBTENCION DE DATOS EN DISTINTOS FORMATOS.
        bool AlmacenarDatos(Estudiante estudiante);
        List<Estudiante> BuscarDatos();
    }
}
