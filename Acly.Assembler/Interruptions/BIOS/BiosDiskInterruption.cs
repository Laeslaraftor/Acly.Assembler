using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с дисками
    /// </summary>
    public class BiosDiskInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosDiskInterruption() : base(0x13, "Прерывание для работы с дисками")
        {
        }

        #region Управление

        /// <summary>
        /// Сбросить дисковую подсистему
        /// </summary>
        /// <param name="diskType">Тип диска, которого надо сбросить</param>   
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка)</remarks>
        public void ResetDiskSubsystem(DiskType diskType)
        {
            RealMode.Data.Lower.Set((byte)diskType);
            PerformInterruption(DiskSubsystemResetFunction);
        }
        /// <summary>
        /// Получить состояние последней операции
        /// </summary>
        /// <remarks>AH = код ошибки, CF = 0 (успех), CF = 1 (ошибка)</remarks>
        public void GetLastOperationStatus()
        {
            PerformInterruption(LastOperationStatusGetterFunction);
        }
        /// <summary>
        /// Прочитать сектора с диска
        /// </summary>
        /// <param name="diskType">Тип диска с которого надо прочитать сектора</param>
        /// <param name="amountOfSectors">Количество секторов, которые надо прочитать</param>
        /// <param name="cylinder">Номер цилиндра</param>
        /// <param name="sector">Номер сектора</param>
        /// <param name="header">Номер головки</param>
        /// <param name="bufferAddress">Адрес, по которому будут записаны прочитанные данные</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = код ошибки, AL = количество прочитанных секторов</remarks>
        public void ReadSectors(DiskType diskType, MemoryOperand amountOfSectors, MemoryOperand cylinder, 
                                MemoryOperand sector, MemoryOperand header, MemoryOperand bufferAddress)
        {
            SectorsFunction(ReadSectorFunction, diskType, amountOfSectors, cylinder, sector, header, bufferAddress);
        }
        /// <summary>
        /// Прочитать сектора с диска
        /// </summary>
        /// <param name="diskType">Тип диска с которого надо прочитать сектора</param>
        /// <param name="amountOfSectors">Количество секторов, которые надо прочитать</param>
        /// <param name="bufferAddress">Адрес, по которому будут записаны прочитанные данные</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = код ошибки, AL = количество прочитанных секторов</remarks>
        public void ReadSectors(DiskType diskType, MemoryOperand amountOfSectors, MemoryOperand bufferAddress)
        {
            ReadSectors(diskType, amountOfSectors, 0, 2, 0, bufferAddress);
        }
        /// <summary>
        /// Записать данные в сектора
        /// </summary>
        /// <param name="diskType">Тип диска на который надо записать сектора</param>
        /// <param name="amountOfSectors">Количество секторов, которые надо записать</param>
        /// <param name="cylinder">Номер цилиндра</param>
        /// <param name="sector">Номер сектора</param>
        /// <param name="header">Номер головки</param>
        /// <param name="bufferAddress">Адрес, по которому будут браться данные для записи</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = код ошибки, AL = количество записанных секторов</remarks>
        public void WriteSectors(DiskType diskType, MemoryOperand amountOfSectors, MemoryOperand cylinder,
                                 MemoryOperand sector, MemoryOperand header, MemoryOperand bufferAddress)
        {
            SectorsFunction(WriteSectorFunction, diskType, amountOfSectors, cylinder, sector, header, bufferAddress);
        }
        /// <summary>
        /// Записать данные в сектора
        /// </summary>
        /// <param name="diskType">Тип диска на который надо записать сектора</param>
        /// <param name="amountOfSectors">Количество секторов, которые надо записать</param>
        /// <param name="bufferAddress">Адрес, по которому будут браться данные для записи</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = код ошибки, AL = количество записанных секторов</remarks>
        public void WriteSectors(DiskType diskType, MemoryOperand amountOfSectors, MemoryOperand bufferAddress)
        {
            WriteSectors(diskType, amountOfSectors, 0, 1, 0, bufferAddress);
        }
        /// <summary>
        /// Проверить готовность секторов
        /// </summary>
        /// <param name="diskType">Тип диска на котором надо проверить готовность секторов</param>
        /// <param name="amountOfSectors">Количество секторов для проверки</param>
        /// <param name="sector">Сектор, с которого начнётся проверка</param>
        /// <param name="cylinder">Номер цилиндра</param>
        /// <param name="header">Номер головки</param>
        public void CheckSectorsReadyStatus(DiskType diskType, MemoryOperand amountOfSectors, MemoryOperand sector, 
                                            MemoryOperand cylinder, MemoryOperand header)
        {
            RealMode.Accumulator.Lower.Set(amountOfSectors);
            RealMode.Count.Higher.Set(cylinder);
            RealMode.Count.Lower.Set(sector);
            RealMode.Data.Higher.Set(header);
            RealMode.Data.Lower.Set((byte)diskType);
            PerformInterruption(CheckSectorReadyStatusFunction);
        }
        /// <summary>
        /// Получить параметры диска
        /// </summary>
        /// <param name="diskType">Тип диска, которого надо получить параметры</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), BL = тип диска, CX = геометрия, DX = количество головок/цилиндров</remarks>
        public void GetDiskParameters(DiskType diskType)
        {
            RealMode.Data.Lower.Set((byte)diskType);
            PerformInterruption(DiskParametersGetterFunction);
        }
        /// <summary>
        /// Установить параметры диска
        /// </summary>
        /// <param name="diskType">Тип диска для которого надо установить параметры</param>
        /// <param name="parametersBufferAddress">Адрес буфера с параметрами диска</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = код ошибки</remarks>
        public void SetDiskParameters(DiskType diskType, MemoryOperand parametersBufferAddress)
        {
            RealMode.Base.Set(parametersBufferAddress);
            RealMode.Data.Lower.Set((byte)diskType);
            PerformInterruption(DiskParametersSetterFunction);
        }
        /// <summary>
        /// Получить тип диска
        /// </summary>
        /// <param name="diskType">Тип диска которого надо получить тип диска (???)</param>
        /// <remarks>CF = 0 (успех), CF = 1 (ошибка), AH = тип диска</remarks>
        public void GetDiskType(DiskType diskType)
        {
            RealMode.Data.Lower.Set((byte)diskType);
            PerformInterruption(DiskTypeGetterFunction);
        }

        private void SectorsFunction(byte number, DiskType diskType, MemoryOperand amountOfSectors, 
                                     MemoryOperand cylinder, MemoryOperand sector, MemoryOperand header, 
                                     MemoryOperand bufferAddress)
        {
            RealMode.Accumulator.Lower.Set(amountOfSectors);
            RealMode.Count.Higher.Set(cylinder);
            RealMode.Count.Lower.Set(sector);
            RealMode.Data.Higher.Set(header);
            RealMode.Data.Lower.Set((byte)diskType);
            RealMode.Base.Set(bufferAddress);
            PerformInterruption(number);
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции сброса дисковой подсистемы
        /// </summary>
        public const byte DiskSubsystemResetFunction = 0x00;
        /// <summary>
        /// Номер функции получения состояния последней операции
        /// </summary>
        public const byte LastOperationStatusGetterFunction = 0x01;
        /// <summary>
        /// Номер функции чтения секторов с диска
        /// </summary>
        public const byte ReadSectorFunction = 0x02;
        /// <summary>
        /// Номер функции записи данных в сектора
        /// </summary>
        public const byte WriteSectorFunction = 0x03;
        /// <summary>
        /// Номер функции проверки готовности секторов
        /// </summary>
        public const byte CheckSectorReadyStatusFunction = 0x04;
        /// <summary>
        /// Номер функции получения параметров диска
        /// </summary>
        public const byte DiskParametersGetterFunction = 0x08;
        /// <summary>
        /// Номер функции установки параметров диска
        /// </summary>
        public const byte DiskParametersSetterFunction = 0x18;
        /// <summary>
        /// Номер функции получения типа диска
        /// </summary>
        public const byte DiskTypeGetterFunction = 0x15;

        #endregion
    }
}
