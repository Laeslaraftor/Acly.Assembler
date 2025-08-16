using Acly.Assembler.Registers;

namespace Acly.Assembler.Video
{
    /// <summary>
    /// Класс, представляющий структуру VBE с информацией о видеорежиме
    /// </summary>
    public class VbeModeInfoBlockStructure : ICodeGenerator
    {
        /// <summary>
        /// Создать новый экземпляр структуры с информацией о видеорежиме
        /// </summary>
        public VbeModeInfoBlockStructure() : this("vbe_mode_info_block")
        {

        }
        /// <summary>
        /// Создать новый экземпляр структуры с информацией о видеорежиме
        /// </summary>
        /// <param name="name">Название структуры</param>
        public VbeModeInfoBlockStructure(string name)
        {
            Name = name;
            _struct = new(name)
            {
                IsBss = true,
            };

            _struct.CreateField(nameof(Attributes), Prefix.Word, 1, "Атрибуты режима (битовая маска)", true);
            _struct.CreateField(nameof(WindowAAttributes), Prefix.Byte, 1, "Атрибуты окна A", true);
            _struct.CreateField(nameof(WindowBAttributes), Prefix.Byte, 1, "Атрибуты окна B", true);
            _struct.CreateField(nameof(WindowGranularity), Prefix.Word, 1, "Размер гранулярности окна (в КБ)", true);
            _struct.CreateField(nameof(WindowSize), Prefix.Word, 1, "Размер окна (в КБ)", true);
            _struct.CreateField(nameof(WindowASegment), Prefix.Word, 1, "Сегмент окна A", true);
            _struct.CreateField(nameof(WindowBSegment), Prefix.Word, 1, "Сегмент окна B", true);
            _struct.CreateField(nameof(WindowFunctionPointer), Prefix.Dword, 1, "Указатель на функцию переключения окна", true);
            _struct.CreateField(nameof(BytesPerScanLine), Prefix.Word, 1, "Байт на строку (ширина * бит на пиксель / 8)", true);
            _struct.CreateField(nameof(Width), Prefix.Word, 1, "Ширина в пикселях", true);
            _struct.CreateField(nameof(Height), Prefix.Word, 1, "Высота в пикселях", true);
            _struct.CreateField(nameof(CharWidth), Prefix.Byte, 1, "Ширина символа (обычно 8)", true);
            _struct.CreateField(nameof(CharHeight), Prefix.Byte, 1, "Высота символа (обычно 16)", true);
            _struct.CreateField(nameof(NumberOfPlanes), Prefix.Byte, 1, "Число плоскостей (обычно 1)", true);
            _struct.CreateField(nameof(BitsPerPixel), Prefix.Word, 1, "Бит на пиксель (8, 15, 16, 24, 32)", true);
            _struct.CreateField(nameof(NumberOfBanks), Prefix.Byte, 1, "Число банков памяти (устарело)", true);
            _struct.CreateField(nameof(MemoryModel), Prefix.Byte, 1, "Тип цветовой модели (1=текст, 4=прямой цвет, 6=палитра)", true);
            _struct.CreateField(nameof(BankSize), Prefix.Byte, 1, "Размер банка (устарело)", true);
            _struct.CreateField(nameof(NumberOfImagePages), Prefix.Byte, 1, "Число доступных страниц изображения", true);
            _struct.CreateField(nameof(Reserved1), Prefix.Byte, 1, "Зарезервировано", true);
            _struct.CreateField(nameof(RedMaskSize), Prefix.Byte, 1, "Размер красной маски (обычно 8)", true);
            _struct.CreateField(nameof(RedFieldPosition), Prefix.Byte, 1, "Позиция красного канала", true);
            _struct.CreateField(nameof(GreenMaskSize), Prefix.Byte, 1, "Размер зеленой маски", true);
            _struct.CreateField(nameof(GreenFieldPosition), Prefix.Byte, 1, "Позиция зеленого канала", true);
            _struct.CreateField(nameof(BlueMaskSize), Prefix.Byte, 1, "Размер синей маски", true);
            _struct.CreateField(nameof(BlueFieldPosition), Prefix.Byte, 1, "Позиция синего канала", true);
            _struct.CreateField(nameof(ReservedMaskSize), Prefix.Byte, 1, "Размер маски зарезервированных бит", true);
            _struct.CreateField(nameof(ReservedFieldPosition), Prefix.Byte, 1, "Позиция зарезервированных бит", true);
            _struct.CreateField(nameof(DirectColorModeInfo), Prefix.Byte, 1, "Информация о прямом цвете", true);
            _struct.CreateField(nameof(PhysicalBasePointer), Prefix.Dword, 1, "Физический адрес LFB (например, 0xE0000000)", true);
            _struct.CreateField(nameof(OffScreenMemoryOffset), Prefix.Dword, 1, "Смещение дополнительной видеопамяти", true);
            _struct.CreateField(nameof(OffScreenMemorySize), Prefix.Word, 1, "Размер дополнительной видеопамяти (в КБ)", true);
            _struct.CreateField("Reserved2", Prefix.Byte, 206, reserved: true);
            //_struct.CreateField(null, $"206 - ($ - {name})", Prefix.Byte);

            Attributes = $"{name}.{nameof(Attributes)}";
            WindowAAttributes = $"{name}.{nameof(WindowAAttributes)}";
            WindowBAttributes = $"{name}.{nameof(WindowBAttributes)}";
            WindowGranularity = $"{name}.{nameof(WindowGranularity)}";
            WindowSize = $"{name}.{nameof(WindowSize)}";
            WindowASegment = $"{name}.{nameof(WindowASegment)}";
            WindowBSegment = $"{name}.{nameof(WindowBSegment)}";
            WindowFunctionPointer = $"{name}.{nameof(WindowFunctionPointer)}";
            BytesPerScanLine = $"{name}.{nameof(BytesPerScanLine)}";
            Width = $"{name}.{nameof(Width)}";
            Height = $"{name}.{nameof(Height)}";
            CharWidth = $"{name}.{nameof(CharWidth)}";
            CharHeight = $"{name}.{nameof(CharHeight)}";
            NumberOfPlanes = $"{name}.{nameof(NumberOfPlanes)}";
            BitsPerPixel = $"{name}.{nameof(BitsPerPixel)}";
            NumberOfBanks = $"{name}.{nameof(NumberOfBanks)}";
            MemoryModel = $"{name}.{nameof(MemoryModel)}";
            BankSize = $"{name}.{nameof(BankSize)}";
            NumberOfImagePages = $"{name}.{nameof(NumberOfImagePages)}";
            Reserved1 = $"{name}.{nameof(Reserved1)}";
            RedMaskSize = $"{name}.{nameof(RedMaskSize)}";
            RedFieldPosition = $"{name}.{nameof(RedFieldPosition)}";
            GreenMaskSize = $"{name}.{nameof(GreenMaskSize)}";
            GreenFieldPosition = $"{name}.{nameof(GreenFieldPosition)}";
            BlueMaskSize = $"{name}.{nameof(BlueMaskSize)}";
            BlueFieldPosition = $"{name}.{nameof(BlueFieldPosition)}";
            ReservedMaskSize = $"{name}.{nameof(ReservedMaskSize)}";
            ReservedFieldPosition = $"{name}.{nameof(ReservedFieldPosition)}";
            DirectColorModeInfo = $"{name}.{nameof(DirectColorModeInfo)}";
            PhysicalBasePointer = $"{name}.{nameof(PhysicalBasePointer)}";
            OffScreenMemoryOffset = $"{name}.{nameof(OffScreenMemoryOffset)}";
            OffScreenMemorySize = $"{name}.{nameof(OffScreenMemorySize)}";
        }

