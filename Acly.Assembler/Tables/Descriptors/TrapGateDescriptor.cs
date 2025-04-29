namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор ловушки прерывания
    /// </summary>
    public class TrapGateDescriptor : HandlerInterruptDescriptor
    {
        internal TrapGateDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GateType Type
        {
            get
            {
                if (Size == GateSize.x16)
                {
                    return GateType.TrapGate16;
                }

                return GateType.TrapGate32;
            }
        }
    }
}
