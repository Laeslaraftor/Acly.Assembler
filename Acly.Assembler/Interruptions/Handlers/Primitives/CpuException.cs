namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Исключения, ошибки и прерывания процессора
    /// </summary>
    public enum CpuException
    {
        /// <summary>
        /// Деление на ноль или переполнение при DIV/IDIV
        /// </summary>
        DivisionError,
        /// <summary>
        /// Выполнение пошаговой отладки, аппаратные точки останова
        /// </summary>
        DebugException,
        /// <summary>
        /// Не маскируемое прерывание (аппаратный сбой)
        /// </summary>
        NMIInterrupt,
        /// <summary>
        /// Выполнение инструкции INT3 (0xCC)
        /// </summary>
        Breakpoint,
        /// <summary>
        /// Выполнение INTO при установленном флаге OF
        /// </summary>
        Overflow,
        /// <summary>
        /// BOUND-инструкция: индекс вне диапазона
        /// </summary>
        BoundRangeExceeded,
        /// <summary>
        /// Неизвестная/неподдерживаемая инструкция
        /// </summary>
        InvalidOpcode,
        /// <summary>
        /// Попытка использовать FPU/MMX/SSE при отключённом CR0.EM
        /// </summary>
        DeviceNotAvailable,
        /// <summary>
        /// Ошибка при обработке другого исключения
        /// </summary>
        DoubleFault,
        /// <summary>
        /// Устарело (на современных CPU вызывает #GP)
        /// </summary>
        CoprocessorSegmentOverrun,
        /// <summary>
        /// Ошибка при переключении задач/сегментов
        /// </summary>
        InvalidTSS,
        /// <summary>
        /// Обращение к несуществующему сегменту
        /// </summary>
        SegmentNotPresent,
        /// <summary>
        /// Нарушение доступа к стеку (SS-сегмент)
        /// </summary>
        StackSegmentFault,
        /// <summary>
        /// Любое нарушение защиты (неверные права доступа и т.д.)
        /// </summary>
        GeneralProtectionFault,
        /// <summary>
        /// Обращение к несуществующей/защищённой странице памяти
        /// </summary>
        PageFault,
        /// <summary>
        /// Не используется
        /// </summary>
        Reserved,
        /// <summary>
        /// Ошибка FPU (деление на 0, переполнение и т.д.)
        /// </summary>
        x87FloatingPointError,
        /// <summary>
        /// Не выровненный доступ при CR0.AM=1
        /// </summary>
        AlignmentCheck,
        /// <summary>
        /// Аппаратный сбой (ECC-ошибка памяти и т.д.)
        /// </summary>
        MachineCheck,
        /// <summary>
        /// Ошибка SSE/AVX-инструкций
        /// </summary>
        SIMDFloatingPoint,
        /// <summary>
        /// Ошибка виртуализации (VT-x/AMD-V)
        /// </summary>
        VirtualizationException,
        /// <summary>
        /// Зарезервировано для будущих процессоров
        /// </summary>
        Reserved2
    }
}
