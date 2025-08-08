using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Атрибут, обозначающий исключение процессора
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CpuFaultAttribute : Attribute
    {
    }
}
