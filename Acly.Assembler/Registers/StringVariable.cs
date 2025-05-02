namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Строковая переменная
    /// </summary>
    public class StringVariable : Variable<string>
    {
        internal StringVariable(Size size, string name, bool isReserved) 
            : base(size, name, isReserved, string.Empty)
        {
        }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void UpdateLine()
        {
            AssemblerLine = $"{Name} {GetTypeForSize(Size)} \"{Value}\", 0";
        }

        #endregion
    }
}
