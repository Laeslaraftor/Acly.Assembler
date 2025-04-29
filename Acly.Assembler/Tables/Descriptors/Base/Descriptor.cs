using System;
using System.Collections.Generic;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс дескриптора. 
    /// Дескриптор - это некие "метаданные" данных/кода
    /// </summary>
    public abstract class Descriptor
    {
        /// <summary>
        /// Создать экземпляр дескриптора
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Descriptor(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Название дескриптора
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Существует ли дескриптор. По умолчанию - true
        /// </summary>
        public virtual bool IsPresent { get; set; } = true;
        /// <summary>
        /// DPL. Уровень привилегий дескриптора
        /// </summary>
        public virtual PrivilegeLevel PrivilegeLevel { get; set; }

        #region Ассемблер

        /// <summary>
        /// Получить ассемблерный код дескриптора
        /// </summary>
        /// <returns></returns>
        public abstract string ToAssembler();

        #endregion

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"Дескриптор: {Name}";
        }

        /// <summary>
        /// Получить отформатированное имя дескриптора
        /// </summary>
        /// <returns>Отформатированное имя дескриптора</returns>
        protected string FormatName()
        {
            return Name.Trim().Replace(" ", string.Empty);
        }

        #endregion
    }
}
