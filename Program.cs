using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using Etapa5.Entidades;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            //Eventos => Estos pueden ser agregados o elimnados dependiendo del caso
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(1200, 1000, 1);
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;

            var engine = new EscuelaEngine();
            MostrarMenuOpciones(ref engine);
            /*
            ImpimirCursosEscuela(engine.Escuela);
            var lista = engine.GetObjetoEscuelaBases();

            var dicObjetos = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dicObjetos, true);

            Printer.WriteTitle("Reportes...");
            var reporteador = new Reporteador(dicObjetos);
            var evaluaciones = reporteador.GetListaEvaluaciones();
            var asignaturas = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluacionesxAsignatura();
            var promedios = reporteador.GetPromediosAlumnosxAsignatura();
            var topPromedios = reporteador.GetTopPromedio(10);
            Printer.WriteTitle("Fin de reportes...");

            IngresarEvaluacion();
            */

            #region Comentarios
            /*
            Dictionary<int,string> diccionario= new Dictionary<int, string>();
            diccionario.Add(10,"Juanca"); //Equivalente: diccionario[23] = "Juanca;
            diccionario.Add(23,"Lorem Ipsum");
            foreach (var item in diccionario)
            {
                Console.WriteLine($"Key: {item.Key} , Valor: {item.Value} ");
            }

            Printer.WriteTitle("Acceso a Diccionario");
            diccionario[0] = "Pekerman";
            WriteLine(diccionario[0]);

            Printer.WriteTitle("Otro diccionario");
            var dic = new Dictionary<string,string>();
            dic["Luna"] = "Cuerpo celeste que gira al rededor de la tierra.";
            WriteLine(dic["Luna"]);
            dic["Luna"] = "Protagonista de Soy Luna.";
            WriteLine(dic["Luna"]);*/

            /*
            var listILugar = from obj in lista
                             where obj is ILugar
                             select (ILugar) obj; 
            */
            //engine.Escuela.LimpiarLugar();

            /*
            Printer.DrawLine(20);
            Printer.WriteTitle("PRUEBAS DE POLIMORFISMO");
            var alumnoTest = new Alumno{Nombre="Jhon Phileppe Reyes Flores"};
            
            ObjetoEscuelaBase ob = alumnoTest;
            
            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"Alumno: {alumnoTest.UniqueId}");
            WriteLine($"Alumno: {alumnoTest.GetType()}");

            Printer.WriteTitle("ObjetoEscuela 1");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

            var objDummy = new ObjetoEscuelaBase(){
                Nombre = "Javier Reyes"
            };

            Printer.WriteTitle("ObjetoEscuela 2");
            WriteLine($"Alumno: {objDummy.Nombre}");
            WriteLine($"Alumno: {objDummy.UniqueId}");
            WriteLine($"Alumno: {objDummy.GetType()}");   

            var evaluacion = new Evaluacion() {
                Nombre = "Evaluacion de mate!",
                Nota = 4.5f 
            };
            Printer.WriteTitle("Evaluacion");
            WriteLine($"Evaluacion: {evaluacion.Nombre}");
            WriteLine($"Evaluacion: {evaluacion.UniqueId}");
            WriteLine($"Evaluacion: {evaluacion.Nota}");
            WriteLine($"Evaluacion: {evaluacion.GetType()}");  

            //ob = evaluacion;
            Printer.WriteTitle("ObjetoEscuela 1");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

            
            if(ob is Alumno){
                Alumno alumnoRecuperado = (Alumno) ob;
            }

            Alumno alumnoRecuperado2 = ob as Alumno; 
            //Cuando utilizamos el [as] considera si es posible convertirlo a "Alumno" retorna el ojeto, caso contrario retorna null
            if(alumnoRecuperado2 != null){
                //logica para el caso..
            }
            */
            #endregion
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Printer.WriteTitle("Saliendo");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIo");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");

            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }

        private static void IngresarEvaluacion()
        {
            Printer.WriteTitle("Captura de una nueva Evaluacion por Consola");
            var newEval = new Evaluacion(); //Nombre y Nota
            string nombre, nota;

            WriteLine("Ingrese el nombre de la evaluacion:");
            Printer.PresioneEnter();
            nombre = ReadLine();

            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException("El valor del nombre no puede ser null");
            newEval.Nombre = nombre.ToLower();
            WriteLine("Nombre de la evaluacion ingresado correctamente!");

            WriteLine("Ingrese la nota de la evaluacion:");
            Printer.PresioneEnter();
            nota = ReadLine();

            if (string.IsNullOrWhiteSpace(nota)) throw new ArgumentNullException("El valor de la nota no puede ser null");
            if (!float.TryParse(nota, out float minota)) throw new ArgumentException("El valor de la nota debe ser un numero float");
            if (minota < 0 || minota > 5) throw new ArgumentOutOfRangeException("El valor de la nota esta fuera de rango");
            newEval.Nota = minota;
            WriteLine("Nota de la evaluacion ingresado correctamente!");
        }

        public static void MostrarMenuOpciones(ref EscuelaEngine escuela)
        {
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            string opcion = string.Empty; bool mostrarMenu = true;
            while (opcion != "9")
            {
                if (mostrarMenu)
                {
                    WriteLine("1. Generar datos");
                    WriteLine("2. Imprimir cursos escuela");
                    WriteLine("3. Imprimir diccionario escuela");
                    WriteLine("4. Ver reporte Evaluaciones");
                    WriteLine("5. Ver reporte Asignaturas");
                    WriteLine("6. Ver reporte Evaluaciones por Asignaturas");
                    WriteLine("7. Ver Promedios por Alumnos por Asignatura");
                    WriteLine("8. Ver {x} mejores promedios");
                    WriteLine("9. Salir del programa");

                    WriteLine("Ingrese una de las opciones mostradas y presione ENTER...");
                }
                opcion = ReadLine();
                EvaluarOpcionIngresada(opcion, ref escuela, out mostrarMenu);
            }
        }

        public static bool ValidarDatosGenerados(EscuelaEngine escuela, out bool mostrarmnu)
        {
            mostrarmnu = true;
            if (escuela?.Escuela is null)
            {
                mostrarmnu = false;
                WriteLine("Aun no existen datos generados en la escuela.");
                return false;
            }
            return true;
        }

        public static void EvaluarOpcionIngresada(string opcion, ref EscuelaEngine escuela, out bool mostrarmnu)
        {
            var dicObjetos = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();
            Reporteador reporteador;
            mostrarmnu = true;
            switch (opcion.ToUpper())
            {
                case "1": //Generar datos
                    WriteLine("\nGenerando informacion...");
                    escuela.Inicializar();
                    WriteLine("Informacion generada con exito\n");
                    break;

                case "2": //Imprimir cursos escuela
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    ImpimirCursosEscuela(escuela.Escuela);
                    WriteLine("Impresion generada con exito\n");
                    break;

                case "3": //Imprimir diccionario escuela
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    dicObjetos = escuela.GetDiccionarioObjetos();
                    escuela.ImprimirDiccionario(dicObjetos, true);
                    WriteLine("Impresion generada con exito\n");

                    break;

                case "4": //Ver reporte evaluaciones
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    dicObjetos = escuela.GetDiccionarioObjetos();
                    reporteador = new Reporteador(dicObjetos);
                    var evaluaciones = reporteador.GetListaEvaluaciones();
                    Printer.GenerateTable(new int[] { 35, 55, 15 }, new string[] { "Alumno", "Asignatura", "Nota" }, evaluaciones);

                    WriteLine("Impresion generada con exito\n");

                    break;

                case "5": //Ver reporte de asignaturas
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    dicObjetos = escuela.GetDiccionarioObjetos();
                    reporteador = new Reporteador(dicObjetos);
                    var asignaturas = reporteador.GetListaAsignaturas();
                    Printer.GenerateTable(new int[] { 25 }, new string[] { "Asignatura" }, asignaturas);

                    WriteLine("Impresion generada con exito\n");

                    break;

                case "6": //Ver reporte Evaluaciones por asignatura
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    dicObjetos = escuela.GetDiccionarioObjetos();
                    reporteador = new Reporteador(dicObjetos);
                    var listaEvalXAsig = reporteador.GetDicEvaluacionesxAsignatura();
                    Printer.GenerateTable(new int[] { 55, 15 }, new string[] { "Asignatura/Nombre", "Nota" }, listaEvalXAsig);

                    WriteLine("Impresion generada con exito\n");

                    break;

                case "7": //Ver promedios por alumno y asignatura
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;

                    WriteLine("\nImprimiendo informacion...");
                    dicObjetos = escuela.GetDiccionarioObjetos();
                    reporteador = new Reporteador(dicObjetos);
                    var promedios = reporteador.GetPromediosAlumnosxAsignatura();
                    Printer.GenerateTable(new int[] { 55, 15 }, new string[] { "Alumno", "Promedio" }, promedios);

                    WriteLine("Impresion generada con exito\n");

                    break;

                case "8": //Ver {x} mejores proyectos
                    if (!ValidarDatosGenerados(escuela, out mostrarmnu)) return;


                    WriteLine("\nIngreses la cantidad de promedios a mostrar...");
                    string cantidad = ReadLine();
                    if (!int.TryParse(cantidad, out int top)) new Exception("Error al digitar la cantida...");

                    dicObjetos = escuela.GetDiccionarioObjetos();
                    reporteador = new Reporteador(dicObjetos);

                    var topPromedios = reporteador.GetTopPromedio(top);
                    Printer.GenerateTable(new int[] { 25, 5, 55, 15 }, new string[] { "Asignatura", "Item", "Alumno", "Promedio" }, topPromedios);

                    WriteLine("Impresion generada con exito\n");

                    break;

                case "9":
                    mostrarmnu = false;
                    break;

                default:
                    mostrarmnu = false;
                    WriteLine("La opcion ingresada no es valida. Por favor reintente ingresando el numero correcto.");
                    break;
            }
        }
    }
}
