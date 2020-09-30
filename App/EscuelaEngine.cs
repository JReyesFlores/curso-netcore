using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using Etapa5.Entidades;

namespace CoreEscuela
{
    //SEALED => Esta clase puede ser instanciada y crear objetos, pero no puede ser heredada
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        #region Métodos de carga
        private void CargarEvaluaciones()
        {
            var listCursos = this.Escuela.Cursos.ToList();
            foreach (var cur in listCursos) {
                foreach(var asi in cur.Asignaturas) {
                    foreach (var alu in cur.Alumnos) { 
                        var listEval = new List<Evaluacion>();

                        //5 evaluaciones por asignatura
                        for (int i = 1; i <= 5; i++) {  
                            var rnd = new Random();
                            var nuevaEval = new Evaluacion();
                            nuevaEval.Alumno = alu;
                            nuevaEval.Asignatura = asi;
                            nuevaEval.Nombre = $"Evaluacion #{i}";
                            nuevaEval.Nota = (float) (5 * rnd.NextDouble());
                            listEval.Add(nuevaEval);
                        }

                        alu.Evaluaciones.AddRange(listEval);
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar( int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos =  from n1 in nombre1
                                from n2 in nombre2
                                from a1 in apellido1
                                select new Alumno{ Nombre=$"{n1} {n2} {a1}" };
            
            return listaAlumnos.OrderBy( (al)=> al.UniqueId ).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() { Nombre = "201", Jornada = TiposJornada.Mañana },
                        new Curso() { Nombre = "301", Jornada = TiposJornada.Mañana },
                        new Curso() { Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde },
            };
            
            Random rnd = new Random();
            foreach(var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        
        }

        /*
        * Sobrecargar del método
        */
        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases(
                        bool traeCursos = true,
                        bool traeAsignaturas = true,
                        bool traeAlumnos = true, 
                        bool traeEvaluaciones = true){
            return GetObjetoEscuelaBases(out int dummy,out dummy, out dummy, out dummy);
        }

        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases(
                        out int conteoEvaluaciones,
                        bool traeCursos = true,
                        bool traeAsignaturas = true,
                        bool traeAlumnos = true, 
                        bool traeEvaluaciones = true){
            return GetObjetoEscuelaBases(out conteoEvaluaciones,out int dummy, out dummy, out dummy);
        }

        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases(
                        out int conteoEvaluaciones,
                        out int conteoCursos,
                        bool traeCursos = true,
                        bool traeAsignaturas = true,
                        bool traeAlumnos = true, 
                        bool traeEvaluaciones = true){
            return GetObjetoEscuelaBases(out conteoEvaluaciones,out conteoCursos, out int dummy, out dummy);
        }

        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases(
                        out int conteoEvaluaciones,
                        out int conteoCursos,
                        out int conteoAsignaturas,
                        bool traeCursos = true,
                        bool traeAsignaturas = true,
                        bool traeAlumnos = true, 
                        bool traeEvaluaciones = true){
            return GetObjetoEscuelaBases(out conteoEvaluaciones,out conteoCursos, out conteoAsignaturas, out int dummy);
        }

        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases(
                        out int conteoEvaluaciones,
                        out int conteoCursos,
                        out int conteoAsignaturas,
                        out int conteoAlumnos,
                        bool traeCursos = true,
                        bool traeAsignaturas = true,
                        bool traeAlumnos = true, 
                        bool traeEvaluaciones = true) 
        {
            conteoEvaluaciones = 0;
            conteoCursos = 0;
            conteoAsignaturas = 0;
            conteoAlumnos = 0;
            
            var listObj = new List<ObjetoEscuelaBase>();
            listObj.Add(Escuela);
 
            listObj.AddRange(Escuela.Cursos);
            conteoCursos = Escuela.Cursos.Count;
            foreach (var item in Escuela.Cursos)
            {
                if(traeAsignaturas) listObj.AddRange(item.Asignaturas);   

                conteoAsignaturas += item.Asignaturas.Count;
                if(traeAlumnos) listObj.AddRange(item.Alumnos);

                conteoAlumnos += item.Alumnos.Count;
                if(traeEvaluaciones){
                    foreach (var item2 in item.Alumnos)
                    {
                        listObj.AddRange(item2.Evaluaciones);
                        conteoEvaluaciones += item2.Evaluaciones.Count;
                    }
                } 
            }

            return listObj;
        }

        #endregion
    }
}