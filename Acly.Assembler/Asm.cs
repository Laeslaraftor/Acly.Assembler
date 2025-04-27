using Acly.Assembler.Contexts;
using System.Text;
using System;
using Acly.Assembler.Registers;

namespace Acly.Assembler
{
    /// <summary>
    /// Статический класс, в котором происходит основная движуха
    /// </summary>
    public static class Asm
    {
        /// <summary>
        /// Текущий контекст режима процессора
        /// </summary>
        public static CpuModeContext Context
        {
            get
            {
                if (_currentContext == null)
                {
                    throw new InvalidOperationException("Режим работы процессора не был установлен");
                }

                return _currentContext;
            }
        }

        private static readonly StringBuilder _builder = new();
        private static CpuModeContext? _currentContext;

        #region Основные оп-коды

        public static void Label(string name)
        {
            _builder.AppendLine($".{name}");
        }
        public static void Comment(string comment)
        {
            _builder.AppendLine($"; {comment}");
        }

        public static void Set(Register destination, Register source, bool destinationAsPointer = false, bool sourceAsPointer = false)
        {
            string to = destination.ToString();
            string from = source.ToString();
            string opCode = "mov";

            if (destinationAsPointer) 
            {
                from = $"[{from}]";
            }
            if (sourceAsPointer)
            {
                to = $"[{to}]";
            }

            if (destination.Size > source.Size)
            {
                opCode = "movxz";
            }
            else if (source.Size > destination.Size)
            {
                if (source is GeneralRegister generalSource && generalSource.Lower != null)
                {
                    Set(destination, generalSource.Lower, destinationAsPointer, sourceAsPointer);
                    return;
                } 

                throw new InvalidOperationException($"Нельзя присвоить меньшему регистру {destination.Size} значение из большего {source.Size}");
            }

            _builder.AppendLine($"{opCode} {to}, {from}");
        }
        public static void Return()
        {
            _builder.AppendLine("ret");
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// Переключить режим работы процессора. Если будет указан текущий режим, то ничего не произойдёт.
        /// </summary>
        /// <param name="cpuMode">Новый режим работы процессора</param>
        public static void Switch(Mode cpuMode)
        {
            _builder.AppendLine($"[BITS {(int)cpuMode}]");
            _currentContext = CpuModeContext.GetContext(cpuMode);
        }
        /// <summary>
        /// Указание на то, куда разместить следующий фрагмент кода
        /// </summary>
        /// <param name="origin"></param>
        public static void Origin(uint origin)
        {
            _builder.AppendLine($"[ORG 0x{origin:X}]");
        }
        /// <summary>
        /// Задать текущую секцию кода.
        /// </summary>
        /// <param name="section"></param>
        public static void Section(Section section)
        {
            _builder.AppendLine($"section .{section.ToString().ToLower()}");
        }

        #endregion

        #region Управление

        /// <summary>
        /// Получить составленный ассемблерный код
        /// </summary>
        /// <returns>Составленный код</returns>
        public static string GetAssembly()
        {
            return _builder.ToString();
        }

        #endregion
    }
}
