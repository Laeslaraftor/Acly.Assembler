using Acly.Assembler.Registers;

namespace Acly.Assembler.Contexts
{
    /// <summary>
    /// Контекст 64 битного режима процессора (Long Mode).
    /// </summary>
    public class LongModeContext : ProtectedModeContext
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Mode Mode { get; } = Mode.x64;

        #region Общие регистры

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Accumulator
        {
            get
            {
                _accumulator ??= new(Size.x64, "RAX", null, () => base.Accumulator);
                return _accumulator;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Base
        {
            get
            {
                _base ??= new(Size.x64, "RBX", null, () => base.Base);
                return _base;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Count
        {
            get
            {
                _count ??= new(Size.x64, "RCX", null, () => base.Count);
                return _count;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Data
        {
            get
            {
                _data ??= new(Size.x64, "RDX", null, () => base.Data);
                return _data;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R8
        {
            get
            {
                _r8 ??= new(Size.x64, "R8", null, () => base.R8);
                return _r8;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R9
        {
            get
            {
                _r9 ??= new(Size.x64, "R9", null, () => base.R9);
                return _r9;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R10
        {
            get
            {
                _r10 ??= new(Size.x64, "R10", null, () => base.R10);
                return _r10;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R11
        {
            get
            {
                _r11 ??= new(Size.x64, "R11", null, () => base.R11);
                return _r11;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R12
        {
            get
            {
                _r12 ??= new(Size.x64, "R12", null, () => base.R12);
                return _r12;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R13
        {
            get
            {
                _r13 ??= new(Size.x64, "R13", null, () => base.R13);
                return _r13;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R14
        {
            get
            {
                _r14 ??= new(Size.x64, "R14", null, () => base.R14);
                return _r14;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R15
        {
            get
            {
                _r15 ??= new(Size.x64, "R15", null, () => base.R15);
                return _r15;
            }
        }

        /// <summary>
        /// RSI. Используется как источник данных в строковых операциях. 
        /// Может использоваться для передачи параметров функциям.
        /// </summary>
        public Register SourceIndex { get; } = new(Size.x64, "RSI");
        /// <summary>
        /// RDI. Используется как приемник данных в строковых операциях.
        /// Также часто используется для передачи параметров функциям.
        /// </summary>
        public Register DestinationIndex { get; } = new(Size.x64, "RDI");

        #endregion

        #region Указатели

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register InstructionPointer { get; } = new(Size.x64, "RIP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register StackPointer { get; } = new(Size.x64, "RSP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register BasePointer { get; } = new(Size.x64, "RBP");

        #endregion

        #region Флаги

        /// <summary>
        /// ID. Поддержка CPUID.
        /// </summary>
        public Register IdentificationFlag { get; } = new(Size.x64, "ID");
        /// <summary>
        /// AC. Проверка выравнивания.
        /// </summary>
        public Register AlignmentCheckFlag { get; } = new(Size.x64, "AC");

        #endregion

        #region Дополнительно

        /// <summary>
        /// MMX 0. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia0 { get; } = new(Size.x64, "MM0");
        /// <summary>
        /// MMX 1. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia1 { get; } = new(Size.x64, "MM1");
        /// <summary>
        /// MMX 2. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia2 { get; } = new(Size.x64, "MM2");
        /// <summary>
        /// MMX 3. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia3 { get; } = new(Size.x64, "MM3");
        /// <summary>
        /// MMX 4. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia4 { get; } = new(Size.x64, "MM4");
        /// <summary>
        /// MMX 5. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia5 { get; } = new(Size.x64, "MM5");
        /// <summary>
        /// MMX 6. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia6 { get; } = new(Size.x64, "MM6");
        /// <summary>
        /// MMX 7. Используются для работы с числами с плавающей запятой, 
        /// SIMD-вычислениями (Single Instruction, Multiple Data) и другими специализированными задачами
        /// </summary>
        public Register Multimedia7 { get; } = new(Size.x64, "MM7");

        /// <summary>
        /// XMM8. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia8 { get; } = new(Size.x128, "XMM8");
        /// <summary>
        /// XMM9. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia9 { get; } = new(Size.x128, "XMM9");
        /// <summary>
        /// XMM10. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia10 { get; } = new(Size.x128, "XMM10");
        /// <summary>
        /// XMM11. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia11 { get; } = new(Size.x128, "XMM11");
        /// <summary>
        /// XMM12. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia12 { get; } = new(Size.x128, "XMM12");
        /// <summary>
        /// XMM13. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia13 { get; } = new(Size.x128, "XMM13");
        /// <summary>
        /// XMM14. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia14 { get; } = new(Size.x128, "XMM14");
        /// <summary>
        /// XMM15. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia15 { get; } = new(Size.x128, "XMM15");

        /// <summary>
        /// YMM8. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia8 { get; } = new(Size.x256, "YMM8");
        /// <summary>
        /// YMM9. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia9 { get; } = new(Size.x256, "YMM9");
        /// <summary>
        /// YMM10. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia10 { get; } = new(Size.x256, "YMM10");
        /// <summary>
        /// YMM11. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia11 { get; } = new(Size.x256, "YMM11");
        /// <summary>
        /// YMM12. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia12 { get; } = new(Size.x256, "YMM12");
        /// <summary>
        /// YMM13. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia13 { get; } = new(Size.x256, "YMM13");
        /// <summary>
        /// YMM14.Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia14 { get; } = new(Size.x256, "YMM14");
        /// <summary>
        /// YMM15. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia15 { get; } = new(Size.x256, "YMM15");

        /// <summary>
        /// ZMM8. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia8 { get; } = new(Size.x512, "ZMM8");
        /// <summary>
        /// ZMM9. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia9 { get; } = new(Size.x512, "ZMM9");
        /// <summary>
        /// ZMM10. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia10 { get; } = new(Size.x512, "ZMM10");
        /// <summary>
        /// ZMM11. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia11 { get; } = new(Size.x512, "ZMM11");
        /// <summary>
        /// ZMM12. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia12 { get; } = new(Size.x512, "ZMM12");
        /// <summary>
        /// ZMM13. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia13 { get; } = new(Size.x512, "ZMM13");
        /// <summary>
        /// ZMM14. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia14 { get; } = new(Size.x512, "ZMM14");
        /// <summary>
        /// ZMM16. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia16 { get; } = new(Size.x512, "ZMM16");
        /// <summary>
        /// ZMM17. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia17 { get; } = new(Size.x512, "ZMM17");
        /// <summary>
        /// ZMM18. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia18 { get; } = new(Size.x512, "ZMM18");
        /// <summary>
        /// ZMM19. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia19 { get; } = new(Size.x512, "ZMM19");
        /// <summary>
        /// ZMM20. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia20 { get; } = new(Size.x512, "ZMM20");
        /// <summary>
        /// ZMM21. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia21 { get; } = new(Size.x512, "ZMM21");
        /// <summary>
        /// ZMM22. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia22 { get; } = new(Size.x512, "ZMM22");
        /// <summary>
        /// ZMM23. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia23 { get; } = new(Size.x512, "ZMM23");
        /// <summary>
        /// ZMM24. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia24 { get; } = new(Size.x512, "ZMM24");
        /// <summary>
        /// ZMM25. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia25 { get; } = new(Size.x512, "ZMM25");
        /// <summary>
        /// ZMM26. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia26 { get; } = new(Size.x512, "ZMM26");
        /// <summary>
        /// ZMM27. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia27 { get; } = new(Size.x512, "ZMM27");
        /// <summary>
        /// ZMM28. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia28 { get; } = new(Size.x512, "ZMM28");
        /// <summary>
        /// ZMM29. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia29 { get; } = new(Size.x512, "ZMM29");
        /// <summary>
        /// ZMM30. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia30 { get; } = new(Size.x512, "ZMM30");
        /// <summary>
        /// ZMM31. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia31 { get; } = new(Size.x512, "ZMM31");

        #endregion

        private GeneralRegister? _accumulator;
        private GeneralRegister? _base;
        private GeneralRegister? _count;
        private GeneralRegister? _data;
        private GeneralRegister? _r8;
        private GeneralRegister? _r9;
        private GeneralRegister? _r10;
        private GeneralRegister? _r11;
        private GeneralRegister? _r12;
        private GeneralRegister? _r13;
        private GeneralRegister? _r14;
        private GeneralRegister? _r15;

        #region Статика

        /// <summary>
        /// Глобальный экземпляр 64 битного контекста
        /// </summary>
        public static readonly new LongModeContext Instance = new();

        #endregion
    }
}
