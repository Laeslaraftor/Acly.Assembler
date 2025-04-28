namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Переменная массива
    /// </summary>
    public class ArrayVariable : Variable
    {
        internal ArrayVariable(Size size, string name, bool isReserved) : base(size, name, isReserved)
        {
            Value = 0;
        }

        /// <summary>
        /// Длина массива
        /// </summary>
        public uint Length
        {
            get => _length;
            set
            {
                if (_length != value)
                {
                    _length = value;
                    OnPropertyChanged(nameof(Length));
                }
            }
        }

        private uint _length = 1;

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void UpdateLine()
        {
            AssemblerLine = $"{Name} {GetTypeForSize(Size)} {Length}";

            if (IsReserved)
            {
                AssemblerLine += $" dup({Value})";
            }
        }

        #endregion
    }
}
