using System;
using System.Collections.Generic;
using Etapa5.Entidades;

namespace CoreEscuela.Entidades
{
    public class Alumno : ObjetoEscuelaBase
    { 
        public List<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>(); 
    }
}