using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание аппаратного таймера (PIT)
    /// </summary>
    public class CpuPitInterruption : Interruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public CpuPitInterruption() : base((int)CpuInterruption.PIT)
        {
        }

        /// <summary>
        /// Инициализировать PIT
        /// </summary>
        /// <param name="intervalInHertz">
        /// Интервал прерывания в герцах. 100 герц = 10 миллисекунд.
        /// Проще говоря, это количество прерываний в секунду
        /// </param>
        public void Initialize(int intervalInHertz)
        {
            Asm.Comment("Инициализация PIT");
            RealMode.Accumulator.Lower.Set(0x36);
            MemoryOperand.Create(0x43).Output(RealMode.Accumulator.Lower);

            Asm.EmptyLine();

            var pit40 = MemoryOperand.Create(0x40);
            RealMode.Accumulator.Set(Frequency / intervalInHertz);
            pit40.Output(RealMode.Accumulator.Lower);
            RealMode.Accumulator.Lower.Set(RealMode.Accumulator.Higher);
            pit40.Output(RealMode.Accumulator.Lower);
        }
        /// <summary>
        /// Переназначение PIC, чтобы IRQ0 был на векторе 0x20 (<see cref="CpuInterruption.PIT"/>).
        /// Это надо сделать до включения прерываний.
        /// </summary>
        public void Remap()
        {
            MemoryOperand pic21 = 0x21;
            MemoryOperand picA1 = 0xA1;

            Asm.Comment("ICW1: начало инициализации");
            RealMode.Accumulator.Lower.Set(0x11);
            Pic20.Output(RealMode.Accumulator.Lower);
            MemoryOperand.Create(0xA0).Output(RealMode.Accumulator.Lower);

            Asm.EmptyLine();
            Asm.Comment("ICW2: вектор начала IRQ0 = 0x20");
            RealMode.Accumulator.Lower.Set(Pic20);
            pic21.Output(RealMode.Accumulator.Lower);
            RealMode.Accumulator.Lower.Set(0x28);
            picA1.Output(RealMode.Accumulator.Lower);

            Asm.EmptyLine();
            Asm.Comment("ICW3: IRQ2 соединён с slave");
            RealMode.Accumulator.Lower.Set(0x04);
            pic21.Output(RealMode.Accumulator.Lower);
            RealMode.Accumulator.Lower.Set(0x02);
            picA1.Output(RealMode.Accumulator.Lower);

            Asm.EmptyLine();
            Asm.Comment("ICW4: режим 8086");
            RealMode.Accumulator.Lower.Set(0x01);
            pic21.Output(RealMode.Accumulator.Lower);
            picA1.Output(RealMode.Accumulator.Lower);
        }
        /// <summary>
        /// Сбросить сигнал на PIC
        /// </summary>
        public void Reset()
        {
            RealMode.Accumulator.Lower.Set(Pic20);
            Pic20.Output(RealMode.Accumulator.Lower);
        }

        #region Статика

        /// <summary>
        /// Тактовая частота программного таймера
        /// </summary>
        public static int Frequency { get; } = 1193182;

        private static RealModeContext RealMode => RealModeContext.Instance;
        private static ProtectedModeContext ProtectedMode => ProtectedModeContext.Instance;
        private static readonly MemoryOperand Pic20 = (int)CpuInterruption.PIT;

        #endregion
    }
}
