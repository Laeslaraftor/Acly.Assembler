using Acly.Assembler.Registers;

namespace Acly.Assembler.Video
{
    /// <summary>
    /// Класс, представляющий структуру с VBE информацией
    /// </summary>
    public class VbeInfoStructure : ICodeGenerator
    {
        /// <summary>
        /// Создать новый экземпляр структуры с VBE информацией
        /// </summary>
        public VbeInfoStructure() : this("vbe_info")
        {

        }
        /// <summary>
        /// Создать новый экземпляр структуры с VBE информацией
        /// </summary>
        /// <param name="name">Название структуры</param>
        public VbeInfoStructure(string name)
        {
            Name = name;
            _struct = new(name);

            _struct.CreateField(nameof(Signature), Prefix.Byte, "'VBE2'", "Должно быть \"VESA\" (4 байта)");
            _struct.CreateField(nameof(Version), Prefix.Word, "Версия VBE (например, 0x0300 для VBE 3.0)");
            _struct.CreateField(nameof(OemStringPointer), Prefix.Dword, "FAR-указатель на строку производителя (сегмент:смещение)");
            _struct.CreateField(nameof(Capabilities), Prefix.Dword, "Битовые флаги возможностей");
            _struct.CreateField(nameof(VideoModePointer), Prefix.Dword, "FAR-указатель на список поддерживаемых видеорежимов");
            _struct.CreateField(nameof(TotalMemory), Prefix.Word, "Кол-во 64КБ блоков видеопамяти (например, 8 = 512КБ)");
            _struct.CreateField(nameof(OemSoftwareRevision), Prefix.Word, "Ревизия ПО OEM");
            _struct.CreateField(nameof(OemVendorNamePointer), Prefix.Dword, "Указатель на имя производителя");
            _struct.CreateField(nameof(OemProductNamePointer), Prefix.Dword, "Указатель на название продукта");
            _struct.CreateField(nameof(OemProductRevisionPointer), Prefix.Dword, "Указатель на ревизию продукта");
            _struct.CreateField(null, 222, Prefix.Byte, "Reserved (222 байта)");
            _struct.CreateField(nameof(OemData), Prefix.Byte, "Данные OEM (256 байт)");

            Signature = $"{name}.{nameof(Signature)}";
            Version = $"{name}.{nameof(Version)}";
            OemStringPointer = $"{name}.{nameof(OemStringPointer)}";
            Capabilities = $"{name}.{nameof(Capabilities)}";
            VideoModePointer = $"{name}.{nameof(VideoModePointer)}";
            TotalMemory = $"{name}.{nameof(TotalMemory)}";
            OemSoftwareRevision = $"{name}.{nameof(OemSoftwareRevision)}";
            OemVendorNamePointer = $"{name}.{nameof(OemVendorNamePointer)}";
            OemProductNamePointer = $"{name}.{nameof(OemProductNamePointer)}";
            OemProductRevisionPointer = $"{name}.{nameof(OemProductRevisionPointer)}";
            OemData = $"{name}.{nameof(OemData)}";
        }

        /// <summary>
        /// Название структуры
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Должно содержать 'VESA' после вызова INT 0x10. Если осталось 'VBE2' — VBE не поддерживается.
        /// </summary>
        public string Signature { get; }
        /// <summary>
        /// Версия VBE в формате BCD. Например: 0x0200 — VBE 2.0, 0x0300 — VBE 3.0
        /// </summary>
        public string Version { get; }
        /// <summary>
        /// Указатель (сегмент:смещение) на строку производителя в формате ASCIIZ.
        /// </summary>
        public string OemStringPointer { get; }
        /// <summary>
        /// Битовые флаги:
        /// <br></br>
        /// Бит 0: DAC поддерживает 8-битный режим. 
        /// <br></br>
        /// Бит 1: Возможность переключения VGA в VESA режим. 
        /// <br></br>
        /// Бит 2: Поддержка RAMDAC с программируемыми частотами.
        /// </summary>
        public string Capabilities { get; }
        /// <summary>
        /// Указатель на список поддерживаемых видеорежимов (каждый режим — word, заканчивается 0xFFFF).
        /// <br></br>
        /// Пример: 0x0111, 0x0112, ..., 0xFFFF.
        /// </summary>
        public string VideoModePointer { get; }
        /// <summary>
        /// Объем видеопамяти в 64КБ блоках. Например:
        /// <br></br>
        /// 8 = 8 × 64КБ = 512КБ.
        /// <br></br>
        /// 32 = 32 × 64КБ = 2МБ.
        /// </summary>
        public string TotalMemory { get; }
        /// <summary>
        /// Ревизия ПО OEM
        /// </summary>
        public string OemSoftwareRevision { get; }
        /// <summary>
        /// Указатель на имя производителя
        /// </summary>
        public string OemVendorNamePointer { get; }
        /// <summary>
        /// Указатель на название продукта
        /// </summary>
        public string OemProductNamePointer { get; }
        /// <summary>
        /// Указатель на ревизию продукта
        /// </summary>
        public string OemProductRevisionPointer { get; }
        /// <summary>
        /// Данные OEM (256 байт)
        /// </summary>
        public string OemData { get; }

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
