using Acly.Assembler.Contexts;
using System.Text;
using System;
using Acly.Assembler.Registers;
using System.Collections.Generic;
using Acly.Assembler.Interruptions;

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

        private static StringBuilder Builder
        {
            get 
            {
                _builder ??= _beginningBuilder;
                return _builder;
            }
            set => _builder = value;
        }

        private static readonly StringBuilder _beginningBuilder = new();
        private static readonly StringBuilder _textBuilder = new();
        private static readonly StringBuilder _dataBuilder = new();
        private static readonly StringBuilder _bssBuilder = new();
        private static readonly Dictionary<string, Variable> _variables = new();

        private static StringBuilder? _builder;
        private static CpuModeContext? _currentContext;

        #region Основные оп-коды

        /// <summary>
        /// Установить элемент как глобальный
        /// </summary>
        /// <param name="label">Название элемента</param>
        public static void SetGlobal(string label)
        {
            Emit($"global {label}");
        }
        /// <summary>
        /// Начать функцию
        /// </summary>
        /// <param name="name">Название функции</param>
        public static void Label(string name)
        {
            Emit();
            Emit($"{name}:", false);
        }
        /// <summary>
        /// Поставить оператор возвращения
        /// </summary>
        public static void Return()
        {
            Emit("ret");
        }
        /// <summary>
        /// Остановить процессор
        /// </summary>
        public static void Halt()
        {
            Emit("hlt");
        }
        /// <summary>
        /// Поставить оператора получения информации о процессоре
        /// </summary>
        public static void CpuId()
        {
            Emit("cpuid");
        }

        /// <summary>
        /// Системный вызов (syscall). Работает только на Linux
        /// </summary>
        public static void SystemCall()
        {
            Emit("syscall");
        }

        /// <summary>
        /// Вызвать функцию по заданному адресу
        /// </summary>
        /// <param name="address">Адрес функции</param>
        public static void Call(MemoryOperand address)
        {
            Emit($"call {address}");
        }
        /// <summary>
        /// Вызвать функцию на названию
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void Call(string label)
        {
            Emit($"call {label}");
        }
        /// <summary>
        /// Перейти к выполнению код по заданному адресу
        /// </summary>
        /// <param name="address">Адрес кода</param>
        public static void Jump(MemoryOperand address)
        {
            Emit($"jmp {address}");
        }
        /// <summary>
        /// Перейти к выполнению функции
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void Jump(string label)
        {
            Emit($"jmp {label}");
        }

        /// <summary>
        /// Совершить прерывание. Некоторые полезные прерывания можно найти в <see cref="Ints"/>.
        /// </summary>
        /// <param name="interruption">Прерывание, которое необходимо совершить</param>
        public static void Interrupt(Interruption interruption)
        {
            Emit($"int {interruption}");
        }

        #endregion

        #region Логика

        /// <summary>
        /// Перейти к функции если значения равны\равно нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfEquals(string label)
        {
            Emit($"je {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значения равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfEquals(MemoryOperand functionAddress)
        {
            Emit($"je {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значения не равны\не равны нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotEquals(string label)
        {
            Emit($"jne {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значения не равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfNotEquals(MemoryOperand functionAddress)
        {
            Emit($"jne {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значение больше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfGreater(string label)
        {
            Emit($"jg {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение больше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfGreater(MemoryOperand functionAddress)
        {
            Emit($"jg {functionAddress}");
        }
        /// <summary>
        /// Перейти к функции если значение меньше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfLess(string label)
        {
            Emit($"jl {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение меньше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfLess(MemoryOperand functionAddress)
        {
            Emit($"jl {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значение больше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfAbove(string label)
        {
            Emit($"ja {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение больше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfAbove(MemoryOperand functionAddress)
        {
            Emit($"ja {functionAddress}");
        }
        /// <summary>
        /// Перейти к функции если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfBelow(string label)
        {
            Emit($"jb {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfBelow(MemoryOperand functionAddress)
        {
            Emit($"jb {functionAddress}");
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// Начать код со стандартной точки входа _start с указанным режимом процессора
        /// </summary>
        /// <param name="mode">Начальный режим процессора</param>
        public static void StartWithMode(Mode mode)
        {
            Switch(mode);
            Section(Assembler.Section.Text);
            SetGlobal("_start");
            Label("_start");
        }

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <param name="comment">Комментарий, который будет добавлен к коду</param>
        public static void Comment(string comment)
        {
            Emit($"; {comment}");
        }
        /// <summary>
        /// Добавить пустую строку
        /// </summary>
        public static void EmptyLine() => Emit();

        /// <summary>
        /// Переключить режим работы процессора. Если будет указан текущий режим, то ничего не произойдёт.
        /// </summary>
        /// <param name="cpuMode">Новый режим работы процессора</param>
        public static void Switch(Mode cpuMode)
        {
            Emit($"[BITS {(int)cpuMode}]");
            _currentContext = CpuModeContext.GetContext(cpuMode);
        }
        /// <summary>
        /// Указание на то, куда разместить следующий фрагмент кода
        /// </summary>
        /// <param name="origin"></param>
        public static void Origin(uint origin)
        {
            Emit($"[ORG 0x{origin:X}]");
        }
        /// <summary>
        /// Задать текущую секцию кода.
        /// </summary>
        /// <param name="section"></param>
        public static void Section(Section section)
        {
            if (section == Assembler.Section.Text)
            {
                Builder = _textBuilder;
            }
            else if (section == Assembler.Section.Data)
            {
                Builder = _dataBuilder;
            }
            else if (section == Assembler.Section.Bss)
            {
                Builder = _bssBuilder;
            }
        }

        internal static void Emit(string line)
        {
            Emit(line, Builder != _beginningBuilder);
        }
        internal static void Emit(string line, bool withTab)
        {
            string tab = string.Empty;

            if (withTab)
            {
                tab = Tab;
            }

            Builder.AppendLine(tab + line);
        }
        internal static void Emit() => Emit(string.Empty, false);

        #endregion

        #region Переменные

        /// <summary>
        /// Создать переменную
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная</returns>
        public static Variable CreateVariable(string name, Size size, bool isReserved = true)
        {
            Variable result = new(size, name, isReserved);
            _variables.Add(name, result);

            return result;
        }
        /// <summary>
        /// Создать переменную массива
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная массива</returns>
        public static ArrayVariable CreateArrayVariable(string name, Size size, bool isReserved = true)
        {
            ArrayVariable result = new(size, name, isReserved);
            _variables.Add(name, result);

            return result;
        }

        #endregion

        #region Управление

        /// <summary>
        /// Получить составленный ассемблерный код
        /// </summary>
        /// <returns>Составленный код</returns>
        public static string GetAssembly()
        {
            StringBuilder result = new();
            result.Append(_beginningBuilder.ToString());

            AppendSection(result, Assembler.Section.Text, _textBuilder.ToString());

            var dataVariables = GetVariables(true);
            var bssVariables = GetVariables(false);

            if (dataVariables != null)
            {
                AppendSection(result, Assembler.Section.Data, dataVariables);
            }
            if (bssVariables != null)
            {
                AppendSection(result, Assembler.Section.Bss, bssVariables);
            }

            return result.ToString();
        }

        private static void AppendSection(StringBuilder builder, Section section, string content)
        {
            builder.AppendLine();
            builder.AppendLine($"section .{section.ToString().ToLower()}");
            builder.AppendLine(content.TrimEnd());
        }
        private static string? GetVariables(bool isReserved)
        {
            StringBuilder result = new();

            foreach (var variable in _variables.Values)
            {
                if (variable.IsReserved == isReserved)
                {
                    result.AppendLine(Tab + variable.AssemblerLine);
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return result.ToString();
        }

        #endregion

        #region Константы

        private const string Tab = "    ";

        #endregion
    }
}
