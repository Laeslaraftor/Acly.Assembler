using Acly.Assembler.Registers;

namespace Acly.Assembler.Contexts
{
    /// <summary>
    /// Контекст 32 битного режима процессора (Protected Mode).
    /// </summary>
    public class ProtectedModeContext : RealModeContext
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Mode Mode { get; } = Mode.x32;

        #region Общие регистры

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Accumulator
        {
            get
            {
                _accumulator ??= new(Size.x32, "EAX", 
                    () => LongModeContext.Instance.Accumulator, () => base.Accumulator);
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
                _base ??= new(Size.x32, "EBX", 
                    () => LongModeContext.Instance.Base, () => base.Base);
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
                _count ??= new(Size.x32, "ECX",
                    () => LongModeContext.Instance.Count, () => base.Count);
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
                _data ??= new(Size.x32, "EDX",
                    () => LongModeContext.Instance.Data, () => base.Data);
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
                _r8 ??= new(Size.x32, "R8D", null, () => base.R8);
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
                _r9 ??= new(Size.x32, "R9D", null, () => base.R9);
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
                _r10 ??= new(Size.x32, "R10D", null, () => base.R10);
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
                _r11 ??= new(Size.x32, "R11D", null, () => base.R11);
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
                _r12 ??= new(Size.x32, "R12D", null, () => base.R12);
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
                _r13 ??= new(Size.x32, "R13D", null, () => base.R13);
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
                _r14 ??= new(Size.x32, "R14D", null, () => base.R14);
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
                _r15 ??= new(Size.x32, "R15D", null, () => base.R15);
                return _r15;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister SourceIndex
        {
            get
            {
                _sourceIndex ??= new(Size.x32, "ESI",
                    () => LongModeContext.Instance.SourceIndex, () => base.SourceIndex);
                return _sourceIndex;
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister DestinationIndex
        {
            get
            {
                _destinationIndex ??= new(Size.x32, "EDI",
                    () => LongModeContext.Instance.DestinationIndex, () => base.DestinationIndex);
                return _destinationIndex;
            }
        }

        #endregion

        #region Управляющие регистры

        /// <summary>
        /// CR0. Управляет глобальными параметрами процессора. 
        /// PE - Protection Enabled, Разрешает защищенный режим (PE = 1 → Protected Mode);
        /// PG - Paging,  Включает страничную адресацию (PG = 1 → Paging Enabled);
        /// EM - Emulation, Указывает на эмуляцию сопроцессора;
        /// TS - Task Switched,  Используется для отслеживания переключения задач.
        /// </summary>
        public Register Control0 { get; } = new(Size.x32, "CR0");
        /// <summary>
        /// CR1. Зарезервирован и не используется.
        /// </summary>
        public Register Control1 { get; } = new(Size.x32, "CR1");
        /// <summary>
        /// CR2. Содержит линейный адрес, вызвавший ошибку страничной адресации (Page Fault).
        /// Используется для диагностики проблем с памятью.
        /// </summary>
        public Register Control2 { get; } = new(Size.x32, "CR2");
        /// <summary>
        /// CR3. Указывает на базовый адрес таблицы страниц (Page Directory).
        /// Используется для управления страничной адресацией.
        /// В Long Mode также может содержать дополнительные флаги.
        /// </summary>
        public Register Control3 { get; } = new(Size.x32, "CR3");
        /// <summary>
        /// CR4. Управляет расширенными функциями процессора.
        /// PSE - Page Size Extension, Разрешает использование больших страниц (4 MB); 
        /// PAE - Physical Address Extension, Включает поддержку 36-битной физической адресации.
        /// ASFXSR - Разрешает использование расширенных регистров FPU (XMM), 
        /// VMXE - Virtual Machine Extensions Enable, Включает поддержку виртуализации.
        /// </summary>
        public Register Control4 { get; } = new(Size.x32, "CR4");
        /// <summary>
        /// CR8. Управляет приоритетом прерываний (Task Priority Level). Используется в многопроцессорных системах.
        /// </summary>
        public Register Control8 { get; } = new(Size.x32, "CR8");

        #endregion

        #region Системные регистры

        /// <summary>
        /// GDTR. GDT содержит дескрипторы сегментов памяти.
        /// </summary>
        public Register GlobalDescriptorTable { get; } = new(Size.x32, "GDTR");
        /// <summary>
        /// LDTR. Используется для хранения дескрипторов сегментов, специфичных для задачи.
        /// </summary>
        public Register LocalDescriptorTable { get; } = new(Size.x32, "LDTR");
        /// <summary>
        /// IDTR. Содержит дескрипторы обработчиков прерываний и исключений.
        /// </summary>
        public Register InterruptDescriptorTable { get; } = new(Size.x32, "IDTR");
        /// <summary>
        /// TR. Cодержит состояние задачи (регистры, указатели стека и т.д.).
        /// </summary>
        public Register Task { get; } = new(Size.x32, "TR");

        #endregion

        #region Указатели

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register InstructionPointer { get; } = new(Size.x32, "EIP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register StackPointer { get; } = new(Size.x32, "ESP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register BasePointer { get; } = new(Size.x32, "EBP");

        #endregion

        #region Флаги

        /// <summary>
        /// Флаг режима виртуального 8086.
        /// </summary>
        public Register VirtualModeFlag { get; } = new(Size.x32, "VM");
        /// <summary>
        /// Уровень привилегий для операций ввода/вывода.
        /// </summary>
        public Register IOPrivilegeLevelFlag { get; } = new(Size.x32, "IOPL");

        #endregion

        #region Дополнительно

        /// <summary>
        /// ST0. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop0 { get; } = new(Size.x80, "ST0");
        /// <summary>
        /// ST1. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop1 { get; } = new(Size.x80, "ST1");
        /// <summary>
        /// ST2. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop2 { get; } = new(Size.x80, "ST2");
        /// <summary>
        /// ST3. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop3 { get; } = new(Size.x80, "ST3");
        /// <summary>
        /// ST4. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop4 { get; } = new(Size.x80, "ST4");
        /// <summary>
        /// ST5. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop5 { get; } = new(Size.x80, "ST5");
        /// <summary>
        /// ST6. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop6 { get; } = new(Size.x80, "ST6");
        /// <summary>
        /// ST7. Относится к архитектуре x87 FPU (Floating-Point Unit) — сопроцессора для вычислений с плавающей точкой в старых x86-процессорах.
        /// Не путать со стеком вызовов!
        /// </summary>
        public Register StackTop7 { get; } = new(Size.x80, "ST7");

        /// <summary>
        /// XMM0. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia0 { get; } = new(Size.x128, "XMM0");
        /// <summary>
        /// XMM1. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia1 { get; } = new(Size.x128, "XMM1");
        /// <summary>
        /// XMM2. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia2 { get; } = new(Size.x128, "XMM2");
        /// <summary>
        /// XMM3. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia3 { get; } = new(Size.x128, "XMM3");
        /// <summary>
        /// XMM4. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia4 { get; } = new(Size.x128, "XMM4");
        /// <summary>
        /// XMM5. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia5 { get; } = new(Size.x128, "XMM5");
        /// <summary>
        /// XMM6. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia6 { get; } = new(Size.x128, "XMM6");
        /// <summary>
        /// XMM7. Используются для работы с числами с плавающей запятой и SIMD-операциями.
        /// Поддерживают операции над одинарной (float, 32 бита) и двойной (double, 64 бита) точностью.
        /// </summary>
        public Register XMultimedia7 { get; } = new(Size.x128, "XMM7");

        /// <summary>
        /// YMM0. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia0 { get; } = new(Size.x256, "YMM0");
        /// <summary>
        /// YMM1. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia1 { get; } = new(Size.x256, "YMM1");
        /// <summary>
        /// YMM2. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia2 { get; } = new(Size.x256, "YMM2");
        /// <summary>
        /// YMM3. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia3 { get; } = new(Size.x256, "YMM3");
        /// <summary>
        /// YMM4. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia4 { get; } = new(Size.x256, "YMM4");
        /// <summary>
        /// YMM5. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia5 { get; } = new(Size.x256, "YMM5");
        /// <summary>
        /// YMM6. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia6 { get; } = new(Size.x256, "YMM6");
        /// <summary>
        /// YMM7. Расширяют XMM-регистры (128 бит) и используются для ускорения операций с плавающей точкой и SIMD-вычислений.
        /// </summary>
        public Register YMultimedia7 { get; } = new(Size.x256, "YMM7");

        /// <summary>
        /// ZMM0. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia0 { get; } = new(Size.x512, "ZMM0");
        /// <summary>
        /// ZMM1. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia1 { get; } = new(Size.x512, "ZMM1");
        /// <summary>
        /// ZMM2. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia2 { get; } = new(Size.x512, "ZMM2");
        /// <summary>
        /// ZMM3. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia3 { get; } = new(Size.x512, "ZMM3");
        /// <summary>
        /// ZMM4. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia4 { get; } = new(Size.x512, "ZMM4");
        /// <summary>
        /// ZMM5. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia5 { get; } = new(Size.x512, "ZMM5");
        /// <summary>
        /// ZMM6. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia6 { get; } = new(Size.x512, "ZMM6");
        /// <summary>
        /// ZMM7. Векторные регистры, введённые с расширением AVX-512.
        /// </summary>
        public Register ZMultimedia7 { get; } = new(Size.x512, "ZMM7");

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
        private GeneralRegister? _sourceIndex;
        private GeneralRegister? _destinationIndex;

        #region Статика

        /// <summary>
        /// Глобальный экземпляр 32 битного контекста
        /// </summary>
        public static readonly new ProtectedModeContext Instance = new();

        #endregion
    }
}
