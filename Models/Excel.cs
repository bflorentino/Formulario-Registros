using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpreadsheetLight;
using System.Text;
using System.IO;

namespace Formulario_de_Registro.Models
{
    public class Excel : IDataManager
    { 
        // PATRON DE DISEÑO SINGLETON  

        private static Excel archivoExcel;
        private readonly string ruta = "wwwroot/Estudiantes.xlsx";
        private List<Estudiante> estudiantes = new List<Estudiante>();
        private Excel()
        { }
        public static Excel GetArchivoExcel()
        {
            if (archivoExcel == null)
            {
                archivoExcel = new Excel();
            }
            return archivoExcel;
        }

        public bool AlmacenarDatos(Estudiante estudiante)
        {
            // OBTENEMOS LA LISTA EXISTENTE DE ESTUDIANTES EN EL EXCEL, LUEGO EL ESTUDIANTE NUEVO
            // LO AGREGAMOS A LA LISTA EN LA ULTIMA POSICION Y NUEVAMENTE CREAMOS EL EXCEL A PARTIR DE LA
            // LISTA (SOBREESCRIBIENDO EL QUE EXISTIA), PERMITIENDO ASI PERSISTIR A
            // LOS ESTUDIANTES QUE YA ESTABAN Y AGREGAR NUEVOS.

            try
            {
                if (estudiantes.Count == 0)
                {
                    LlenarListaEstudiantes(); // Solo se ejecuta la primera vez que se invoca al metodo en tiempo de ejecucion
                }
                estudiantes.Add(estudiante);
                SLDocument sLDocument = new SLDocument();
                SetColumnsValues(sLDocument);
                SetColumnsStyle(sLDocument);
                sLDocument.SaveAs(ruta);
                //int row = 2; // Fila a partir de donde se van agregar los estudiantes
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void SetColumnsValues(SLDocument sLDocument)
        {
            // Estos son los encabezados de cada columna de datos
            sLDocument.SetCellValue("A1", "Matricula");
            sLDocument.SetCellValue("B1", "Nombre");
            sLDocument.SetCellValue("C1", "Apellidos");
            sLDocument.SetCellValue("D1", "Fecha de Nacimiento");
            sLDocument.SetCellValue("E1", "Carrera");
            sLDocument.SetCellValue("F1", "Direccion");
            sLDocument.SetCellValue("G1", "Telefono");
            sLDocument.SetCellValue("H1", "Correo Electronico");

            int row = 2; // Fila a partir de donde se van agregar los estudiantes

            // CON ESTE FOREACH ESTAMOS RECORRIENDO LA LISTA DE ESTUDIANTES QUE TENEMOS PARA 
            // ALMACENARLOS EN EL ARCHIVO EXCEL
            foreach (Estudiante nestudiante in estudiantes)
            {
                sLDocument.SetCellValue("A" + row, nestudiante.Matricula);
                sLDocument.SetCellValue("B" + row, nestudiante.Nombre);
                sLDocument.SetCellValue("C" + row, nestudiante.Apellidos);
                sLDocument.SetCellValue("D" + row, nestudiante.FechaNacimiento);
                sLDocument.SetCellValue("E" + row, nestudiante.Carrera);
                sLDocument.SetCellValue("F" + row, nestudiante.Direccion);
                sLDocument.SetCellValue("G" + row, nestudiante.Telefono);
                sLDocument.SetCellValue("H" + row, nestudiante.EMail);
                row++;
            }
        }

        private void SetColumnsStyle(SLDocument sLDocument)
        {
            // Ponemos el estilo de letra negrita para los encabezados
            SLStyle estilo = new SLStyle();
            estilo.SetFontBold(true);
            sLDocument.SetCellStyle("A1", "H1", estilo);

            sLDocument.AutoFitColumn("A", "H");
        }
        private List<Estudiante> LlenarListaEstudiantes()
        {
            // Obtenemos los datos registrados en todas las filas del archivo de excel, en forma de lista
            // de tipo estudiante, siempre y cuando exista el archivo
            if (File.Exists(ruta))
            {
                SLDocument sLDocument = new SLDocument(ruta);
                int row = 2;
                while(!string.IsNullOrEmpty(sLDocument.GetCellValueAsString(row, 1)))
                {
                    estudiantes.Add(
                        new Estudiante()
                        {
                            Matricula = sLDocument.GetCellValueAsString(row, 1),
                            Nombre = sLDocument.GetCellValueAsString(row, 2),
                            Apellidos = sLDocument.GetCellValueAsString(row, 3),
                            FechaNacimiento = sLDocument.GetCellValueAsString(row, 4),
                            Carrera = sLDocument.GetCellValueAsString(row, 5),
                            Direccion = sLDocument.GetCellValueAsString(row, 6),
                            Telefono = sLDocument.GetCellValueAsString(row, 7),
                            EMail = sLDocument.GetCellValueAsString(row, 8)
                        });
                    row++;
                }
            }
            return estudiantes;
        }
        public List<Estudiante> BuscarDatos()
        {
            try
            {
                return estudiantes.Count == 0 ? LlenarListaEstudiantes() : estudiantes;
            }
            catch
            {
                throw new Exception("Hubo errores al momento de obtener los datos en EXCEL");
            }
        }
    }
}