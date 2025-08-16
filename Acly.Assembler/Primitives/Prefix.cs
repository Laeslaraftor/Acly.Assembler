using System.ComponentModel;

namespace Acly.Assembler
{
    /// <summary>
    /// Префикс оператора
    /// </summary>
    public enum Prefix
    {
        /// <summary>
        /// 16 битное значение
        /// </summary>
        [Description("db")]
        Byte,
        /// <summary>
        /// Слово
        /// </summary>
        [Description("dw")]
        Word,
        /// <summary>
        /// 32 битное значение
        /// </summary>
        [Description("dd")]
        Dword,
        /// <summary>
        /// 64 битное значение
        /// </summary>
        [Description("dq")]
        Qword
    }
}
