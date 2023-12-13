using CoreEscuela;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        // AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
        // AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(100, 100, 1);
        // AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;

        var engine = new EscuelaEngine();
        engine.Inicializar();
        Printer.WriteTitle("Bienvenidos a nuestra escuela");

        var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
        var evalList = reporteador.GetListaEvaluaciones();
        var evalAsignaturas = reporteador.GetListaAsignaturas();
        var evalEvaluacionxAsignaturas = reporteador.GetDiccionarioEvaluacionXAsignatura();
        var evalAlumnoxAsignaturas = reporteador.GetPromedioAlumnosXAsignatura();

        Printer.WriteTitle("Captura de evaluacion");
        var newEval = new Evaluacion();

        string nombre, notastring;
        float nota;

        WriteLine("Ingrese el nombre de la evaluacion");
        Printer.PresioneEnter();
        nombre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede ser vacio");
        }
        {
            newEval.Nombre = nombre.ToLower();
            WriteLine("Evaluacion ingresada correctamente");
        }


        WriteLine("Ingrese el nombre de la asignacion");
        Printer.PresioneEnter();
        notastring = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(notastring))
        {
            throw new ArgumentException("Nota no puede ser vacio");
        }
        {
            try
            {
                newEval.Nota = float.Parse(notastring);
                if (newEval.Nota < 0 || newEval.Nota > 5)
                    throw new ArgumentOutOfRangeException("Nota fuera de rango (0-5)");
            }
            catch (ArgumentOutOfRangeException arge)
            {
                Printer.WriteTitle(arge.Message);
                WriteLine("Saliendo del programa");

            }
            catch (Exception)
            {
                Printer.WriteTitle("Nota no es numero");
                WriteLine("Saliendo del programa");
            }
        }









        Printer.WriteTitle("FIN");
    }

    private static void AccionDelEvento(object sender, EventArgs e)
    {
        Printer.WriteTitle("Saliendo");
        //Printer.Beep(3000, 1000, 3);
        Printer.WriteTitle("Salio");
    }

    private static void ImprimirCursos(Escuela escuela)
    {
        if (escuela?.Cursos != null)
        {
            foreach (var curso in escuela.Cursos)
            {
                WriteLine($"Nombre: {curso.Nombre}, ID {curso.UniqueId}");
            }
        }
    }
}