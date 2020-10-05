using System.Collections.Generic;
using Etapa5.Entidades;
using System;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public sealed class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicOb)
        {
            if (dicOb is null) throw new ArgumentNullException(nameof(dicOb));

            _diccionario = dicOb;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            //GetValueOrDefault => Retorna el valor existente y en caso de no localizar la llave retornará un valor por defecto dependiendo del tipo de dato. 
            //List => Null | String => "" | Integer => 0
            //var lista = _diccionario.GetValueOrDefault(LlaveDiccionario.Escuela);

            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dumy);
        }
        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluacion)
        {
            listaEvaluacion = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluacion
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvaluacionesxAsignatura()
        {
            Dictionary<string, IEnumerable<Evaluacion>> dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsig = GetListaAsignaturas(out var listEval);
            foreach (var asig in listaAsig)
            {
                var evalsAsig = from Evaluacion ev in listEval
                                where ev.Asignatura.Nombre == asig
                                select ev;

                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromediosAlumnosxAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var listaEvalxAsig = GetDicEvaluacionesxAsignatura();
            foreach (var item in listaEvalxAsig)
            {
                var promAlumno = from eval in item.Value
                                 group eval by new
                                 {
                                     eval.Alumno.UniqueId,
                                     eval.Alumno.Nombre
                                 }
                                 into grupoEvalsAlumno
                                 select new AlumnoPromedio
                                 {
                                     AlumnoId = grupoEvalsAlumno.Key.UniqueId,
                                     AlumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                     Promedio = grupoEvalsAlumno.Average((x) => x.Nota)
                                 };

                rta.Add(item.Key, promAlumno);
            }
            return rta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetTopPromedio(int top)
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var listEval = GetPromediosAlumnosxAsignatura();
            foreach (var item in listEval)
            {
                var topAlumno = (from AlumnoPromedio eval in item.Value
                                 orderby eval.Promedio descending
                                 select eval).Take(top);

                rta.Add(item.Key, topAlumno);
            }

            return rta;
        }
    }
}