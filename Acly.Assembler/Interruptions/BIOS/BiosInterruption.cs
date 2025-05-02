using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Базовый класс прерывания BIOS
    /// </summary>
    public class BiosInterruption : Interruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        protected BiosInterruption(byte index) : base(index)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        /// <param name="name"><inheritdoc/></param>
        protected BiosInterruption(byte index, string name) : base(index, name)
        {
        }

        #region Управление

        /// <summary>
        /// Выполнить простое прерывание
        /// </summary>
        /// <param name="code">Код варианта прерывания</param>
        protected void PerformInterruption(byte code)
        {
            RealMode.Accumulator.Higher.Set(code);
            Asm.Interrupt(this);
        }

        #endregion

        #region Статика

        /// <summary>
        /// Контекст реального режима
        /// </summary>
        protected static RealModeContext RealMode => RealModeContext.Instance;

        #endregion
    }
}
