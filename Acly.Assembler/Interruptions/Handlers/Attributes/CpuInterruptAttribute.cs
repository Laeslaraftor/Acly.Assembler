using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Атрибут, обозначающий аппаратное прерывание
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CpuInterruptAttribute : Attribute
    {
    }
}
