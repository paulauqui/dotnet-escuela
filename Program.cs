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

        Printer.WriteTitle("Imprimir Objeto");
        ImprimirCursos(engine.Escuela);

        Dictionary<int, string> diccionario = new Dictionary<int, string>();
        diccionario.Add(10, "Paul");
        diccionario.Add(23, "Lorem Ipsum");

        foreach (var KeyValPair in diccionario)
        {
            WriteLine($"Key: {KeyValPair.Key} Valor: {KeyValPair.Value}");
        }

        var dicTmp = engine.GetDiccionarioObjetos();

        // Printer.WriteTitle("Acceso a diccionario");
        // diccionario[0] = "Pekerman";
        // WriteLine(diccionario[23]);
        // WriteLine(diccionario[0]);


        // Printer.WriteTitle("Otro diccionario");
        // var dic = new Dictionary<string, string>();
        // dic["Luna"] = "Cuerpo celeste que gira al rededor de la tierra";
        // WriteLine(dic["Luna"]);
        // dic["Luna"] = "Protagonista";
        // WriteLine(dic["Luna"]);




        // var listaObjetos = engine.GetObjetoEscuelas(
        //     out int conteoEvaluaciones,
        //     out int conteoCursos,
        //     out int conteoAsignaturas,
        //     out int conteoAlumnos,
        //     true,
        //     false,
        //     false,
        //     false
        // );

        // var listaILugar = from obj in listaObjetos
        //                   where obj is ILugar
        //                   select (ILugar)obj;

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