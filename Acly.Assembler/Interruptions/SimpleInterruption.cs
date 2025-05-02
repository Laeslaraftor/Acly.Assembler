namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Простое прерывание
    /// </summary>
    public class SimpleInterruption : Interruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        public SimpleInterruption(byte index) : base(index)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        /// <param name="name"><inheritdoc/></param>
        public SimpleInterruption(byte index, string name) : base(index, name)
        {
        }
    }
}
