using Acly.Assembler.Registers;

namespace Acly.Assembler.Contexts
{
    /// <summary>
    /// Контекст 16 битного режима процессора (Real Mode).
    /// </summary>
    public class RealModeContext : CpuModeContext
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Mode Mode { get; } = Mode.x16;

        #region Общие регистры

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Accumulator { get; } =  new(Size.x16, "AX", new GeneralRegister(Size.x8, "AH"), new GeneralRegister(Size.x8, "AL"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Base { get; } = new(Size.x16, "BX", new GeneralRegister(Size.x8, "BH"), new GeneralRegister(Size.x8, "BL"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Count { get; } = new(Size.x16, "CX", new GeneralRegister(Size.x8, "CH"), new GeneralRegister(Size.x8, "CL"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister Data { get; } = new(Size.x16, "DX", new GeneralRegister(Size.x8, "DH"), new GeneralRegister(Size.x8, "DL"));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R8 { get; } = new(Size.x16, "R8W", null, new GeneralRegister(Size.x8, "R8B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R9 { get; } = new(Size.x16, "R9W", null, new GeneralRegister(Size.x8, "R9B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R10 { get; } = new(Size.x16, "R10W", null, new GeneralRegister(Size.x8, "R10B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R11 { get; } = new(Size.x16, "R11W", null, new GeneralRegister(Size.x8, "R11B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R12 { get; } = new(Size.x16, "R12W", null, new GeneralRegister(Size.x8, "R12B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R13 { get; } = new(Size.x16, "R13W", null, new GeneralRegister(Size.x8, "R13B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R14 { get; } = new(Size.x16, "R14W", null, new GeneralRegister(Size.x8, "R14B"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister R15 { get; } = new(Size.x16, "R15W", null, new GeneralRegister(Size.x8, "R15B"));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister SourceIndex { get; } = new(Size.x16, "SI", null, new GeneralRegister(Size.x8, "SIL"));
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GeneralRegister DestinationIndex { get; } = new(Size.x16, "DI", null, new GeneralRegister(Size.x8, "DIL"));

        #endregion

        #region Сегменты

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register CodeSegment { get; } = new(Size.x16, "CS");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register DataSegment { get; } = new(Size.x16, "DS");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register StackSegment { get; } = new(Size.x16, "SS");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register ExtraSegment { get; } = new(Size.x16, "ES");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register FrameSegment { get; } = new(Size.x16, "FS");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register GlobalSegment { get; } = new(Size.x16, "GS");

        #endregion

        #region Указатели

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register InstructionPointer { get; } = new(Size.x16, "IP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register StackPointer { get; } = new(Size.x16, "SP");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register BasePointer { get; } = new(Size.x16, "BP");

        #endregion

        #region Флаги

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register ZeroFlag { get; } = new(Size.x16, "ZF");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register CarryFlag { get; } = new(Size.x16, "CF");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register SignFlag { get; } = new(Size.x16, "SF");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register OverflowFlag { get; } = new(Size.x16, "OF");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register InterruptFlag { get; } = new(Size.x16, "IF");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Register DirectionFlag { get; } = new(Size.x16, "DF");

        #endregion

        #region Статика

        /// <summary>
        /// Глобальный экземпляр 16 битного контекста
        /// </summary>
        public static readonly RealModeContext Instance = new();

        #endregion
    }
}
