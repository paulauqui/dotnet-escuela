using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }
        string ILugar.Direccion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limpiando establecimiento...");
            Console.WriteLine($"Curos {Nombre} limpio");
        }
    }
}