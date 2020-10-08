using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using Etapa5.Entidades;
using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }

        public static void PresioneEnter()
        {
            WriteLine("Presione ENTER para continuar...");
        }

        public static void WriteTitle(string titulo)
        {
            var tamaño = titulo.Length + 4;
            DrawLine(tamaño);
            WriteLine($"| {titulo} |");
            DrawLine(tamaño);
        }

        public static void Beep(int hz = 2000, int tiempo = 500, int cantidad = 1)
        {
            while (cantidad-- > 0)
            {
                Beep(hz, tiempo);
            }
        }

        public static void GenerateTable(int[] spaceWithColumns, string[] columnsName)
        {
            //Imprimiendo las cabeceras
            if (spaceWithColumns?.Count() == 0) return;
            string linea = "|" + string.Concat(Enumerable.Repeat("*", spaceWithColumns.Sum() + (columnsName.Length - 1))) + "|";
            WriteLine($"{linea}");

            string title = "|"; int index = 0;
            foreach (var item in columnsName)
            {
                var name = item.Trim();
                if (spaceWithColumns[index] < name.Length) throw new System.Exception($"La longitud de la columna {item} excede en su tamaño.");
                var newtitulo = name.PadRight(spaceWithColumns[index]) + "|";

                title += newtitulo;
                index += 1;
            }

            WriteLine($"{title}");
            WriteLine($"{linea}");
        }

        public static void GenerateTable(int[] spaceWithColumns, string[] columnsName, IEnumerable<ObjetoEscuelaBase> data)
        {
            //Imprimiendo cabeceras
            GenerateTable(spaceWithColumns, columnsName);
            string linea = "|" + string.Concat(Enumerable.Repeat("*", spaceWithColumns.Sum() + (columnsName.Length - 1))) + "|";

            //Imprimiendo los datos
            foreach (var item in data)
            {
                var dataLine = "";
                switch (item.GetType().Name.ToUpper())
                {
                    case "EVALUACION":
                        var eval = item as Evaluacion;
                        dataLine = "|" + eval.Alumno.Nombre.PadRight(spaceWithColumns[0]) + "|" + eval.Asignatura.Nombre.PadRight(spaceWithColumns[1]) + "|" + eval.Nota.ToString().PadRight(spaceWithColumns[2]) + "|";
                        break;
                    default:
                        break;
                }
                WriteLine($"{dataLine}");
            }
            WriteLine($"{linea}");
        }

        public static void GenerateTable(int[] spaceWithColumns, string[] columnsName, IEnumerable<string> data)
        {
            //Imprimiendo cabeceras
            GenerateTable(spaceWithColumns, columnsName);
            string linea = "|" + string.Concat(Enumerable.Repeat("*", spaceWithColumns.Sum() + (columnsName.Length - 1))) + "|";

            //Imprimiendo los datos
            foreach (var item in data)
            {
                var dataLine = "|" + item.PadRight(spaceWithColumns[0]) + "|";

                WriteLine($"{dataLine}");
            }
            WriteLine($"{linea}");
        }

        public static void GenerateTable(int[] spaceWithColumns, string[] columnsName, Dictionary<string, IEnumerable<Evaluacion>> data)
        {
            //Imprimiendo cabeceras
            GenerateTable(spaceWithColumns, columnsName);
            var spaceTotal = spaceWithColumns.Sum() + (columnsName.Length - 1);
            string linea = "|" + string.Concat(Enumerable.Repeat("*", spaceTotal)) + "|";
            bool firstTitle = true;

            //Imprimiendo los datos
            foreach (var asignatura in data)
            {
                if (!firstTitle) WriteLine($"{linea}");
                var dataLine = "|" + asignatura.Key.PadRight(spaceTotal) + "|";
                WriteLine($"{dataLine}");
                WriteLine($"{linea}");

                var evaluaciones = data.Values.ToList();
                foreach (var item2 in evaluaciones)
                {
                    foreach (var item3 in item2)
                    {
                        if (item3.Asignatura.Nombre == asignatura.Key)
                        {
                            dataLine = "|" + item3.Alumno.Nombre.PadRight(spaceWithColumns[0]) + "|" + item3.Nota.ToString().PadRight(spaceWithColumns[1]) + "|";
                            WriteLine($"{dataLine}");
                        }
                    }
                    firstTitle = false;
                }
            }
            WriteLine($"{linea}");
        }

        public static void GenerateTable(int[] spaceWithColumns, string[] columnsName, Dictionary<string, IEnumerable<AlumnoPromedio>> data)
        {
            //Imprimiendo cabeceras
            GenerateTable(spaceWithColumns, columnsName);
            string linea = "|" + string.Concat(Enumerable.Repeat("*", spaceWithColumns.Sum() + (columnsName.Length - 1))) + "|";

            //Imprimiendo los datos
            foreach (var item in data)
            {
                var position = 1;
                foreach (var item2 in item.Value)
                {
                    var dataLine = "|" + item.Key.PadRight(spaceWithColumns[0]) + "|" + position.ToString().PadLeft(spaceWithColumns[1]) + "|" + item2.AlumnoNombre.PadRight(spaceWithColumns[2]) + "|" + item2.Promedio.ToString().PadRight(spaceWithColumns[3]) + "|";
                    WriteLine($"{dataLine}");
                    position += 1;
                }
            }
            WriteLine($"{linea}");
        }
    }
}