using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с клавиатурой
    /// </summary>
    public class BiosKeyboardInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosKeyboardInterruption() : base(0x16, "Прерывание для работы с клавиатурой")
        {
        }

        #region Управление

        /// <summary>
        /// Прочитать символ с клавиатуры
        /// </summary>
        /// <remarks>AL = ASCII-код символа, AH = скан-код</remarks>
        public void ReadChar()
        {
            PerformInterruption(CharReadFunction);
        }
        /// <summary>
        /// Проверить состояние буфера
        /// </summary>
        /// <remarks>ZF = 0 (символ есть), ZF = 1 (буфер пуст), AL = ASCII-код, AH = скан-код</remarks>
        public void CheckBufferStatus()
        {
            PerformInterruption(BufferStatusCheckFunction);
        }
        /// <summary>
        /// Получить состояние клавиш (Shift, Ctrl, Alt и т.д.).
        /// Все возможные клавиши перечислены здесь - <see cref="SpecialButton"/>
        /// </summary>
        /// <remarks>AL = состояние клавиш (биты: Insert, CapsLock, ScrollLock, NumLock, Alt, Ctrl)</remarks>
        public void GetButtonsStatus()
        {
            PerformInterruption(ButtonsStatusGetterFunction);
        }
        /// <summary>
        /// Обработать нажатие специальной клавиши
        /// </summary>
        /// <param name="button">Клавиша, нажатие которой надо обработать</param>
        /// <param name="pressedFunction">Название функции для обработки, если клавиша нажата</param>
        /// <param name="notPressedFunction">Нажатие функции для обработки, если клавиша не нажата</param>
        public void HandleSpecialButton(SpecialButton button, string? pressedFunction, string? notPressedFunction)
        {
            GetButtonsStatus();

            RealMode.Accumulator.Lower.And((byte)button);

            if (pressedFunction != null)
            {
                Asm.JumpIfNotZero(pressedFunction);
            }
            if (notPressedFunction != null)
            {
                Asm.Jump(notPressedFunction);
            }
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции чтения символа с клавиатуры
        /// </summary>
        public const byte CharReadFunction = 0x00;
        /// <summary>
        /// Номер функции проверки состояния буфера клавиатуры
        /// </summary>
        public const byte BufferStatusCheckFunction = 0x01;
        /// <summary>
        /// Номер функции получения состояния клавиш (Shift, Ctrl, Alt и т.д.)
        /// </summary>
        public const byte ButtonsStatusGetterFunction = 0x02;

        #endregion
    }
}
