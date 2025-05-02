using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с часами реального времени (RTC)
    /// </summary>
    public class BiosRtcInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosRtcInterruption() : base(0x1A, "Прерывание для работы с часами реального времени (RTC)")
        {
        }

        #region Управление

        /// <summary>
        /// Получить время суток в тиках
        /// </summary>
        /// <remarks>
        /// CX:DX = количество тиков (1 тик = 1/18 секунды), AL = 0 (если прошло менее 24 часов)
        /// </remarks>
        public void GetDayTime()
        {
            PerformInterruption(DayTimeGetterFunction);
        }
        /// <summary>
        /// Получить время
        /// </summary>
        /// <remarks>
        /// CH = часы (BCD), CL = минуты (BCD), DH = секунды (BCD), DL = день недели
        /// </remarks>
        public void GetTime()
        {
            PerformInterruption(RtcTimeGetterFunction);
        }
        /// <summary>
        /// Установить время
        /// </summary>
        /// <param name="hours">Количество часов (BCD)</param>
        /// <param name="minutes">Количество минут (BCD)</param>
        /// <param name="seconds">Количество секунд (BCD)</param>
        /// <param name="dayOfTheWeek">День недели</param>
        public void SetTime(MemoryOperand hours, MemoryOperand minutes, MemoryOperand seconds, MemoryOperand dayOfTheWeek)
        {
            PerformSetter(RtcTimeSetterFunction, hours, minutes, seconds, dayOfTheWeek);
        }
        /// <summary>
        /// Получить дату
        /// </summary>
        /// <remarks>
        /// CH = год (BCD), CL = месяц (BCD), DH = день (BCD), DL = день недели
        /// </remarks>
        public void GetDate()
        {
            PerformInterruption(RtcDateGetterFunction);
        }
        /// <summary>
        /// Установить дату
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <param name="day">День</param>
        /// <param name="dayOfTheWeek">День недели</param>
        public void SetDate(MemoryOperand year, MemoryOperand month, MemoryOperand day, MemoryOperand dayOfTheWeek)
        {
            PerformSetter(RtcTimeSetterFunction, year, month, day, dayOfTheWeek);
        }

        private void PerformSetter(byte code, MemoryOperand ch, MemoryOperand cl, 
                                  MemoryOperand dh, MemoryOperand dl)
        {
            RealMode.Count.Higher.Set(ch);
            RealMode.Count.Lower.Set(cl);
            RealMode.Data.Higher.Set(dh);
            RealMode.Data.Lower.Set(dl);

            PerformInterruption(code);
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции получения времени суток в тиках
        /// </summary>
        public const byte DayTimeGetterFunction = 0x00;
        /// <summary>
        /// Номер функции получения времени
        /// </summary>
        public const byte RtcTimeGetterFunction = 0x02;
        /// <summary>
        /// Номер функции установки времени
        /// </summary>
        public const byte RtcTimeSetterFunction = 0x03;
        /// <summary>
        /// Номер функции получения даты
        /// </summary>
        public const byte RtcDateGetterFunction = 0x04;
        /// <summary>
        /// Номер функции установки даты
        /// </summary>
        public const byte RtcDateSetterFunction = 0x04;

        #endregion
    }
}
