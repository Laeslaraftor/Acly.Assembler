using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Demos.Bootloader32
{
    public class ExceptionsHandler : IExceptionHandler
    {
        public MemoryOperand ExitEntryPoint { get; set; } = 0;
        public MemoryOperand PitHandlerPointer { get; set; } = 0;
        public string HandlerName { get; set; } = "exception_handler";

        private readonly Dictionary<CpuInterruption, Handler> _codeGenerators = [];
        private Handler? _reservedHandlerGenerator;

        public MemoryOperand Handle(CpuInterruption interruption)
        {
            Handler? generator;
            bool isReserved = interruption.IsReserved();

            if (isReserved && _reservedHandlerGenerator != null)
            {
                return _reservedHandlerGenerator.HandlerName;
            }
            else
            {
                if (_codeGenerators.TryGetValue(interruption, out generator))
                {
                    return generator.HandlerName;
                }
            }

            string handlerName = $"{HandlerName}_{interruption}";

            if (interruption == CpuInterruption.PIT)
            {
                generator = new PitHandlerCodeGenerator()
                {
                    HandlerPointer = PitHandlerPointer
                };
            }
            else
            {
                generator = new HandlerCodeGenerator()
                {
                    ExitEntryPoint = ExitEntryPoint,
                    ErrorMessageVariable = Asm.CreateStringVariable($"{handlerName}_message", interruption.GetInfo())
                };
            }

            generator.HandlerName = handlerName;

            if (isReserved)
            {
                _reservedHandlerGenerator = generator;
            }
            else
            {
                _codeGenerators.Add(interruption, generator);
            }
                
            Asm.Add(generator, true);

            return handlerName;
        }

        #region Классы

        private abstract class Handler : ICodeGenerator
        {
            public string HandlerName { get; set; } = string.Empty;

            public abstract GeneratedCode GenerateCode();
        }
        private class HandlerCodeGenerator : Handler
        {
            public MemoryOperand ExitEntryPoint { get; set; } = 0;
            public StringVariable? ErrorMessageVariable { get; set; }

            public override GeneratedCode GenerateCode()
            {
                var startMode = Asm.CurrentMode;

                if (startMode != Mode.x32)
                {
                    Asm.Switch(Mode.x32);
                }

                Asm.Label(HandlerName);
                ProtectedModeContext.Instance.StackPointer.Set(Prefix.Dword, true, ExitEntryPoint);

                if (ErrorMessageVariable != null)
                {
                    ProtectedModeContext.Instance.SourceIndex.Set(ErrorMessageVariable);
                }

                Asm.InterruptionReturn();

                return new(Section.Text, string.Empty, Mode.x32);
            }
        }
        private class PitHandlerCodeGenerator : Handler
        {
            public MemoryOperand HandlerPointer { get; set; } = 0;

            public override GeneratedCode GenerateCode()
            {
                var startMode = Asm.CurrentMode;

                Asm.Label(HandlerName);

                Asm.PushAll();

                Asm.EmptyLine();
                Asm.Call(HandlerPointer);
                Asm.EmptyLine();

                Asm.Comment("Сброс сигнала на PIC");
                Ints.CPU.PIT.Reset();
                Asm.EmptyLine();

                Asm.PopAll();

                Asm.InterruptionReturnAuto();

                return new(Section.Text, string.Empty, startMode);
            }
        }

        #endregion
    }
}
