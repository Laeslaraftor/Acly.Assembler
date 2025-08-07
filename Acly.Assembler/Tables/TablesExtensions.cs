namespace Acly.Assembler.Tables
{
    internal static class TablesExtensions
    {
        public static int ToInt(bool value)
        {
            if (value)
            {
                return 1;
            }

            return 0;
        }
    }
}
