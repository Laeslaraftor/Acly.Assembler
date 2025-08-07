using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Построитель цвета BIOS
    /// </summary>
    public class ColorBuilder
    {
        /// <summary>
        /// Цвет текста
        /// </summary>
        public BiosColor TextColor { get; set; } = BiosColor.White;
        /// <summary>
        /// Цвет фона
        /// </summary>
        public BiosColor Background { get; set; } = BiosColor.Black;
        /// <summary>
        /// Мигает ли цвет
        /// </summary>
        public bool IsBlinking { get; set; }
        /// <summary>
        /// Яркий ли цвет
        /// </summary>
        public bool IsBright { get; set; } = true;

        #region Управление

        /// <summary>
        /// Получить значение цвета BIOS
        /// </summary>
        /// <returns>Цвет BIOS</returns>
        public byte ToValue()
        {
            byte blink = 0;
            byte brightness = 0;

            if (IsBlinking)
            {
                blink = 1;
            }
            if (!IsBright)
            {
                brightness = 1;
            }

            return (byte)(
                (blink << 7) |            // Мигание -> бит 7
                ((byte)Background << 4) | // Цвет фона -> биты 4–6
                (brightness << 3) |       // Яркость -> бит 3
                (byte)TextColor           // Цвет текста -> биты 0–2
            );
        }

        #endregion

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        public static implicit operator MemoryOperand(ColorBuilder builder)
        {
            return builder.ToValue();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        public static implicit operator byte(ColorBuilder builder)
        {
            return builder.ToValue();
        }

        #endregion
    }
}
