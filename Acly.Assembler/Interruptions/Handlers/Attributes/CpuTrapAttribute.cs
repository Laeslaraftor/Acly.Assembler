using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Атрибут, обозначающий отладочное исключение/прерывание
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CpuTrapAttribute : Attribute
    {
    }
}
