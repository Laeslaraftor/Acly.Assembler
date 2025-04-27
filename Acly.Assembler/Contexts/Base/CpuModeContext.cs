using Acly.Assembler.Registers;

namespace Acly.Assembler.Contexts
{
    /// <summary>
    /// Базовый класс контекста режима работы процессора.
    /// </summary>
    public abstract class CpuModeContext
    {
        /// <summary>
        /// Режим работы процессора.
        /// </summary>
        public abstract Mode Mode { get; }

        #region Основные

        /// <summary>
        /// AX. Используется для арифметических операций и хранения результатов.
        /// </summary>
        public abstract GeneralRegister Accumulator { get; }
        /// <summary>
        /// BX. Используется для адресации памяти.
        /// </summary>
        public abstract GeneralRegister Base { get; }
        /// <summary>
        /// CX. Используется для счетчиков циклов.
        /// </summary>
        public abstract GeneralRegister Count { get; }
        /// <summary>
        /// DX. Используется для ввода/вывода и арифметических операций.
        /// </summary>
        public abstract GeneralRegister Data { get; }

        /// <summary>
        /// R8. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R8 { get; }
        /// <summary>
        /// R9. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R9 { get; }
        /// <summary>
        /// R10. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R10 { get; }
        /// <summary>
        /// R11. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R11 { get; }
        /// <summary>
        /// R12. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R12 { get; }
        /// <summary>
        /// R13. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R13 { get; }
        /// <summary>
        /// R14. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R14 { get; }
        /// <summary>
        /// R15. Дополнительный регистр.
        /// </summary>
        public abstract GeneralRegister R15 { get; }

        #endregion

        #region Сегменты

        /// <summary>
        /// CS. Указывает на сегмент кода.
        /// </summary>
        public abstract Register CodeSegment { get; }
        /// <summary>
        /// DS. Указывает на сегмент данных.
        /// </summary>
        public abstract Register DataSegment { get; }
        /// <summary>
        /// SS. Указывает на сегмент стека.
        /// </summary>
        public abstract Register StackSegment { get; }
        /// <summary>
        /// ES. Дополнительный сегмент для данных.
        /// </summary>
        public abstract Register ExtraSegment { get; }
        /// <summary>
        /// FS. Часто используется для доступа к данным, связанным с текущим потоком выполнения (например, стековые фреймы или Thread Local Storage).
        /// </summary>
        public abstract Register FrameSegment { get; }
        /// <summary>
        /// GS. Часто используется для доступа к глобальным или системным структурам данных.
        /// </summary>
        public abstract Register GlobalSegment { get; }

        #endregion

        #region Указатели

        /// <summary>
        /// IP. Указывает на следующую инструкцию для выполнения.
        /// </summary>
        public abstract Register InstructionPointer { get; }
        /// <summary>
        /// SP. Указывает на вершину стека.
        /// </summary>
        public abstract Register StackPointer { get; }
        /// <summary>
        /// BP. Используется для адресации в стеке.
        /// </summary>
        public abstract Register BasePointer { get; }

        #endregion

        #region Флаги

        /// <summary>
        /// ZF. Флаг нуля.
        /// </summary>
        public abstract Register ZeroFlag { get; }
        /// <summary>
        /// CF. Флаг переноса.
        /// </summary>
        public abstract Register CarryFlag { get; }
        /// <summary>
        /// SF. Флаг знака.
        /// </summary>
        public abstract Register SignFlag { get; }
        /// <summary>
        /// OF. Флаг переполнения.
        /// </summary>
        public abstract Register OverflowFlag { get; }
        /// <summary>
        /// IT. Разрешение прерываний.
        /// </summary>
        public abstract Register InterruptFlag { get; }
        /// <summary>
        /// DT. Направление обработки строк.
        /// </summary>
        public abstract Register DirectionFlag { get; }

        #endregion

        #region Статика

        /// <summary>
        /// Получить контекст режима работы процессора
        /// </summary>
        /// <param name="Mode">Режим работы процессора</param>
        /// <returns>Контекст режима работы процессора</returns>
        public static CpuModeContext GetContext(Mode Mode)
        {
            if (Mode == Mode.x32)
            {
                return ProtectedModeContext.Instance;
            }
            else if (Mode == Mode.x64)
            {
                return LongModeContext.Instance;
            }

           return RealModeContext.Instance;
        }

        #endregion
    }
}
