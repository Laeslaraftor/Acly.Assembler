using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание
    /// </summary>
    public abstract class Interruption : IComparable, IEquatable<Interruption>
    {
        /// <summary>
        /// Создать новый экземпляр прерывания
        /// </summary>
        /// <param name="index">Номер прерывания</param>
        protected Interruption(byte index) : this(index, $"0x{index:0}")
        {
        }
        /// <summary>
        /// Создать новый экземпляр прерывания
        /// </summary>
        /// <param name="index">Номер прерывания</param>
        /// <param name="name">Название прерывания</param>
        protected Interruption(byte index, string name)
        {
            Index = index;
            Value = name;
        }

        /// <summary>
        /// Номер прерывания
        /// </summary>
        public byte Index { get; }
        /// <summary>
        /// Название прерывания
        /// </summary>
        public string Value { get; }

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        public static implicit operator Interruption(byte index)
        {
            return new SimpleInterruption(index);
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override bool Equals(object? obj)
        {
            return obj is Interruption interruption &&
                   Equals(interruption);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="other"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool Equals(Interruption other)
        {
            return Index == other.Index &&
                   Value == other.Value;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public int CompareTo(object obj)
        {
            if (obj is not Interruption interruption)
            {
                return -1;
            }

            return Index.CompareTo(interruption.Index);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Value);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"0x{Index:X}";
        }
        

        #endregion
    }
}
