using static System.Console;

namespace CoreEscuela.Util
{
    public class Printer
    {
        public static void DrawLine(int size = 10)
        {
            WriteLine("".PadLeft(size, '='));
        }

        public static void WriteTitle(string title, int size = 10)
        {
            DrawLine(title.Length + 4);
            WriteLine($"| {title} |");
            DrawLine(title.Length + 4);
        }

        public static void Beep(int hz = 2000, int time = 500, int count = 1)
        {
            while (count-- > 0)
            {
                System.Console.Beep(hz, time);
            }
        }

        public static void PresioneEnter()
        {
            WriteLine("Presione enter para continuar");
            //WriteLine("",Pad);
        }

    }
}