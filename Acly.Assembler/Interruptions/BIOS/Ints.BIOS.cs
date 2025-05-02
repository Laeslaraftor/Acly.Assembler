namespace Acly.Assembler.Interruptions
{
    public static partial class Ints
    {
        /// <summary>
        /// Класс, содержащий прерывания BIOS
        /// </summary>
        public static class BIOS
        {
            /// <summary>
            /// Прерывание для работы с видеосистемой.
            /// Установка видеорежима: AH = 0x00;
            /// вывод символа на экран: AH = 0x0E;
            /// чтение позиции курсора: AH = 0x03;
            /// прокрутка окна вверх: AH = 0x06;
            /// прокрутка окна вниз: AH = 0x07
            /// </summary>
            public static BiosVideoInterruption Video { get; } = new();
            /// <summary>
            /// Прерывание для работы с дисками (жёсткими дисками, флоппи-дисками).
            /// Сброс дисковой подсистемы: AH = 0x00;
            /// чтение секторов с диска: AH = 0x02;
            /// запись секторов на диск: AH = 0x03;
            /// получение параметров диска: AH = 0x08;
            /// проверка поддержки расширенных функций дисков: AH = 0x41
            /// </summary>
            public static BiosDiskInterruption Disk { get; } = new();
            /// <summary>
            /// Прерывание для работы с клавиатурой.
            /// Чтение символа с клавиатуры: AH = 0x00;
            /// проверка состояния буфера клавиатуры: AH = 0x01;
            /// получение состояния клавиш (Shift, Ctrl и т.д.): AH = 0x02
            /// </summary>
            public static BiosKeyboardInterruption Keyboard { get; } = new();
            /// <summary>
            /// Прерывание для работы с системным таймером и часами реального времени (RTC).
            /// Получение времени суток: AH = 0x00;
            /// получение времени из RTC: AH = 0x02;
            /// установка времени в RTC: AH = 0x03;
            /// получение даты из RTC: AH = 0x04
            /// </summary>
            public static BiosRtcInterruption RTC { get; } = new();
            /// <summary>
            /// Прерывание для различных системных функций - задержка на указанное время, получение карты памяти системы.
            /// Для задержки: AH = 0x86; 
            /// для получения карты памяти: AH = 0xE820;
            /// получить информацию о системе: AH = 0xC0
            /// </summary>
            public static BiosSystemFunctionsInterruption SystemFunctions { get; } = new();
            /// <summary>
            /// Прерывание для перезагрузки системы.
            /// </summary>
            public static SimpleInterruption Reboot { get; } = new(0x19);
            /// <summary>
            /// Прерывание для получения информации об оборудовании.
            /// </summary>
            /// <remarks>
            /// AX = битовая маска(см.описание ниже)
            /// </remarks>
            public static SimpleInterruption Hardware { get; } = new(0x11);
            /// <summary>
            /// Прерывание для получения информации о размере базовой памяти (RAM) в системе.
            /// Это прерывание возвращает значение, которое указывает количество доступной памяти в килобайтах, 
            /// начиная с адреса 0x00000 до адреса 0x9FFFF (первый мегабайт памяти).
            /// </summary>
            /// <remarks>
            /// AX = размер базовой памяти в килобайтах
            /// </remarks>
            public static SimpleInterruption Memory { get; } = new(0x12);
            /// <summary>
            /// Прерывание для работы с параллельным портом (принтером).
            /// Инициализация принтера: AH = 0x00;
            /// печать символа: AH = 0x01;
            /// получить состояния принтера: AH = 0x02
            /// </summary>
            public static SimpleInterruption Printer { get; } = new(0x10);
            /// <summary>
            /// Прерывание для работы с программируемым интервальным таймером (PIT).
            /// Этот таймер - ключ к реализации многозадачности.
            /// </summary>
            public static BiosPitInterruption PIT { get; } = new();
            /// <summary>
            /// Прерывание для связи через последовательный порт (COM).
            /// Инициализация COM-порта: AH = 0x00;
            /// отправка символа через COM-порт: AH = 0x01;
            /// чтение символа из COM-порта: AH = 0x02;
            /// получение статуса COM-порта: AH = 0x03 
            /// </summary>
            public static BiosComInterruption COM { get; } = new();
            /// <summary>
            /// Прерывание для работы с сетевыми устройствами.
            /// </summary>
            public static SimpleInterruption NIC { get; } = new(0x1D);
            /// <summary>
            /// Это прерывание вызывается, когда происходит аппаратная ошибка при работе с диском 
            /// (например, чтение/запись сектора завершилась неудачей). 
            /// Это прерывание может быть перехвачено для выполнения пользовательских действий.
            /// </summary>
            public static SimpleInterruption DiskExceptionHandler { get; } = new(0x1E);
            /// <summary>
            /// Это прерывание вызывается при возникновении ошибок в видеосистеме. 
            /// Например, это может произойти при попытке записи в недопустимую область видеопамяти или при неправильной настройке видеорежима.
            /// </summary>
            public static SimpleInterruption VideoExceptionHandler { get; } = new(0x1F);
        }
    }
}
