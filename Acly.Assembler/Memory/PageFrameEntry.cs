using System.Collections.Generic;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Страница памяти
    /// </summary>
    public class PageFrameEntry : PageTableEntry
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="framePhysicalAddress"><inheritdoc/></param>
        /// <param name="flags"><inheritdoc/></param>
        public PageFrameEntry(ulong framePhysicalAddress, PageTableFlags flags)
            : base(framePhysicalAddress, flags)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override PageEntryType EntryType { get; } = PageEntryType.PageFrame;

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToAssembler()
        {
            return $"dq 0x{GetEntryValue():X16} ; Page Frame | {FlagsToString()}";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        protected override string FlagsToString()
        {
            List<string> flags = new();

            if (Flags.HasFlag(PageTableFlags.Writable))
            {
                flags.Add("RW");
            }
            if (Flags.HasFlag(PageTableFlags.UserAccess))
            {
                flags.Add("US");
            }
            if (Flags.HasFlag(PageTableFlags.Accessed))
            {
                flags.Add("A");
            }
            if (Flags.HasFlag(PageTableFlags.Dirty))
            {
                flags.Add("D");
            }
            if (Flags.HasFlag(PageTableFlags.Global))
            {
                flags.Add("G");
            }
            if (Flags.HasFlag(PageTableFlags.NoExecute))
            {
                flags.Add("NX");
            }
            if (Flags.HasFlag(PageTableFlags.LargePage))
            {
                flags.Add("PS");
            }

            return string.Join("|", flags);
        }

        #endregion
    }
}
