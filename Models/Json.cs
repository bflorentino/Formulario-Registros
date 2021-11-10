using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Formulario_de_Registro.Models
{
    public class Json : IDataManager
    {
        // PATRON DE DISEÑO SINGLETON       
        private static Json archivoJson;
        private string ruta = "wwwroot/Estudiantes.Json";
        private List<Estudiante> estudiantes = new List<Estudiante>();
        private Json()
        { }
        public static Json GetArchivoJson()
        {
            if (archivoJson == null)
            {
                archivoJson = new Json();
            }
            return archivoJson;
        }

        public bool AlmacenarDatos(Estudiante estudiante)
        {
           // OBTENEMOS LA LISTA EXISTENTE DE ESTUDIANTES EN EL JSON, LUEGO EL ESTUDIANTE NUEVO
           // LO AGREGAMOS A LA LISTA EN LA ULTIMA POSICION Y NUEVAMENTE CREAMOS UN JSON A PARTIR DE LA
           // LISTA (SOBREESCRIBIENDO EL QUE EXISTIA), PERMITIENDO ASI PERSISTIR A
           // LOS ESTUDIANTES QUE YA ESTABAN Y AGREGAR NUEVOS.

            if(estudiantes.Count == 0)
            {
                LlenarListaEstudiantes(); // Esto solo se ejecuta la primera vez que se invoca al metodo en tiempo de ejecucion
            }

            estudiantes.Add(estudiante);
            string valores = JsonConvert.SerializeObject(estudiantes.ToArray(), Formatting.Indented);
            VerificarArchivo();
            File.WriteAllText(ruta, valores);
            return true;
        }

        private List<Estudiante> LlenarListaEstudiantes()
        {
            // OBTENER LISTA DE ESTUDIANTES DESDE EL JSON SI EL ARCHIVO EXISTE
            string informacion;

            if (File.Exists(ruta))
            {
                var lectorJSON = new StreamReader(ruta);
                informacion = lectorJSON.ReadToEnd();
                lectorJSON.Close();
                estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(informacion);
            
                if(estudiantes == null)
                {
                    estudiantes = new List<Estudiante>();
                }
            }

            return estudiantes;
        }
        private void VerificarArchivo()
        {
            //COMPRUEBA SI EXISTE UN ARCHIVO CON EL SIGUIENTE NOMBRE SI NO EXISTE, LO CREA 

            if (File.Exists(ruta) == false)
            {
                File.Create(ruta).Close();
            }
        }

        public List<Estudiante> BuscarDatos()
        {
            return estudiantes.Count == 0 ? LlenarListaEstudiantes() : estudiantes;
        }
    }
}