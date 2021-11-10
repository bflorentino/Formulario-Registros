using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario_de_Registro.Models
{
    public class FormatoArchivo
    {
        // PATRON DE DISEÑO FACTORY. TODOS LAS CLASES SON SINGLETON, POR LO CUAL SOLO ACCEDEMOS A LA
        // INSTANCIA QUE NOS DEVUELVE LA CLASE
        public static IDataManager GetFormatoArchivo(string formato)
        {
            switch (formato)
            {
                case "TXT": return Txt.GetArchivoTXT();
                case "JSON": return Json.GetArchivoJson();
                case "EXCEL": return Excel.GetArchivoExcel();
                default: return null;
            }
        }
    }
}
