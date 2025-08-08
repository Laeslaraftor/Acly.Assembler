using Acly.Assembler.Memory;
using Acly.Assembler.Registers;
using Acly.Assembler.Tables;
using System;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Построитель обработчиков исключений
    /// </summary>
    public class ExceptionsHandlerBuilder
    {
        /// <summary>
        /// Обработчик исключений
        /// </summary>
        public IExceptionHandler? Handler { get; set; }
        /// <summary>
        /// Селектор дескриптора кода
        /// </summary>
        public MemoryOperand SegmentSelector { get; set; } = 0;
        /// <summary>
        /// Тип обрабатываемого прерывания
        /// </summary>
        public InterruptionType Type { get; set; }

        #region Управление

        /// <summary>
        /// Построить обработчики исключений
        /// </summary>
        /// <param name="idt">Таблица дескрипторов прерываний в которую будут записаны обработчики</param>
        public void Build(IDT idt)
        {
            if (Handler == null)
            {
                throw new InvalidOperationException("Невозможно построить обработчики исключений, так как обработчик не указан");
            }

            foreach (var exception in Enum.GetValues(typeof(CpuException)))
            {
                var e = (CpuException)exception;
                var handler = Handler.Handle(e);

                InterruptionDescriptor descriptor = new()
                {
                    Description = e.ToString(),
                    Handler = handler,
                    SegmentSelector = SegmentSelector
                };
                descriptor.AccessByte.Type = Type;

                idt.Sections.Add(descriptor);
            }
        }

        #endregion

        #region Классы

        /// <summary>
        /// Обработчик исключений по умолчанию
        /// </summary>
        public class StandardHandler : IExceptionHandler
        {
            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <param name="exception"><inheritdoc/></param>
            /// <returns><inheritdoc/></returns>
            public MemoryOperand Handle(CpuException exception)
            {
                string handlerName = $"{exception}_handler";

                Asm.Add(new HandlerGenerator(exception, handlerName), true);

                return handlerName;
            }

            #region Статика

            /// <summary>
            /// Глобальный экземпляр обработчика исключений
            /// </summary>
            public static StandardHandler Instance { get; } = new();

            private static readonly ColorBuilder _color = new()
            {
                Background = BiosColor.LightRed,
                TextColor = BiosColor.White
            };

            #endregion

            #region Классы

            private class HandlerGenerator : ICodeGenerator
            {
                public HandlerGenerator(CpuException exception, string handlerName)
                {
                    _exception = exception;
                    _handlerName = handlerName;
                }

                private readonly CpuException _exception;
                private readonly string _handlerName;

                public GeneratedCode GenerateCode()
                {
                    var startMode = Asm.CurrentMode;
                    string exceptionName = _exception.GetInfo();
                    var exceptionVariable = Asm.CreateStringVariable($"exception_{_exception}_message", exceptionName);

                    if (startMode != Mode.x32)
                    {
                        Asm.Switch(Mode.x32);
                    }

                    Asm.Label(_handlerName);
                    VideoMemory.Fill(0xCC20, 160 * 50);
                    VideoMemory.PrintString(exceptionVariable, (byte)(39 - exceptionName.Length / 2), 11, 80, _color);
                    Asm.Halt();
                    Asm.InterruptionReturnDouble();

                    return new(Section.Text, string.Empty, Mode.x32);
                }
            }

            #endregion
        }

        #endregion
    }
}
