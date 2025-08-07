namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Класс, представляющий байт доступа
    /// </summary>
    public abstract class AccessByte
    {
        /// <summary>
        /// Активен ли сегмент. Если он неактивен, то его нельзя использовать.
        /// </summary>
        public bool IsPresent { get; set; } = true;
        /// <summary>
        /// Уровень привилегий. По умолчанию Ring0.
        /// </summary>
        public PrivilegeLevel DPL { get; set; } = PrivilegeLevel.Ring0;
        /// <summary>
        /// Тип дескриптора.
        /// </summary>
        public abstract DescriptorType DescriptorType { get; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"Present={ToInt(IsPresent)}, DPL={DPL} ({DPL.Name}), Type={(int)DescriptorType} ({DescriptorType})";
        }

        /// <summary>
        /// Получить байт доступа
        /// </summary>
        /// <returns>Байт доступа</returns>
        protected abstract byte ToByte();

        #endregion

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="accessByte"><inheritdoc/></param>
        public static implicit operator byte(AccessByte accessByte)
        {
            return accessByte.ToByte();
        }

        #endregion

        #region Статика

        /// <summary>
        /// Получить числовое значение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static int ToInt(bool value) => TablesExtensions.ToInt(value);

        #endregion
    }
}
