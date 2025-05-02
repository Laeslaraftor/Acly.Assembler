namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Видеорежим
    /// </summary>
    public enum VideoMode : byte
    {
        /// <summary>
        /// Текстовый режим 40x25 чёрно-белый
        /// </summary>
        TextSmallGrayscale = 0x00,
        /// <summary>
        /// Текстовый режим 40x25 цветной
        /// </summary>
        TextSmallColors = 0x01,
        /// <summary>
        /// Текстовый режим 80x25 чёрно-белый
        /// </summary>
        TextGrayScale = 0x02,
        /// <summary>
        /// Текстовый режим 80x25 цветной
        /// </summary>
        TextColors = 0x03,
        /// <summary>
        /// Графический режим 320x200, 256 цветов
        /// </summary>
        Graphical = 0x13
    }
}
