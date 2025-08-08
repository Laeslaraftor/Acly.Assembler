using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Атрибут, обозначающий серьёзную ошибку, после которой состояние процесса не сохраняется
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CpuAbortAttribute : Attribute
    {
    }
}
