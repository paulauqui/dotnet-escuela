using CoreEscuela;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        var engine = new EscuelaEngine();
        engine.Inicializar();

        // Printer.WriteTitle("Imprimir Objeto");
        // ImprimirCursos(engine.Escuela);

        Dictionary<int, string> diccionario = new Dictionary<int, string>();
        diccionario.Add(10, "Paul");
        diccionario.Add(23, "Lorem Ipsum");

        foreach (var KeyValPair in diccionario)
        {
            WriteLine($"Key: {KeyValPair.Key} Valor: {KeyValPair.Value}");
        }

        var dicTmp = engine.GetDiccionarioObjetos();

        engine.ImprimirDiccionario(dicTmp, true);

        Printer.WriteTitle("FIN");
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