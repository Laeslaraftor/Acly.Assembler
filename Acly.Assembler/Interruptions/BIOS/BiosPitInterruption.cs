using Acly.Assembler.Registers;
using static Acly.Assembler.AsmOperators;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание программируемого таймера (PIT)
    /// </summary>
    public class BiosPitInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosPitInterruption() : base(0x1C, "Прерывание программируемого таймера (PIT)")
        {
        }

        #region Управление

        /// <summary>
        /// Сменить обработчик прерывания программируемого таймера (PIT)
        /// </summary>
        /// <param name="newHandler">Адрес обработчика</param>
        public void SetHandler(MemoryOperand newHandler)
        {
            Asm.Comment($"Начало смены PIT обработчика на {newHandler}");
            RealMode.Accumulator.Push();
            RealMode.Data.Push();
            RealMode.ExtraSegment.Push();

            Asm.EmptyLine();
            RealMode.Data.Set(newHandler);

            Asm.EmptyLine();
            RealMode.Accumulator.Xor(RealMode.Accumulator);
            RealMode.ExtraSegment.Set(RealMode.Accumulator);

            Asm.EmptyLine();
            Asm.DisableInterruptions();

            Set(Prefix.Word, MemoryOperand.Create(RealMode.ExtraSegment, Index, null, scale: 4), null, RealMode.Data);
            Set(Prefix.Word, MemoryOperand.Create(RealMode.ExtraSegment, Index, null, 2, scale: 4), null, RealMode.CodeSegment);

            Asm.EnableInterruptions();
            Asm.EmptyLine();

            RealMode.Accumulator.Pop();
            RealMode.Data.Pop();
            RealMode.ExtraSegment.Pop();
            Asm.Comment($"Cмена PIT обработчика на {newHandler} завершена");
        }
        /// <summary>
        /// Сменить обработчик прерывания программируемого таймера (PIT)
        /// </summary>
        /// <param name="newHandlerName">Название обработчика (функция)</param>
        public void SetHandler(string newHandlerName)
        {
            SetHandler(MemoryOperand.Create(newHandlerName, true));
        }

        #endregion
    }
}
