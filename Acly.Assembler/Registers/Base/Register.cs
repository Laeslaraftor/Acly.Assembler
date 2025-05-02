using Acly.Assembler.Contexts;
using System;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс регистра
    /// </summary>
    public class Register
    {
        /// <summary>
        /// Создать экземпляр регистра
        /// </summary>
        /// <param name="name">Название регистра</param>
        /// <param name="size">Битовый размер регистра</param>
        public Register(Size size, string name)
        {
            Size = size;
            _name = name;
        }

        /// <summary>
        /// Размер регистра в битах.
        /// </summary>
        public Size Size { get; }
        /// <summary>
        /// Название регистра.
        /// </summary>
        public string Name
        {
            get
            {
                if (!AsmSettings.UpperCaseRegisters && CanChangeCase)
                {
                    return _name.ToLower();
                }

                return _name;
            }
        }

        /// <summary>
        /// Можно ли менять регистр названия
        /// </summary>
        protected virtual bool CanChangeCase { get; } = true;

        private readonly string _name;

        #region Основные операции

        /// <summary>
        /// Задать значение
        /// </summary>
        /// <param name="source">Источник значения</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Set(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Set, typeName, asPointer, source);
        }
        /// <summary>
        /// Прибавить значение
        /// </summary>
        /// <param name="source">Источник прибавляемого значения</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Add(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Add, typeName, asPointer, source);
        }
        /// <summary>
        /// Вычесть значение
        /// </summary>
        /// <param name="source">Источник вычитаемого значения</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Subtract(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Subtract, typeName, asPointer, source);
        }
        /// <summary>
        /// Умножить без знака на значение
        /// </summary>
        /// <param name="source">Источник множителя</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Multiply(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Multiply, typeName, asPointer, source);
        }
        /// <summary>
        /// Умножить со знаком на значение
        /// </summary>
        /// <param name="source">Источник множителя</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void IMultiply(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.IMultiply, typeName, asPointer, source);
        }
        /// <summary>
        /// Разделить без знака на значение. 
        /// Это действие разделит регистры <see cref="CpuModeContext.Accumulator"/> и <see cref="CpuModeContext.Data"/> на текущее значение
        /// </summary>
        public virtual void Divide()
        {
            AsmOperators.Divide(this);
        }
        /// <summary>
        /// Разделить со знаком на значение.
        /// Это действие разделит регистры <see cref="CpuModeContext.Accumulator"/> и <see cref="CpuModeContext.Data"/> на текущее значение
        /// </summary>
        public virtual void IDivide()
        {
            AsmOperators.IDivide(this);
        }
        /// <summary>
        /// Увеличить значение на 1
        /// </summary>
        public virtual void Increment()
        {
            AsmOperators.Increment(this);
        }
        /// <summary>
        /// Уменьшить значение на 1
        /// </summary>
        public virtual void Decrement()
        {
            AsmOperators.Decrement(this);
        }

        /// <summary>
        /// Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="source">Источник, адрес которого надо вычислить и установить</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void LoadEffectiveAddress(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.LoadEffectiveAddress, typeName, asPointer, source);
        }
        /// <summary>
        /// Поместить значение в стек
        /// </summary>
        public virtual void Push()
        {
            AsmOperators.Push(this);
        }
        /// <summary>
        /// Извлечь значение из стека
        /// </summary>
        public virtual void Pop()
        {
            AsmOperators.Pop(this);
        }

        #endregion

        #region Сдвиги

        /// <summary>
        /// Сдвинуть влево
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void ShiftLeft(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.ShiftLeft, typeName, asPointer, source);
        }
        /// <summary>
        /// Сдвинуть вправо
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void ShiftRight(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.ShiftRight, typeName, asPointer, source);
        }
        /// <summary>
        /// Арифметически сдвинуть влево
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void ShiftArithmeticLeft(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.ShiftArithmeticLeft, typeName, asPointer, source);
        }
        /// <summary>
        /// Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void ShiftArithmeticRight(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.ShiftArithmeticRight, typeName, asPointer, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево. 
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void RotateLeft(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.RotateLeft, typeName, asPointer, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void RotateRight(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.RotateRight, typeName, asPointer, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void RotateCarryLeft(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.RotateCarryLeft, typeName, asPointer, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="source">Источник значения сдвига</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void RotateCarryRight(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.RotateCarryRight, typeName, asPointer, source);
        }

        #endregion

        #region Логические операции

        /// <summary>
        /// Логическая операция И.
        /// </summary>
        /// <param name="source">Источник значения для выполнения операции</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void And(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.And, typeName, asPointer, source);
        }
        /// <summary>
        /// Логическая операция ИЛИ.
        /// </summary>
        /// <param name="source">Источник значения для выполнения операции</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Or(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Or, typeName, asPointer, source);
        }
        /// <summary>
        /// Логическая операция исключающее ИЛИ.
        /// </summary>
        /// <param name="source">Источник значения для выполнения операции</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Xor(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Xor, typeName, asPointer, source);
        }
        /// <summary>
        /// Логическая операция НЕ. Инвертирует все биты
        /// </summary>
        public virtual void Not()
        {
            AsmOperators.Not(this);
        }

        #endregion

        #region Проверки

        /// <summary>
        /// Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="source">Источник значения для выполнения операции</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void EqualsZero(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Test, typeName, asPointer, source);
        }
        /// <summary>
        /// Проверить равны ли значения
        /// </summary>
        /// <param name="source">Источник значения для выполнения операции</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        public virtual void Compare(Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            Operation(AsmOperators.Compare, typeName, asPointer, source);
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Выполнить простую операцию
        /// </summary>
        /// <param name="operation">Операция</param>
        /// <param name="source">Значение для прохерачивания. {operation} {this}, {source}</param>
        /// <param name="typeName">Тип данных</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        protected void Operation(Action<Prefix?, MemoryOperand, Prefix?, MemoryOperand> operation, 
                                 Prefix? typeName, bool asPointer, MemoryOperand source)
        {
            operation(typeName, MemoryOperand.Create(this, asPointer), null, source);
        }

        #endregion
    }
}
