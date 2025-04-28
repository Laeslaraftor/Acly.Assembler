namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Класс, содержащий прерывания
    /// </summary>
    public static partial class Ints
    {
        /// <summary>
        /// Класс, содержащий прерывания процессора
        /// </summary>
        public static class CPU
        {
            /// <summary>
            /// Деление на ноль или переполнение при делении.
            /// </summary>
            public static Interruption DivideByZero { get; } = new(0);
            /// <summary>
            /// Отладочное исключение (например, точка останова или одиночный шаг).
            /// </summary>
            public static Interruption DebugException { get; } = new(1);
            /// <summary>
            /// Неперехватываемое прерывание (например, аппаратная ошибка).
            /// </summary>
            public static Interruption NonMaskableInterrupt { get; } = new(2);
            /// <summary>
            /// Точка останова (инструкция int3).
            /// </summary>
            public static Interruption Breakpoint { get; } = new(3);
            /// <summary>
            /// Переполнение (инструкция into, если флаг OF установлен).
            /// </summary>
            public static Interruption Overflow { get; } = new(4);
            /// <summary>
            /// Выход за границы диапазона (инструкция bound).
            /// </summary>
            public static Interruption BoundRangeExceeded { get; } = new(5);
            /// <summary>
            /// Недопустимая или неопределённая инструкция.
            /// </summary>
            public static Interruption InvalidOpcode { get; } = new(6);
            /// <summary>
            /// Попытка использования сопроцессора, когда он недоступен.
            /// </summary>
            public static Interruption DeviceNotAvailable { get; } = new(7);
            /// <summary>
            /// Двойная ошибка (одновременно возникают два исключения).
            /// </summary>
            public static Interruption DoubleFault { get; } = new(8);
            /// <summary>
            /// Ошибка сегмента сопроцессора (устарело, начиная с Intel 386).
            /// </summary>
            public static Interruption CoprocessorSegmentOverrun { get; } = new(9);
            /// <summary>
            /// Ошибка связанной задачи (TSS повреждён или недоступен).
            /// </summary>
            public static Interruption InvalidTTS { get; } = new(10);
            /// <summary>
            /// Сегмент не существует (бит P в дескрипторе сегмента сброшен).
            /// </summary>
            public static Interruption SegmentNotPresent { get; } = new(11);
            /// <summary>
            /// Ошибка стека (например, выход за границы сегмента стека).
            /// </summary>
            public static Interruption StackSegmentFault { get; } = new(12);
            /// <summary>
            /// Общая ошибка защиты (например, неверный селектор или привилегии).
            /// </summary>
            public static Interruption GeneralProtectionFault { get; } = new(13);
            /// <summary>
            /// Ошибка страничной адресации (например, доступ к несуществующей странице).
            /// </summary>
            public static Interruption PageFault { get; } = new(14);
            /// <summary>
            /// Не используется.
            /// </summary>
            public static Interruption Reserved { get; } = new(15);
            /// <summary>
            /// Исключение сопроцессора FPU (например, деление на ноль в FPU).
            /// </summary>
            public static Interruption X86FpuFloatingPointError { get; } = new(16);
            /// <summary>
            /// Ошибка выравнивания данных(если включена проверка выравнивания).
            /// </summary>
            public static Interruption AlignmentCheck { get; } = new(17);
            /// <summary>
            /// Аппаратная ошибка (например, сбой памяти или процессора).
            /// </summary>
            public static Interruption MachineCheck { get; } = new(18);
            /// <summary>
            /// Исключение SIMD (например, деление на ноль в SSE).
            /// </summary>
            public static Interruption SIMDFloatingPointException { get; } = new(19);
            /// <summary>
            /// Ошибка виртуализации (например, некорректная операция в гостевой системе).
            /// </summary>
            public static Interruption VirtualizationException { get; } = new(20);
        }
    }
}
