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
        [CpuFault]
        DivisionError,
        /// <summary>
        /// Выполнение пошаговой отладки, аппаратные точки останова
        /// </summary>
        [CpuTrap, CpuFault]
        DebugException,
        /// <summary>
        /// Не маскируемое прерывание (аппаратный сбой)
        /// </summary>
        [CpuInterrupt]
        NMIInterrupt,
        /// <summary>
        /// Выполнение инструкции INT3 (0xCC)
        /// </summary>
        [CpuTrap]
        Breakpoint,
        /// <summary>
        /// Выполнение INTO при установленном флаге OF
        /// </summary>
        [CpuTrap]
        Overflow,
        /// <summary>
        /// BOUND-инструкция: индекс вне диапазона
        /// </summary>
        [CpuFault]
        BoundRangeExceeded,
        /// <summary>
        /// Неизвестная/неподдерживаемая инструкция
        /// </summary>
        [CpuFault]
        InvalidOpcode,
        /// <summary>
        /// Попытка использовать FPU/MMX/SSE при отключённом CR0.EM
        /// </summary>
        [CpuFault]
        DeviceNotAvailable,
        /// <summary>
        /// Ошибка при обработке другого исключения
        /// </summary>
        [CpuAbort]
        DoubleFault,
        /// <summary>
        /// Устарело (на современных CPU вызывает #GP)
        /// </summary>
        [CpuFault]
        CoprocessorSegmentOverrun,
        /// <summary>
        /// Ошибка при переключении задач/сегментов
        /// </summary>
        [CpuFault]
        InvalidTSS,
        /// <summary>
        /// Обращение к несуществующему сегменту
        /// </summary>
        [CpuFault]
        SegmentNotPresent,
        /// <summary>
        /// Нарушение доступа к стеку (SS-сегмент)
        /// </summary>
        [CpuFault]
        StackSegmentFault,
        /// <summary>
        /// Любое нарушение защиты (неверные права доступа и т.д.)
        /// </summary>
        [CpuFault]
        GeneralProtectionFault,
        /// <summary>
        /// Обращение к несуществующей/защищённой странице памяти
        /// </summary>
        [CpuFault]
        PageFault,
        /// <summary>
        /// Не используется
        /// </summary>
        Reserved,
        /// <summary>
        /// Ошибка FPU (деление на 0, переполнение и т.д.)
        /// </summary>
        [CpuFault]
        x87FloatingPointError,
        /// <summary>
        /// Не выровненный доступ при CR0.AM=1
        /// </summary>
        [CpuFault]
        AlignmentCheck,
        /// <summary>
        /// Аппаратный сбой (ECC-ошибка памяти и т.д.)
        /// </summary>
        [CpuAbort]
        MachineCheck,
        /// <summary>
        /// Ошибка SSE/AVX-инструкций
        /// </summary>
        [CpuFault]
        SIMDFloatingPoint,
        /// <summary>
        /// Ошибка виртуализации (VT-x/AMD-V)
        /// </summary>
        [CpuFault]
        VirtualizationException,
        /// <summary>
        /// Зарезервировано для будущих процессоров
        /// </summary>
        Reserved2
    }
}
