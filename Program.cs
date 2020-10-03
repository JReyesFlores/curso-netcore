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
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o,s) => Printer.Beep(1200,1000,1);
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad:10);
            ImpimirCursosEscuela(engine.Escuela);  
            var lista = engine.GetObjetoEscuelaBases(); 
           
            var dic2 = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dic2,true);

            var reporteador = new Reporteador(dic2);
            var evaluaciones = reporteador.GetListaEvaluaciones();
            var asignaturas = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluacionesxAsignatura();
            var promedios = reporteador.GetPromediosAlumnosxAsignatura();

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
                Nombre = "Evaluación de mate!",
                Nota = 4.5f 
            };
            Printer.WriteTitle("Evaluación");
            WriteLine($"Evaluación: {evaluacion.Nombre}");
            WriteLine($"Evaluación: {evaluacion.UniqueId}");
            WriteLine($"Evaluación: {evaluacion.Nota}");
            WriteLine($"Evaluación: {evaluacion.GetType()}");  

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
                //lógica para el caso..
            }
            */
            #endregion
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Printer.WriteTitle("Saliendo");
            Printer.Beep(3000,1000,3);
            Printer.WriteTitle("SALIÓ");
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
    }
}
