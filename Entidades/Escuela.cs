using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Escuela : ObjetoEscuelaBase, ILugar
    {
        public List<Curso> Cursos { get; set; }
        public int AnioCreacion { get; set; }
        public TiposEscuela TipoEscuela { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        string ILugar.Direccion { get; set; }
        // public string ILugar.Direccion { get; set; }
        public Escuela(string nombre, int anio) => (Nombre, AnioCreacion) = (nombre, anio);
        public Escuela(
            string nombre,
            int anio,
            TiposEscuela tipoEscuela,
            string pais = "",
            string ciudad = ""
        )
        {
            (Nombre, AnioCreacion) = (nombre, anio);
            Pais = pais;
            Ciudad = ciudad;
        }

        void ILugar.LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limpiando escuela...");

            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }

            Console.WriteLine($"Escuela {Nombre} Limpia");
        }
    }
}