namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор шлюза прерывания
    /// </summary>
    public class InterruptGateDescriptor : HandlerInterruptDescriptor
    {
        internal InterruptGateDescriptor(string name) : base(name)
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
                    return GateType.InterruptGate16;
                }

                return GateType.InterruptGate32;
            }
        }
    }
}
