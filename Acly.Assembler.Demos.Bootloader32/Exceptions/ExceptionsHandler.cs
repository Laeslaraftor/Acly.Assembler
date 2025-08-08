using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Demos.Bootloader32
{
    public class ExceptionsHandler : IExceptionHandler
    {
        public MemoryOperand ExitEntryPoint { get; set; } = 0;
        public string HandlerName { get; set; } = "exception_handler";

        private readonly Dictionary<CpuException, HandlerCodeGenerator> _codeGenerators = [];

        public MemoryOperand Handle(CpuException exception)
        {
            if (_codeGenerators.TryGetValue(exception, out var generator))
            {
                return generator.HandlerName;
            }

            string handlerName = $"{HandlerName}_{exception}";
            generator = new()
            {
                ExitEntryPoint = ExitEntryPoint,
                HandlerName = handlerName,
                ErrorMessageVariable = Asm.CreateStringVariable($"{handlerName}_message", exception.GetInfo())
            };

            _codeGenerators.Add(exception, generator);
            Asm.Add(generator, true);

            return generator.HandlerName;
        }

        #region Классы

        private class HandlerCodeGenerator : ICodeGenerator
        {
            public MemoryOperand ExitEntryPoint { get; set; } = 0;
            public string HandlerName { get; set; } = string.Empty;
            public StringVariable? ErrorMessageVariable { get; set; }

            public GeneratedCode GenerateCode()
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

        #endregion
    }
}
