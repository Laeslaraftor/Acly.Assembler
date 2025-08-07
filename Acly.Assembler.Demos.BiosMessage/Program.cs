using System.Runtime.InteropServices;

namespace Acly.Assembler.Demos.BiosMessage
{
    public class Program
    {
        [DllImport("*")]
        private static extern void ClearScreen();

        [DllImport("*")]
        private static extern void PrintChar(char c);

        public static void Print(string s)
        {
            foreach (char c in s)
                PrintChar(c);
        }

        public static void Main()
        {
            ClearScreen();
            Print("Hello from C# via BIOS!");

            while (true) { } // Бесконечный цикл
        }
    }
}
