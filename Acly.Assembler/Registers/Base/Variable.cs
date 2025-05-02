using System;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс переменной
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    public abstract class Variable<T> : VariableBase
    {
        internal Variable(Size size, string name, bool isReserved, T defaultValue) 
            : base(size, name, isReserved, defaultValue)
        {
            if (size != Size.x16 && size != Size.x32 && size != Size.x64)
            {
                throw new ArgumentException("Размер должен быть 16 бит, 32 бит или 64 бита");
            }

            IsReserved = isReserved;
        }

        /// <summary>
        /// Значение переменной по умолчанию
        /// </summary>
        public new T Value
        {
            get
            {
                if (base.Value is T result)
                {
                    return result;
                }

                throw new InvalidOperationException("Кучерявое дело");
            }
            set => base.Value = value ?? DefaultValue;
        }
        
    }
}
