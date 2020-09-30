using System;
using System.Collections.Generic;
using System.Linq;
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
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad:10);
            ImpimirCursosEscuela(engine.Escuela);  
            
            var lista = engine.GetObjetoEscuelaBases(out int conteoEvaluaciones);
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
