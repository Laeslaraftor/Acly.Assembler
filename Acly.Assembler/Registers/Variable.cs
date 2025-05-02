using System;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Переменная ассемблера
    /// </summary>
    public class Variable : Variable<ulong>
    {
        internal Variable(Size size, string name, bool isReserved) : base(size, name, isReserved, 0)
        {
            if (size != Size.x16 && size != Size.x32 && size != Size.x64)
            {
                throw new ArgumentException("Размер должен быть 16 бит, 32 бит или 64 бита");
            }

            IsReserved = isReserved;
        }
    }
}
