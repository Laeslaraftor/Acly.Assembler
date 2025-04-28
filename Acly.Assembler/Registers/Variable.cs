using System;
using System.ComponentModel;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Переменная ассемблера
    /// </summary>
    public class Variable : Register, INotifyPropertyChanged
    {
        internal Variable(Size size, string name, bool isReserved) : base(size, name)
        {
            if (size != Size.x16 && size != Size.x32 && size != Size.x64)
            {
                throw new ArgumentException("Размер должен быть 16 бит, 32 бит или 64 бита");
            }

            IsReserved = isReserved;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Значение переменной по умолчанию. По умолчанию - 1
        /// </summary>
        public ulong Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        /// <summary>
        /// Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/>
        /// </summary>
        public bool IsReserved
        {
            get => _isReserved;
            set
            {
                if (_isReserved != value)
                {
                    _isReserved = value;
                    OnPropertyChanged(nameof(IsReserved));
                }
            }
        }
        /// <summary>
        /// Строка кода переменной
        /// </summary>
        public virtual string AssemblerLine
        {
            get => _assemblerLine;
            set
            {
                if (_assemblerLine != value)
                {
                    _assemblerLine = value;

                    if (_disablePropertyChangedEvent)
                    {
                        return;
                    }

                    _disablePropertyChangedEvent = true;
                    OnPropertyChanged(nameof(AssemblerLine));
                    _disablePropertyChangedEvent = false;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override bool CanChangeCase { get; } = false;

        private ulong _value = 1;
        private bool _isReserved;
        private string _assemblerLine = string.Empty;
        private bool _disablePropertyChangedEvent;

        #region Управление

        /// <summary>
        /// Обновить строку кода
        /// </summary>
        protected virtual void UpdateLine()
        {
            AssemblerLine = $"{Name} {GetTypeForSize(Size)} {Value}";
        }

        /// <summary>
        /// Получить название типа переменной для определённого размера
        /// </summary>
        /// <param name="size">Размер данных. Принимаются только x16, x32, x64</param>
        /// <returns>Название типа переменной</returns>
        /// <exception cref="ArgumentException"></exception>
        protected virtual string GetTypeForSize(Size size)
        {
            if (size == Size.x16)
            {
                if (!IsReserved)
                {
                    return ReservedRealType;
                }

                return RealType;
            }
            else if (size == Size.x32)
            {
                if (!IsReserved)
                {
                    return ReservedProtectedType;
                }

                return ProtectedType;
            }
            else if (size == Size.x64)
            {
                if (!IsReserved)
                {
                    return ReservedLongType;
                }

                return LongType;
            }

            throw new ArgumentException("Должен быть указан размер в 16 бит, 32 бит или 64 бита");
        }

        #endregion

        #region События

        /// <summary>
        /// Событие изменения значения поля
        /// </summary>
        /// <param name="propertyName">Название поля, значение которого было изменено</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            UpdateLine();
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        #endregion

        #region Константы

        private const string RealType = "db";
        private const string ReservedRealType = "resb";

        private const string ProtectedType = "dd";
        private const string ReservedProtectedType = "resd";

        private const string LongType = "dq";
        private const string ReservedLongType = "resq";

        #endregion
    }
}