        /// <summary>
        /// Название структуры
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Атрибуты режима (битовая маска)
        /// </summary>
        public string Attributes { get; }
        /// <summary>
        /// Атрибуты окна A
        /// </summary>
        public string WindowAAttributes { get; }
        /// <summary>
        /// Атрибуты окна B
        /// </summary>
        public string WindowBAttributes { get; }
        /// <summary>
        /// Размер гранулярности окна (в КБ)
        /// </summary>
        public string WindowGranularity { get; }
        /// <summary>
        /// Размер окна (в КБ)
        /// </summary>
        public string WindowSize { get; }
        /// <summary>
        /// Сегмент окна A
        /// </summary>
        public string WindowASegment { get; }
        /// <summary>
        /// Сегмент окна B
        /// </summary>
        public string WindowBSegment { get; }
        /// <summary>
        /// Указатель на функцию переключения окна
        /// </summary>
        public string WindowFunctionPointer { get; }
        /// <summary>
        /// Байт на строку (ширина * бит на пиксель / 8)
        /// </summary>
        public string BytesPerScanLine { get; }
        /// <summary>
        /// Ширина в пикселях 
        /// </summary>
        public string Width { get; }
        /// <summary>
        /// Высота в пикселях 
        /// </summary>
        public string Height { get; }
        /// <summary>
        /// Ширина символа (обычно 8)
        /// </summary>
        public string CharWidth { get; }
        /// <summary>
        /// Высота символа (обычно 16)
        /// </summary>
        public string CharHeight { get; }
        /// <summary>
        /// Число плоскостей (обычно 1)
        /// </summary>
        public string NumberOfPlanes { get; }
        /// <summary>
        /// Бит на пиксель (8, 15, 16, 24, 32)
        /// </summary>
        public string BitsPerPixel { get; }
        /// <summary>
        /// Число банков памяти (устарело)
        /// </summary>
        public string NumberOfBanks { get; }
        /// <summary>
        /// Тип цветовой модели (1=текст, 4=прямой цвет, 6=палитра)
        /// </summary>
        public string MemoryModel { get; }
        /// <summary>
        /// Размер банка (устарело)
        /// </summary>
        public string BankSize { get; }
        /// <summary>
        /// Число доступных страниц изображения
        /// </summary>
        public string NumberOfImagePages { get; }
        /// <summary>
        /// Зарезервировано
        /// </summary>
        public string Reserved1 { get; }
        /// <summary>
        /// Размер красной маски (обычно 8)
        /// </summary>
        public string RedMaskSize { get; }
        /// <summary>
        /// Позиция красного канала
        /// </summary>
        public string RedFieldPosition { get; }
        /// <summary>
        /// Размер зеленой маски
        /// </summary>
        public string GreenMaskSize { get; }
        /// <summary>
        /// Позиция зеленого канала
        /// </summary>
        public string GreenFieldPosition { get; }
        /// <summary>
        /// Размер синей маски
        /// </summary>
        public string BlueMaskSize { get; }
        /// <summary>
        /// Позиция синего канала
        /// </summary>
        public string BlueFieldPosition { get; }
        /// <summary>
        /// Размер маски зарезервированных бит
        /// </summary>
        public string ReservedMaskSize { get; }
        /// <summary>
        /// Позиция зарезервированных бит
        /// </summary>
        public string ReservedFieldPosition { get; }
        /// <summary>
        /// Информация о прямом цвете
        /// </summary>
        public string DirectColorModeInfo { get; }
        /// <summary>
        /// Физический адрес LFB (например, 0xE0000000)
        /// </summary>
        public string PhysicalBasePointer { get; }
        /// <summary>
        /// Смещение дополнительной видеопамяти
        /// </summary>
        public string OffScreenMemoryOffset { get; }
        /// <summary>
        /// Размер дополнительной видеопамяти (в КБ)
        /// </summary>
        public string OffScreenMemorySize { get; }

        private readonly AssemblerStruct _struct;

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public GeneratedCode GenerateCode()
        {
            return _struct.GenerateCode();
        }

        #endregion
    }
}
