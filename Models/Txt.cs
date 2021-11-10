using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Formulario_de_Registro.Models
{
    public class Txt: IDataManager
    {
        // IMPLEMENTACION DE PATRON SINGLETON

        private static Txt archivoTxt;
        private List<Estudiante> estudiantes;
        private readonly string ruta = "wwwroot/Estudiantes.txt";
        private Txt()
        {}
        public static Txt GetArchivoTXT()
        {
            if (archivoTxt == null)
            {
                archivoTxt = new Txt();
            }
            return archivoTxt;
        }

        public bool AlmacenarDatos(Estudiante estudiante)
        {
            VerificarArchivo();

         try
            {
                StreamWriter archivoW = File.AppendText(ruta);

                archivoW.WriteLine($"Matricula: {estudiante.Matricula}");
                archivoW.WriteLine($"Nombres: {estudiante.Nombre}");
                archivoW.WriteLine($"Apellidos: {estudiante.Apellidos}");
                archivoW.WriteLine($"Fecha de nacimiento: {estudiante.FechaNacimiento}");
                archivoW.WriteLine($"Carrera: {estudiante.Carrera}");
                archivoW.WriteLine($"Direccion: {estudiante.Direccion}");
                archivoW.WriteLine($"Telefono: {estudiante.Telefono}");
                archivoW.WriteLine($"Correo Electronico: {estudiante.EMail}");
                archivoW.WriteLine("");
                archivoW.Close();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        private void VerificarArchivo()
        {
            // COMPRUEBA SI EXISTE UN ARCHIVO CON LA RUTA. SI NO EXISTE LO CREA

            if (File.Exists(ruta) == false)
            {
                File.Create(ruta).Close();
            }
        }

        public List<Estudiante> BuscarDatos()
        {
            // PARA OBTENER LOS ESTUDIANTES ALMACENADOS EN EL TXT RECORREMOS EL TXT LINEA POR LINEA.
            // POR CADA LINEA VAMOS SEPARANDO LOS DATOS, ES DECIR, EL TITULO Y EL VALOR,
            // pe:Nombre : Jose. SEPARAMOS LOS DATOS DE CADA LINEA CON UN SPLIT, TOMANDO COMO SEPARADOR
            // LOS 2 PUNTOS. CUANDO LLEGAMOS A UNA LINEA VACIA, SIGNIFICA QUE YA RECORRIMOS TODOS LOS DATOS
            // NECESARIOS PARA CREAR UN OBJETO ESTUDIANTES. SOLO SE REALIZA SI EXISTE EL ARCHIVO.
                
            int contador = 0;
            estudiantes = new List<Estudiante>();

            if (File.Exists(ruta))
            {
                try
                {
                    VerificarArchivo();
                    TextReader lectorEstudiantes = new StreamReader(ruta);
                    string[] datos = new string[2]; 
                    List<string>valores = new List<string>();
                
                    while (true)
                    {
                        string lineaActual = lectorEstudiantes.ReadLine();

                        if (lineaActual == null)
                        {
                            // AL LLEGAR AQUI QUIERE DECIR QUE NO HAY MAS LINEAS Y QUE TERMINO EL RECORRIDO.
                            break;
                        }

                        else if (lineaActual == "" && contador == 8)
                        {
                            estudiantes.Add(

                              new Estudiante()
                              {
                                Matricula = valores[0],
                                Nombre = valores[1],
                                Apellidos = valores[2],
                                FechaNacimiento = valores[3],
                                Carrera = valores[4],
                                Direccion = valores[5],
                                Telefono = valores[6],
                                EMail = valores[7]
                                }
                            );
                            valores.Clear();
                            contador = 0;
                        }
                        else if(lineaActual != "")
                        {
                            datos = lineaActual.Split(":");
                            valores.Add(datos[1]);
                            contador++;
                        }  
                    }
                    lectorEstudiantes.Close();
                }
                catch(Exception)
                {
                    throw new Exception("Hubo errores al obtener los datos del txt");
                }
            }
            return estudiantes;
        }
    }
}