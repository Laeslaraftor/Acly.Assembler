using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание
    /// </summary>
    public readonly struct Interruption : IComparable, IEquatable<Interruption>
    {
        /// <summary>
        /// Создать прерывание
        /// </summary>
        /// <param name="index"></param>
        public Interruption(byte index)
        {
            _index = index;
            _value = $"0x{index:X}";
        }

        /// <summary>
        /// Номер прерывания
        /// </summary>
        public readonly byte Index
        {
            get
            {
                if (_index == null)
                {
                    throw new AssemblerException("Недопустимое прерывание");
                }

                return _index.GetValueOrDefault();
            }
        }
        /// <summary>
        /// Название прерывания
        /// </summary>
        public readonly string Value
        {
            get
            {
                if (_value == null)
                {
                    throw new AssemblerException("Недопустимое прерывание");
                }

                return _value;
            }
        }

        private readonly byte? _index;
        private readonly string? _value;

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"><inheritdoc/></param>
        public static implicit operator Interruption(byte index)
        {
            return new(index);
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
            return _index == other._index &&
                   _value == other._value;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public int CompareTo(object obj)
        {
            if (obj is not Interruption interruption || interruption._index == null)
            {
                return -1;
            }

            return _index.GetValueOrDefault().CompareTo(interruption._index.GetValueOrDefault());
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_index, _value);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
        

        #endregion
    }
}
